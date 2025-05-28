using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StampCollectorApp.Models;
using StampCollectorApp.Services;

namespace StampCollectorApp.ViewModels
{
    [QueryProperty(nameof(Categories), "Categories")]
    [QueryProperty(nameof(SelectedStamp), "SelectedStamp")]
    public partial class AddStampViewModel : ObservableObject
    {
        private readonly IStampService _stampService;
        public List<StampCondition> Conditions { get; } = Enum.GetValues(typeof(StampCondition)).Cast<StampCondition>().ToList();
        public bool CanFetchImageFromApi => SelectedCategory != null;

        private const string PixabayApiKey = "50529772-442b5126c14cb25b021e56dff";
        private const string PixabayApiUrl = "https://pixabay.com/api/";
        public List<int> Years { get; } = Enumerable.Range(1865, DateTime.Now.Year - 1865 + 1).ToList();

        // Lista de categorias válidas do Pixabay
        private static readonly HashSet<string> PixabayCategories = new()
        {
            "backgrounds", "fashion", "nature", "science", "education", "feelings", "health", "people",
            "religion", "places", "animals", "industry", "computer", "food", "sports", "transportation",
            "travel", "buildings", "business", "music"
        };

        public AddStampViewModel(IStampService stampService)
        {
            _stampService = stampService;
        }

        [ObservableProperty]
        private List<Category> categories;

        [ObservableProperty]
        private Category selectedCategory;

        [ObservableProperty]
        private Stamp selectedStamp;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string country;

        [ObservableProperty]
        private int? year;

        [ObservableProperty]
        private StampCondition condition;

        [ObservableProperty]
        private string imagePath;

        [ObservableProperty]
        private int categoryId;

        partial void OnSelectedCategoryChanged(Category value)
        {
            if (value != null)
                CategoryId = value.Id;

            OnPropertyChanged(nameof(CanFetchImageFromApi));
        }

        partial void OnSelectedStampChanged(Stamp? value)
        {
            if (value != null)
            {
                Name = value.Name ?? string.Empty; // Ensure null-safe assignment
                Country = value.Country ?? string.Empty; // Ensure null-safe assignment
                Year = value.Year;
                Condition = value.Condition;
                ImagePath = value.ImagePath ?? string.Empty; // Ensure null-safe assignment
                CategoryId = value.CategoryId;
                SelectedCategory = Categories?.FirstOrDefault(c => c.Id == value.CategoryId) ?? new Category();
            }
            else
            {
                Name = string.Empty;
                Country = string.Empty;
                Year = DateTime.Now.Year;
                Condition = StampCondition.Novo;
                ImagePath = string.Empty;
                CategoryId = 0;
                SelectedCategory = null;
            }
        }

        [RelayCommand]
        public async Task SaveStamp()
        {
            if (Year < 1865 || Year > DateTime.Now.Year)
            {
                await Shell.Current.DisplayAlert("Validação", $"O Ano deverá estar entre 1865 e {DateTime.Now.Year}.", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(Name))
            {
                await Shell.Current.DisplayAlert("Validação", "Preencha Nome para o selo.", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(Country))
            {
                await Shell.Current.DisplayAlert("Validação", "Preencha País.", "OK");
                return;
            }
            if (SelectedCategory == null)
            {
                await Shell.Current.DisplayAlert("Validação", "Selecione Categoria.", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(ImagePath))
            {
                await Shell.Current.DisplayAlert("Validação", "Selecione uma imagem para o selo.", "OK");
                return;
            }
            if (!File.Exists(ImagePath) && !ImagePath.StartsWith("http"))
            {
                await Shell.Current.DisplayAlert("Validação", "A imagem não existe.", "OK");
                return;
            }
            if (ImagePath.StartsWith("http") && !Uri.IsWellFormedUriString(ImagePath, UriKind.Absolute))
            {
                await Shell.Current.DisplayAlert("Validação", "URL inválido (imagem).", "OK");
                return;
            }

            var stamp = new Stamp
            {
                Name = Name,
                Country = Country,
                Year = Year.Value,
                Condition = Condition,
                ImagePath = ImagePath,
                CategoryId = CategoryId
            };

            if (SelectedStamp != null && SelectedStamp.Id != 0)
            {
                stamp.Id = SelectedStamp.Id;
                await _stampService.SaveStampAsync(stamp);
            }
            else
            {
                await _stampService.SaveStampAsync(stamp);
            }

            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        public async Task FetchImageFromApi()
        {
            var query = Name; // Só o nome do selo
            var pixabayCategory = GetPixabayCategory(SelectedCategory?.Name);

            var imageUrl = await GetStampImageUrlAsync(query, pixabayCategory);
            if (!string.IsNullOrEmpty(imageUrl))
                ImagePath = imageUrl;
            else
                ImagePath = "gallery.png";
        }

        [RelayCommand]
        public async Task PickImageFromDevice()
        {
            try
            {
                var result = await FilePicker.Default.PickAsync(new PickOptions
                {
                    PickerTitle = "Escolha uma imagem",
                    FileTypes = FilePickerFileType.Images
                });

                if (result != null)
                {
                    ImagePath = result.FullPath;
                }
            }
            catch
            {
            }
        }

        private string? GetPixabayCategory(string? categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
                return null;

            var normalized = categoryName.Trim().ToLowerInvariant();
            if (PixabayCategories.Contains(normalized))
                return normalized;

            return null;
        }

        // ALTERADO: agora aceita parâmetro de categoria
        public async Task<string?> GetStampImageUrlAsync(string query, string? category = null)
        {
            using var httpClient = new HttpClient();
            var url = $"{PixabayApiUrl}?key={PixabayApiKey}&q={Uri.EscapeDataString(query)}&image_type=photo&per_page=3";

            if (!string.IsNullOrWhiteSpace(category))
                url += $"&category={Uri.EscapeDataString(category)}";

            var response = await httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            var result = System.Text.Json.JsonDocument.Parse(json);
            var hits = result.RootElement.GetProperty("hits");
            if (hits.GetArrayLength() > 0)
                return hits[0].GetProperty("webformatURL").GetString();

            return null;
        }
    }
}
