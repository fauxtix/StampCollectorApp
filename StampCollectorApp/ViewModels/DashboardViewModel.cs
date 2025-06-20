using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StampCollectorApp.Models;
using StampCollectorApp.Services;
using System.Collections.ObjectModel;

public partial class DashboardViewModel : ObservableObject
{
    private readonly IStampService _stampService;
    private readonly ICollectionService _collectionService;
    private readonly ICategoryService _categoryService;
    private readonly IExchangeService _exchangeService;
    private readonly ICountryService _countryService;

    [ObservableProperty] private int totalStamps;
    [ObservableProperty] private int totalCollections;
    [ObservableProperty] private int totalCategories;
    [ObservableProperty] private int stampsForExchange;
    [ObservableProperty] private int totalExchanges;

    // Filtros
    [ObservableProperty] private Category selectedCategory;
    [ObservableProperty] private Collection selectedCollection;
    [ObservableProperty] private Country selectedCountry;
    [ObservableProperty] private string searchText;

    public bool IsCategoryPickerEnabled => SelectedCollection == null && SelectedCountry == null && string.IsNullOrWhiteSpace(SearchText);
    public bool IsCollectionPickerEnabled => SelectedCategory == null && SelectedCountry == null && string.IsNullOrWhiteSpace(SearchText);
    public bool IsCountryPickerEnabled => SelectedCategory == null && SelectedCollection == null && string.IsNullOrWhiteSpace(SearchText);
    public bool IsSearchBarEnabled => SelectedCategory == null && SelectedCollection == null && SelectedCountry == null;

    // Gráficos
    public ObservableCollection<ChartData> StampsByCategory { get; } = new();
    public ObservableCollection<ChartData> StampsByCollection { get; } = new();
    public ObservableCollection<ChartData> StampsByCountry { get; } = new();
    public ObservableCollection<ChartData> ExchangesPerMonth { get; } = new();

    // Listas para pickers
    public ObservableCollection<Category> Categories { get; } = new();
    public ObservableCollection<Collection> Collections { get; } = new();
    public ObservableCollection<Country> Countries { get; } = new();

    public DashboardViewModel(
        IStampService stampService,
        ICollectionService collectionService,
        ICategoryService categoryService,
        IExchangeService exchangeService,
        ICountryService countryService)
    {
        _stampService = stampService;
        _collectionService = collectionService;
        _categoryService = categoryService;
        _exchangeService = exchangeService;
        _countryService = countryService;
    }

    [RelayCommand]
    public async Task LoadDashboardAsync()
    {
        // Carrega filtros
        Categories.Clear();
        foreach (var cat in await _categoryService.GetCategoriesAsync()) Categories.Add(cat);

        Collections.Clear();
        foreach (var col in await _collectionService.GetCollectionsAsync()) Collections.Add(col);

        Countries.Clear();
        foreach (var c in await _countryService.GetCountriesAsync()) Countries.Add(c);

        var stamps = await _stampService.GetStampsAsync();
        TotalStamps = stamps.Count;
        StampsForExchange = stamps.Count(x => x.ForExchange);

        StampsByCategory.Clear();
        var catGroups = stamps.GroupBy(s => s.CategoryId)
            .Select(g => new ChartData
            {
                Label = Categories.FirstOrDefault(c => c.Id == g.Key)?.Name ?? "Sem Categoria",
                Value = g.Count()
            })
            .OrderByDescending(x => x.Value);
        foreach (var item in catGroups) StampsByCategory.Add(item);

        StampsByCollection.Clear();
        var colGroups = stamps.GroupBy(s => s.CollectionId)
            .Select(g => new ChartData
            {
                Label = Collections.FirstOrDefault(c => c.Id == g.Key)?.Name ?? "Sem Coleção",
                Value = g.Count()
            })
            .OrderByDescending(x => x.Value);
        foreach (var item in colGroups) StampsByCollection.Add(item);

        StampsByCountry.Clear();
        var countryGroups = stamps.GroupBy(s => s.CountryId)
            .Select(g => new ChartData
            {
                Label = Countries.FirstOrDefault(c => c.Id == g.Key)?.Name ?? "Sem País",
                Value = g.Count()
            })
            .OrderByDescending(x => x.Value);
        foreach (var item in countryGroups) StampsByCountry.Add(item);

        var exchs = await _exchangeService.GetAllAsync();
        TotalExchanges = exchs.Count;

        ExchangesPerMonth.Clear();
        var now = DateTime.Now;
        var months = Enumerable.Range(0, 12)
            .Select(i => now.AddMonths(-11 + i))
            .ToList();

        var exchangesByMonth = months
            .Select(m => new ChartData
            {
                Label = $"{m.Month:00}/{m.Year}",
                Value = exchs.Count(e => e.ExchangeDate.Year == m.Year && e.ExchangeDate.Month == m.Month)
            });
        foreach (var item in exchangesByMonth) ExchangesPerMonth.Add(item);

        TotalCollections = Collections.Count;
        TotalCategories = Categories.Count;
    }

    [RelayCommand]
    public async Task ApplyFiltersAsync()
    {
        var stamps = await _stampService.GetStampsAsync();

        if (SelectedCategory != null)
            stamps = stamps.Where(s => s.CategoryId == SelectedCategory.Id).ToList();
        else if (SelectedCollection != null)
            stamps = stamps.Where(s => s.CollectionId == SelectedCollection.Id).ToList();
        else if (SelectedCountry != null)
            stamps = stamps.Where(s => s.CountryId == SelectedCountry.Id).ToList();
        else if (!string.IsNullOrWhiteSpace(SearchText))
            stamps = stamps.Where(s => s.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase)).ToList();

        StampsByCategory.Clear();
        var catGroups = stamps.GroupBy(s => s.CategoryId)
            .Select(g => new ChartData
            {
                Label = Categories.FirstOrDefault(c => c.Id == g.Key)?.Name ?? "Sem Categoria",
                Value = g.Count()
            })
            .OrderByDescending(x => x.Value);
        foreach (var item in catGroups) StampsByCategory.Add(item);

        StampsByCollection.Clear();
        var colGroups = stamps.GroupBy(s => s.CollectionId)
            .Select(g => new ChartData
            {
                Label = Collections.FirstOrDefault(c => c.Id == g.Key)?.Name ?? "Sem Coleção",
                Value = g.Count()
            })
            .OrderByDescending(x => x.Value);
        foreach (var item in colGroups) StampsByCollection.Add(item);

        StampsByCountry.Clear();
        var countryGroups = stamps.GroupBy(s => s.CountryId)
            .Select(g => new ChartData
            {
                Label = Countries.FirstOrDefault(c => c.Id == g.Key)?.Name ?? "Sem País",
                Value = g.Count()
            })
            .OrderByDescending(x => x.Value);
        foreach (var item in countryGroups) StampsByCountry.Add(item);
    }

    partial void OnSelectedCategoryChanged(Category value)
    {
        OnPropertyChanged(nameof(IsCollectionPickerEnabled));
        OnPropertyChanged(nameof(IsCountryPickerEnabled));
        OnPropertyChanged(nameof(IsSearchBarEnabled));
    }
    partial void OnSelectedCollectionChanged(Collection value)
    {
        OnPropertyChanged(nameof(IsCategoryPickerEnabled));
        OnPropertyChanged(nameof(IsCountryPickerEnabled));
        OnPropertyChanged(nameof(IsSearchBarEnabled));
    }
    partial void OnSelectedCountryChanged(Country value)
    {
        OnPropertyChanged(nameof(IsCategoryPickerEnabled));
        OnPropertyChanged(nameof(IsCollectionPickerEnabled));
        OnPropertyChanged(nameof(IsSearchBarEnabled));
    }
    partial void OnSearchTextChanged(string value)
    {
        OnPropertyChanged(nameof(IsCategoryPickerEnabled));
        OnPropertyChanged(nameof(IsCollectionPickerEnabled));
        OnPropertyChanged(nameof(IsCountryPickerEnabled));
    }

    [RelayCommand]
    public void ClearFilters()
    {
        SelectedCategory = null;
        SelectedCollection = null;
        SelectedCountry = null;
        SearchText = string.Empty;
    }
}
