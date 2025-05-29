using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StampCollectorApp.Models;
using StampCollectorApp.Services;
using System.Collections.ObjectModel;

namespace StampCollectorApp.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IStampService _stampService;

        private const string PixabayApiKey = "50529772-442b5126c14cb25b021e56dff"; // Replace with your real key
        private const string PixabayApiUrl = "https://pixabay.com/api/";

        [ObservableProperty]
        private ObservableCollection<Stamp> stamps = new();

        [ObservableProperty]
        private ObservableCollection<Stamp> filteredStamps = new();

        [ObservableProperty]
        private string searchQuery;

        public MainViewModel(IStampService stampService)
        {
            _stampService = stampService;
        }

        [RelayCommand]
        public async Task LoadStamps()
        {
            await _stampService.EnsurePixabayCategoriesAsync();

            var allStamps = await _stampService.GetStampsAsync();
            Stamps.Clear();
            foreach (var stamp in allStamps)
                Stamps.Add(stamp);

            FilterStamps();
        }

        partial void OnSearchQueryChanged(string value)
        {
            FilterStamps();
        }

        private void FilterStamps()
        {
            FilteredStamps.Clear();

            var filtered = string.IsNullOrWhiteSpace(SearchQuery)
                ? Stamps
                : new ObservableCollection<Stamp>(
                    Stamps.Where(s =>
                        (s.Name?.Contains(SearchQuery, System.StringComparison.OrdinalIgnoreCase) ?? false) ||
                        (s.Country?.Contains(SearchQuery, System.StringComparison.OrdinalIgnoreCase) ?? false)));

            foreach (var stamp in filtered)
                FilteredStamps.Add(stamp);
        }

        [RelayCommand]
        public async Task AddStamp()
        {
            var categories = await _stampService.GetCategoriesAsync();
            var collections = await _stampService.GetCollectionsAsync();
            var navParams = new Dictionary<string, object>
            {
                { "Categories", categories },
                { "Collections", collections }
            };
            await Shell.Current.GoToAsync(nameof(Views.AddStampPage), navParams);
        }

        [RelayCommand]
        public async Task EditStamp(Stamp stamp)
        {
            if (stamp == null)
                return;

            var categories = await _stampService.GetCategoriesAsync();
            var collections = await _stampService.GetCollectionsAsync();

            var navParams = new Dictionary<string, object>
            {
                { "Categories", categories },
                { "Collections", collections },
                { "SelectedStamp", stamp }
            };

            await Shell.Current.GoToAsync(nameof(Views.AddStampPage), navParams);
        }

        [RelayCommand]
        public async Task DeleteStamp(Stamp stamp)
        {
            if (stamp == null)
                return;

            await _stampService.DeleteStampAsync(stamp);
            Stamps.Remove(stamp);
            FilterStamps();
        }

        [RelayCommand]
        public async Task FetchImageForStamp(Stamp stamp)
        {
            if (stamp == null) return;
            var imageUrl = await GetStampImageUrlAsync($"{stamp.Name} {stamp.Country} stamp");
            if (!string.IsNullOrEmpty(imageUrl))
                stamp.ImagePath = imageUrl;
        }

        // Optional: Allow picking an image from device for a stamp in the main list
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
