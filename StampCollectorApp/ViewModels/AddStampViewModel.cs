using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StampCollectorApp.Models;
using StampCollectorApp.Services;
using StampCollectorApp.Views;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace StampCollectorApp.ViewModels
{
    [QueryProperty(nameof(Categories), "Categories")]
    [QueryProperty(nameof(Collections), "Collections")]
    [QueryProperty(nameof(SelectedStamp), "SelectedStamp")]
    public partial class AddStampViewModel : ObservableObject
    {
        private readonly IStampService _stampService;
        //private readonly ICollectionService _collectionService;

        private int _currentPage = 1;
        public List<StampCondition> Conditions { get; } = Enum.GetValues(typeof(StampCondition)).Cast<StampCondition>().ToList();
        public bool CanFetchImageFromApi =>
            !string.IsNullOrWhiteSpace(Country) && Year.HasValue && Year > 1865;

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
        private Category selectedCategory;

        [ObservableProperty]
        private Collection selectedCollection;

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

        [ObservableProperty]
        private int collectionId;

        [ObservableProperty] private string faceValue;
        [ObservableProperty] private string stampLocation;
        [ObservableProperty] private DateTime acquisitionDate = DateTime.Now;
        [ObservableProperty] private decimal pricePaid;
        [ObservableProperty] private bool forExchange;
        [ObservableProperty] private string notes;
        [ObservableProperty] private int tagId;

        public AddStampViewModel(IStampService stampService)
        {
            _stampService = stampService;

            Categories = new List<Category>();
            Collections = new List<Collection>();
            SelectedCategory = new Category();
            SelectedCollection = new Collection();
            SelectedStamp = new Stamp();
            Name = string.Empty;
            Country = string.Empty;
            ImagePath = "gallery.png";
            FaceValue = string.Empty;
            StampLocation = string.Empty;
            Notes = string.Empty;

            AcquisitionDate = DateTime.Now;
            Notes = string.Empty;
        }



        partial void OnCountryChanged(string value)
        {
            OnPropertyChanged(nameof(CanFetchImageFromApi));
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
                CollectionId = value.CollectionId;
                FaceValue = value.FaceValue ?? "";
                StampLocation = value.StampLocation ?? string.Empty;
                AcquisitionDate = value.AcquisitionDate;
                PricePaid = value.PricePaid;
                ForExchange = value.ForExchange;
                Notes = value.Notes ?? string.Empty;
                TagId = value.TagId;
                SelectedCategory = Categories?.FirstOrDefault(c => c.Id == value.CategoryId) ?? new Category();
                SelectedCollection = Collections?.FirstOrDefault(c => c.Id == value.CollectionId) ?? new Collection();
            }
            else
            {
                Name = string.Empty;
                Country = string.Empty;
                Year = DateTime.Now.Year;
                Condition = StampCondition.Novo;
                ImagePath = string.Empty;
                CategoryId = 0;
                CollectionId = 0;
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
            if (SelectedCollection == null)
            {
                await Shell.Current.DisplayAlert("Validação", "Selecione Coleção.", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(ImagePath))
            {
                await Shell.Current.DisplayAlert("Validação", "Selecione uma imagem para o selo.", "OK");
                return;
            }

            //if (FaceValue <= 0)
            //{
            //    await Shell.Current.DisplayAlert("Validação", "O valor facial deve ser maior que zero.", "OK");
            //    return;
            //}
            if (AcquisitionDate.Date > DateTime.Now.Date)
            {
                await Shell.Current.DisplayAlert("Validação", "A data de aquisição não pode ser no futuro.", "OK");
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
            bool isCollectionFull = collection.TotalExpected.HasValue && collection.TotalCollected.HasValue &&
                                    collection.TotalCollected.Value >= collection.TotalExpected.Value;

            bool forExchange = false;
            if (isCollectionFull)
            {
                bool createForExchange = await Shell.Current.DisplayAlert(
                    "Coleção completa",
                    "A coleção já está completa. Deseja criar este selo como 'Para Troca'?",
                    "Sim", "Não");

                if (!createForExchange)
                    return;

                forExchange = true;
            }

            var stamp = new Stamp
            {
                Name = Name,
                Country = Country,
                Year = Year.Value,
                Condition = Condition,
                ImagePath = ImagePath,
                CategoryId = CategoryId,
                CollectionId = CollectionId,
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
                //await _collectionService.SaveCollectionAsync(collection);
            }


            await Shell.Current.GoToAsync("..");
        }
        [RelayCommand]
        public async Task FetchImageFromApi()
        {
            var current = Connectivity.NetworkAccess;

            if (current != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Sem Internet", "Verifique a ligação e tente de novo.", "OK");
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
                ImagePath = "gallery.png";
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

        [RelayCommand]
        public async Task LoadWikiStampsAsync()
        {
            WikiStamps.Clear();

            if (string.IsNullOrWhiteSpace(Country) || Year <= 0)
            {
                await Shell.Current.DisplayAlert("Validation", "Please enter both country and year.", "OK");
                return;
            }

            // Build the SPARQL query dynamically, injecting country and year
            string sparqlQuery = $@"
SELECT ?item ?itemLabel ?image WHERE {{
  ?item wdt:P31 wd:Q167578;     # instance of postage stamp
        wdt:P18 ?image;         # has image
        wdt:P17 ?country;       # country of origin
        wdt:P577 ?date.         # issuance date

  ?country rdfs:label ?countryLabel.
  FILTER(LANG(?countryLabel) = 'en')
  FILTER(CONTAINS(LCASE(?countryLabel), '{Country.ToLowerInvariant()}'))

  FILTER(YEAR(?date) = {Year})

  SERVICE wikibase:label {{ bd:serviceParam wikibase:language 'en'. }}
}}
LIMIT 10";

            string url = "https://query.wikidata.org/sparql?query=" +
                         Uri.EscapeDataString(sparqlQuery) + "&format=json";

            using var client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.ParseAdd("StampCollectorApp/1.0");

            try
            {
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    await Shell.Current.DisplayAlert("Error", $"HTTP {response.StatusCode}", "OK");
                    return;
                }

                var json = await response.Content.ReadAsStringAsync();

                using var doc = JsonDocument.Parse(json);

                var bindings = doc.RootElement
                    .GetProperty("results")
                    .GetProperty("bindings");

                if (bindings.GetArrayLength() == 0)
                {
                    await Shell.Current.DisplayAlert("No results", "No stamps found for your search.", "OK");
                    return;
                }

                foreach (var item in bindings.EnumerateArray())
                {
                    string name = item.GetProperty("itemLabel").GetProperty("value").GetString() ?? "Unnamed";
                    string rawImageUrl = item.GetProperty("image").GetProperty("value").GetString() ?? "";
                    string imageUrl = rawImageUrl.Replace("http://", "https://");

                    WikiStamps.Add(new WikiStamps
                    {
                        Name = name,
                        Year = Year.ToString(),
                        ImageUrl = imageUrl
                    });
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        [RelayCommand]
        public async Task LoadStampImagesForCountryAsync()
        {
            if (string.IsNullOrWhiteSpace(Country))
            {
                await Shell.Current.DisplayAlert("Error", "Please select a country.", "OK");
                return;
            }

            var images = await _stampService.GetStampImagesByCountryCategoryAsync(Country, 10);
            WikiStamps.Clear();

            foreach (var url in images)
            {
                WikiStamps.Add(new WikiStamps
                {
                    Name = $"Stamp from {Country}",
                    Year = Year?.ToString() ?? "",
                    ImageUrl = url
                });
            }
        }
    }
}
