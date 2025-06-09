using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StampCollectorApp.Models;
using StampCollectorApp.Services;
using StampCollectorApp.Views;
using System.Collections.ObjectModel;

namespace StampCollectorApp.ViewModels;

public partial class CountriesViewModel : ObservableObject
{
    private readonly ICountryService _countryService;
    private readonly IStampService _stampService;


    [ObservableProperty] private ObservableCollection<Country> countries = new();
    [ObservableProperty] private string newCountryName;

    public CountriesViewModel(ICountryService countryService, IStampService stampService)
    {
        _countryService = countryService;
        _stampService = stampService;
    }

    [RelayCommand]
    public async Task LoadCountries()
    {
        var data = await _countryService.GetCountriesAsync();
        Countries.Clear();
        foreach (var item in data)
            Countries.Add(item);
    }

    [RelayCommand]
    private async Task AddCountry()
    {
        await Shell.Current.GoToAsync(nameof(EditCountryPage));
    }

    [RelayCommand]
    private async Task EditCountry(Country country)
    {
        await Shell.Current.GoToAsync($"{nameof(EditCountryPage)}", new Dictionary<string, object>
        {
            ["Country"] = country
        });
    }

    [RelayCommand]
    public async Task DeleteCountry(Country country)
    {
        if (country == null) return;

        bool confirm = await Shell.Current.DisplayAlert(
            "Apagar País",
            "Apagando este País, também apagará todos os selos da do mesmo. Tem a certeza?",
            "Apagar", "Cancelar");

        if (!confirm)
            return;

        var stamps = await _stampService.GetStampsAsync();
        var toDelete = stamps.Where(s => s.CategoryId == country.Id).ToList();
        foreach (var stamp in toDelete)
            await _stampService.DeleteStampAsync(stamp);

        await _countryService.DeleteCountryAsync(country);
        Countries.Remove(country);
    }
}
