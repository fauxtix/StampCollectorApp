﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StampCollectorApp.Models;
using StampCollectorApp.Resources.Languages;
using StampCollectorApp.Services;

public partial class EditCountryViewModel : ObservableObject, IQueryAttributable
{
    private readonly ICountryService _countryService;
    [ObservableProperty]
    private Country country = new();
    public bool IsEditMode => Country != null && Country.Id > 0;

    public EditCountryViewModel(ICountryService countryService, Country? country = null)
    {
        _countryService = countryService;
        Country = country ?? new Country();
    }

    [RelayCommand]
    private async Task Save()
    {
        if (string.IsNullOrWhiteSpace(Country.Name))
        {
            await Shell.Current.DisplayAlert(AppResources.TituloValidacao, AppResources.TituloPaisVazio, "OK");
            return;
        }

        if (await _countryService.CountryNameExistsAsync(Country.Name, Country.Id))
        {
            await Shell.Current.DisplayAlert(AppResources.TituloValidacao, AppResources.TituloPaisJaExiste, "OK");
            return;
        }

        await _countryService.SaveCountryAsync(Country);
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task Delete()
    {
        if (IsEditMode)
        {
            await _countryService.DeleteCountryAsync(Country);
            await Shell.Current.GoToAsync("..");
        }
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("Country", out var countryObj) && countryObj is Country ctry)
        {
            Country = ctry;
        }
        else
        {
            Country = new();
        }
    }
}
