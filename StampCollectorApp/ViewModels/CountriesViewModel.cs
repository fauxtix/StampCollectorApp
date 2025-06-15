using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StampCollectorApp.Models;
using StampCollectorApp.Resources.Languages;
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
            AppResources.TituloApagarPais,
            AppResources.TituloApagarPaisCaption,
            AppResources.TituloApagar, AppResources.TituloCancelar);

        if (!confirm)
            return;

        var stamps = await _stampService.GetStampsAsync();
        var toDelete = stamps.Where(s => s.CountryId == country.Id).ToList();
        if (toDelete.Count > 0)
        {
            foreach (var stamp in toDelete)
                await _stampService.DeleteStampAsync(stamp);
        }
        else
        {
            await _countryService.DeleteCountryAsync(country);
            Countries.Remove(country);
        }
    }
}
