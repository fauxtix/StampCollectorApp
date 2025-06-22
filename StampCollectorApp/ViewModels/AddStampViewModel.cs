using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StampCollectorApp.Models;
using StampCollectorApp.Resources.Languages;
using StampCollectorApp.Services;
using StampCollectorApp.Views;
using System.Collections.ObjectModel;

namespace StampCollectorApp.ViewModels
{
    [QueryProperty(nameof(Categories), "Categories")]
    [QueryProperty(nameof(Collections), "Collections")]
    [QueryProperty(nameof(Countries), "Countries")]
    [QueryProperty(nameof(SelectedStamp), "SelectedStamp")]
    public partial class AddStampViewModel : ObservableObject
    {
        private readonly IStampService _stampService;
        private readonly ICollectionService _collectionService;

        private int _currentPage = 1;
        //public List<StampCondition> Conditions { get; } = Enum.GetValues(typeof(StampCondition)).Cast<StampCondition>().ToList();
        public bool CanFetchImageFromApi =>
            Year.HasValue && Year > 1865;

        private const string PixabayApiKey = "50529772-442b5126c14cb25b021e56dff";
        private const string PixabayApiUrl = "https://pixabay.com/api/";
        public List<int> Years { get; } = Enumerable.Range(1865, DateTime.Now.Year - 1865 + 1).ToList();

        public ObservableCollection<WikiStamps> WikiStamps { get; set; } = new();

        // Lista de categorias válidas do Pixabay
        private static readonly HashSet<string> PixabayCategories = new()
        {
            "backgrounds", "fashion", "nature", "science", "education", "feelings", "health", "people",
            "religion", "places", "animals", "industry", "computer", "food", "sports", "transportation",
            "travel", "buildings", "business", "music"
        };

        [ObservableProperty]
        private List<Category> categories;
        [ObservableProperty]
        private List<Collection> collections;
        [ObservableProperty]
        private List<Country> countries;

        [ObservableProperty]
        private Category selectedCategory;

        [ObservableProperty]
        private Collection selectedCollection;
        [ObservableProperty]
        private Country selectedCountry;

        [ObservableProperty]
        private Stamp selectedStamp;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private int? year;

        [ObservableProperty]
        private StampCondition condition;

        [ObservableProperty]
        private string imagePath;

        [ObservableProperty]
        private int categoryId;

        [ObservableProperty]
        private int collectionId;
        [ObservableProperty]
        private int countryId;

        [ObservableProperty] private string faceValue;
        [ObservableProperty] private string stampLocation;
        [ObservableProperty] private DateTime acquisitionDate = DateTime.Now;
        [ObservableProperty] private decimal pricePaid;
        [ObservableProperty] private bool forExchange;
        [ObservableProperty] private string notes;
        [ObservableProperty] private int tagId;

        public List<StampConditionDisplayOption> Conditions { get; } =
    LocalizationHelper.GetLocalizedConditions();

        [ObservableProperty]
        private StampConditionDisplayOption selectedConditionOption;

        [ObservableProperty]
        private StampCondition? selectedCondition;

        public AddStampViewModel(IStampService stampService, ICollectionService collectionService)
        {
            _stampService = stampService;

            Categories = new List<Category>();
            Collections = new List<Collection>();
            Countries = new List<Country>();
            SelectedCategory = new Category();
            SelectedCollection = new Collection();
            SelectedCountry = new Country();
            SelectedStamp = new Stamp();
            Name = string.Empty;
            ImagePath = "stamp.png";
            FaceValue = string.Empty;
            StampLocation = string.Empty;
            Notes = string.Empty;

            AcquisitionDate = DateTime.Now;
            Notes = string.Empty;
            _collectionService = collectionService;
        }



        partial void OnYearChanged(int? value)
        {
            OnPropertyChanged(nameof(CanFetchImageFromApi));
        }
        partial void OnSelectedCategoryChanged(Category value)
        {
            if (value != null)
            {
                CategoryId = value.Id;
            }

            _currentPage = 1;

            OnPropertyChanged(nameof(CanFetchImageFromApi));
        }

        partial void OnSelectedCollectionChanged(Collection value)
        {
            if (value != null)
                CollectionId = value.Id;

            OnPropertyChanged(nameof(CanFetchImageFromApi));
        }
        partial void OnSelectedCountryChanged(Country value)
        {
            if (value != null)
                CountryId = value.Id;

            OnPropertyChanged(nameof(CanFetchImageFromApi));
        }


        partial void OnSelectedStampChanged(Stamp? value)
        {
            if (value != null)
            {
                Name = value.Name ?? string.Empty;
                Year = value.Year;
                Condition = value.Condition;
                ImagePath = value.ImagePath ?? string.Empty;
                CategoryId = value.CategoryId;
                CollectionId = value.CollectionId;
                CountryId = value.CountryId;
                FaceValue = value.FaceValue ?? "";
                StampLocation = value.StampLocation ?? string.Empty;
                AcquisitionDate = value.AcquisitionDate;
                PricePaid = value.PricePaid;
                ForExchange = value.ForExchange;
                Notes = value.Notes ?? string.Empty;
                TagId = value.TagId;
                SelectedCategory = Categories?.FirstOrDefault(c => c.Id == value.CategoryId) ?? new Category();
                SelectedCollection = Collections?.FirstOrDefault(c => c.Id == value.CollectionId) ?? new Collection();
                SelectedCountry = Countries?.FirstOrDefault(c => c.Id == value.CountryId) ?? new Country();
            }
            else
            {
                Name = string.Empty;
                Year = DateTime.Now.Year;
                Condition = StampCondition.Novo;
                ImagePath = string.Empty;
                CategoryId = 0;
                CollectionId = 0;
                CountryId = 0;
                FaceValue = "";
                StampLocation = string.Empty;
                AcquisitionDate = DateTime.Now;
                PricePaid = 0;
                ForExchange = false;
                Notes = string.Empty;
                TagId = 0;
                SelectedCategory = null;
                SelectedCollection = null;
            }
        }

        partial void OnSelectedConditionOptionChanged(StampConditionDisplayOption value)
        {
            if (value != null)
                SelectedCondition = value.Value;
            else
                SelectedCondition = null;
        }

        [RelayCommand]
        public async Task SaveStamp()
        {
            string validationTitle = AppResources.TituloValidacao;
            string validationYear = AppResources.TituloValidacaoAno;
            string validationCategory = AppResources.TituloValidacaoCategoria;
            string validationStampCollection = AppResources.TituloValidacaoColecao;
            string validationStampName = AppResources.TituloValidacaoNomeSelo;
            string validationCountry = AppResources.TituloValidacaoPais;
            string validationStampImage = AppResources.TituloValidacaoImagemSelo;
            string validationStampAcquisiition = AppResources.TituloValidacaoAquisicao;
            string validationColectionComplete = AppResources.TituloColecaoCompleta;
            string validationColectionCompleteCaption = AppResources.TituloColecaoCompletaCaption;

            if (Year < 1700 || Year > DateTime.Now.Year)
            {
                await Shell.Current.DisplayAlert(validationTitle, $"{validationYear}{DateTime.Now.Year}.", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(Name))
            {
                await Shell.Current.DisplayAlert(validationTitle, validationStampName, "OK");
                return;
            }
            if (SelectedCategory == null)
            {
                await Shell.Current.DisplayAlert(validationTitle, validationCategory, "OK");
                return;
            }
            if (SelectedCountry == null)
            {
                await Shell.Current.DisplayAlert(validationTitle, validationCountry, "OK");
                return;
            }
            if (SelectedCollection == null)
            {
                await Shell.Current.DisplayAlert(validationTitle, validationStampCollection, "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(ImagePath))
            {
                await Shell.Current.DisplayAlert(validationTitle, validationStampImage, "OK");
                return;
            }

            //if (FaceValue <= 0)
            //{
            //    await Shell.Current.DisplayAlert("Validação", "O valor facial deve ser maior que zero.", "OK");
            //    return;
            //}
            if (AcquisitionDate.Date > DateTime.Now.Date)
            {
                await Shell.Current.DisplayAlert(validationTitle, validationStampAcquisiition, "OK");
                return;
            }

            //if (!File.Exists(ImagePath) && !ImagePath.StartsWith("http"))
            //{
            //    await Shell.Current.DisplayAlert("Validação", "A imagem não existe.", "OK");
            //    return;
            //}
            if (ImagePath.StartsWith("http") && !Uri.IsWellFormedUriString(ImagePath, UriKind.Absolute))
            {
                await Shell.Current.DisplayAlert("Validação", "URL inválido (imagem).", "OK");
                return;
            }

            var collection = SelectedCollection;

            bool isCollectionFull = collection.TotalCollected >= collection.TotalExpected;

            bool forExchange = false;
            if (isCollectionFull)
            {
                bool createForExchange = await Shell.Current.DisplayAlert(
                    validationColectionComplete,
                    validationColectionCompleteCaption,
                    AppResources.TituloSim,
                    AppResources.TituloNao);

                if (!createForExchange)
                    return;

                forExchange = true;
            }

            var stamp = new Stamp
            {
                Name = Name,
                Year = Year.Value,
                Condition = Condition,
                ImagePath = ImagePath,
                CategoryId = CategoryId,
                CollectionId = CollectionId,
                CountryId = CountryId,
                FaceValue = FaceValue,
                StampLocation = StampLocation,
                AcquisitionDate = AcquisitionDate,
                PricePaid = PricePaid,
                ForExchange = ForExchange,
                Notes = Notes,
                TagId = TagId
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

            if (!forExchange)
            {
                collection.TotalCollected = (collection.TotalCollected ?? 0) + 1;
                await _collectionService.SaveCollectionAsync(collection);
            }


            await Shell.Current.GoToAsync("..");
        }
        [RelayCommand]
        public async Task FetchImageFromApi()
        {
            var current = Connectivity.NetworkAccess;

            if (current != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("No Internet", "Check the connection and try again.", "OK");
                return;
            }

            var connectionType = Connectivity.ConnectionProfiles.Contains(ConnectionProfile.WiFi)
                ? "Wi-Fi"
                : Connectivity.ConnectionProfiles.Contains(ConnectionProfile.Cellular)
                    ? "Cellular"
                    : "Unknown";

            var pixabayCategory = GetPixabayCategory(SelectedCategory?.Name);

            var imageUrl = await GetStampImageUrlAsync(null, pixabayCategory, _currentPage);

            if (!string.IsNullOrEmpty(imageUrl))
                ImagePath = imageUrl;
            else
                ImagePath = "stamp.png";
        }

        [RelayCommand]
        private async Task ViewImage()
        {
            if (!string.IsNullOrWhiteSpace(ImagePath))
            {
                await Shell.Current.GoToAsync($"{nameof(ViewStampImagePage)}?ImagePath={Uri.EscapeDataString(ImagePath)}");
            }
        }


        [RelayCommand]
        public async Task PickImageFromDevice()
        {
            try
            {
                var result = await FilePicker.Default.PickAsync(new PickOptions
                {
                    PickerTitle = AppResources.TituloSelecaoImagem,
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

        public async Task<string?> GetStampImageUrlAsync(string? query, string? category = null, int page = 1)
        {
            using var httpClient = new HttpClient();
            var url = $"{PixabayApiUrl}?key={PixabayApiKey}&image_type=photo&per_page=30&page={page}";

            if (!string.IsNullOrWhiteSpace(query))
                url += $"&q={Uri.EscapeDataString(query)}";

            if (!string.IsNullOrWhiteSpace(category))
                url += $"&category={Uri.EscapeDataString(category)}";

            var response = await httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            var result = System.Text.Json.JsonDocument.Parse(json);
            var hits = result.RootElement.GetProperty("hits");
            int count = hits.GetArrayLength();
            if (count > 0)
            {
                var random = new Random();
                int index = random.Next(count);
                return hits[index].GetProperty("webformatURL").GetString();
            }

            return null;
        }
    }
}
