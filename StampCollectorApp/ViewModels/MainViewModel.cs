using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StampCollectorApp.Models;
using StampCollectorApp.Services;
using System.Collections.ObjectModel;

namespace StampCollectorApp.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private const string PixabayApiKey = "50529772-442b5126c14cb25b021e56dff";
        private const string PixabayApiUrl = "https://pixabay.com/api/";

        private const int PageSize = 4;
        private int _currentPage = 1;
        private List<Stamp> _allFilteredStamps = new();

        public bool CanShowMore => FilteredStamps.Count < _allFilteredStamps.Count;

        [ObservableProperty] private ObservableCollection<Stamp> stamps = new();
        [ObservableProperty] private ObservableCollection<Stamp> filteredStamps = new();
        [ObservableProperty] private string searchQuery;
        [ObservableProperty] private bool showOnlyForExchange;
        [ObservableProperty] private bool isLoading;

        private readonly IStampService _stampService;
        private readonly IDatabaseInitializerService _initService;

        public MainViewModel(IStampService stampService, IDatabaseInitializerService initService)
        {
            _stampService = stampService;
            _initService = initService;
            //_initService.RecreateTablesAsync().Wait(); // Ensure database is initialized
            FilteredStamps.CollectionChanged += (s, e) => OnPropertyChanged(nameof(CanShowMore));
        }

        [RelayCommand]
        public async Task ResetDatabaseAsync()
        {
            bool confirm = await Shell.Current.DisplayAlert("Confirmar", "Deseja apagar e recriar todas as tabelas?", "Sim", "Não");
            if (confirm)
            {
                await _initService.RecreateTablesAsync();
                await Shell.Current.DisplayAlert("Feito", "Base de dados reiniciada.", "OK");
                await LoadStampsAsync();
            }
        }

        [RelayCommand]
        public async Task ClearDataAsync()
        {
            bool confirm = await Shell.Current.DisplayAlert("Confirmar", "Deseja limpar os dadosem todas as tabelas?", "Sim", "Não");
            if (confirm)
            {
                await _initService.ClearDataAsync();
                await Shell.Current.DisplayAlert("Feito", "Base de dados limpa.", "OK");
                await LoadStampsAsync();
            }
        }

        [RelayCommand]
        public async Task LoadStampsAsync()
        {
            IsLoading = true;
            _currentPage = 1;

            // Load all stamps
            var allStamps = await _stampService.GetStampsAsync();
            // Load all countries
            var allCountries = await _stampService.GetCountriesAsync();

            // Create a dictionary for fast lookup
            var countryDict = allCountries.ToDictionary(c => c.Id, c => c.Name);

            // Set CountryName efficiently with O(1) lookups
            foreach (var stamp in allStamps)
            {
                stamp.CountryName = countryDict.TryGetValue(stamp.CountryId, out string name) ? name : "";
            }

            Stamps = new ObservableCollection<Stamp>(allStamps);
            FilterStamps();
            IsLoading = false;
        }
        partial void OnSearchQueryChanged(string value) => FilterStamps();
        partial void OnShowOnlyForExchangeChanged(bool value) => FilterStamps();

        private void FilterStamps()
        {
            _currentPage = 1;
            FilteredStamps.Clear();

            IEnumerable<Stamp> query = Stamps;

            if (ShowOnlyForExchange)
                query = query.Where(s => s.ForExchange);

            _allFilteredStamps = string.IsNullOrWhiteSpace(SearchQuery)
                ? query.ToList()
                : query.Where(s =>
                    (s.Name?.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    s.Year.ToString().Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                    s.Condition.ToString().Replace("_", " ").Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)
                ).ToList();

            foreach (var stamp in _allFilteredStamps.Take(PageSize))
                FilteredStamps.Add(stamp);

            OnPropertyChanged(nameof(CanShowMore));
        }
        [RelayCommand]
        public void ShowMoreAsync()
        {
            if (!CanShowMore) return;
            _currentPage++;
            var nextPage = _allFilteredStamps.Skip((_currentPage - 1) * PageSize).Take(PageSize);
            foreach (var stamp in nextPage)
                FilteredStamps.Add(stamp);
            OnPropertyChanged(nameof(CanShowMore));
        }


        [RelayCommand]
        public async Task AddStampAsync()
        {
            var categories = await _stampService.GetCategoriesAsync();
            var collections = await _stampService.GetCollectionsAsync();
            var countries = await _stampService.GetCountriesAsync();
            var navParams = new Dictionary<string, object>
            {
                { "Categories", categories },
                { "Collections", collections },
                { "Countries", countries}
            };
            await Shell.Current.GoToAsync(nameof(Views.AddStampPage), navParams);
        }

        [RelayCommand]
        public async Task EditStampAsync(Stamp stamp)
        {
            if (stamp == null)
                return;

            var categories = await _stampService.GetCategoriesAsync();
            var collections = await _stampService.GetCollectionsAsync();
            var countries = await _stampService.GetCountriesAsync();

            var navParams = new Dictionary<string, object>
            {
                { "Categories", categories },
                { "Collections", collections },
                { "Countries", countries},
                { "SelectedStamp", stamp }
            };

            await Shell.Current.GoToAsync(nameof(Views.AddStampPage), navParams);
        }

        [RelayCommand]
        public async Task DeleteStampAsync(Stamp stamp)
        {
            if (stamp == null) return;
            bool confirm = await Shell.Current.DisplayAlert(
                "Apagar Selo",
                "Tem a certeza que deseja apagar este selo?",
                "Apagar", "Cancelar");
            if (!confirm) return;

            await _stampService.DeleteStampAsync(stamp);
            Stamps.Remove(stamp);
            FilterStamps();
        }
        [RelayCommand]
        public async Task FetchImageForStamp(Stamp stamp)
        {
            if (stamp == null) return;
            var imageUrl = await GetStampImageUrlAsync($"{stamp.Name} {stamp.Name} stamp");
            if (!string.IsNullOrEmpty(imageUrl))
                stamp.ImagePath = imageUrl;
        }

        [RelayCommand]
        public async Task PickImageForStamp(Stamp stamp)
        {
            if (stamp == null) return;
            try
            {
                var result = await FilePicker.Default.PickAsync(new PickOptions
                {
                    PickerTitle = "Select an image",
                    FileTypes = FilePickerFileType.Images
                });

                if (result != null)
                {
                    stamp.ImagePath = result.FullPath;
                }
            }
            catch
            {
                // Optionally handle exceptions (e.g., user cancels)
            }
        }

        public async Task<string?> GetStampImageUrlAsync(string query)
        {
            using var httpClient = new HttpClient();
            var url = $"{PixabayApiUrl}?key={PixabayApiKey}&q={Uri.EscapeDataString(query)}&image_type=photo&per_page=3";
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
