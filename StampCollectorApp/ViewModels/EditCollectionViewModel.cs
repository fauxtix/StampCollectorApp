using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StampCollectorApp.Models;
using StampCollectorApp.Resources.Languages;

public partial class EditCollectionViewModel : ObservableObject, IQueryAttributable
{
    private readonly ICollectionService _collectionService;

    [ObservableProperty]
    private Collection collection = new();

    public EditCollectionViewModel(ICollectionService collectionService)
    {
        _collectionService = collectionService;
    }

    [RelayCommand]
    public async Task Save()
    {
        if (string.IsNullOrWhiteSpace(Collection.Name))
        {
            await Shell.Current.DisplayAlert(AppResources.TituloValidacao, AppResources.TituloColecaoVazio, "OK");
            return;
        }

        if (Collection.TotalExpected == 0 && Collection.TotalCollected == 0)
        {
            Collection.TotalExpected = 1000;
            Collection.TotalCollected = 0;
        }

        //if (Collection.TotalExpected != 1000 || Collection.TotalCollected > 0)
        //{
        //    if ((Collection.TotalExpected > 0 && Collection.TotalCollected <= 1) || Collection.TotalExpected == 0 && Collection.TotalCollected > 0)
        //    {
        //        await Shell.Current.DisplayAlert("Validação", "Um dos Totais não está preenchido", "OK");
        //        return;
        //    }
        //}
        if (Collection.TotalExpected < 0)
        {
            await Shell.Current.DisplayAlert(AppResources.TituloValidacao, AppResources.TituloValidacaoTotalEsperado, "OK");
            return;
        }
        else if (Collection.TotalCollected > 0 && Collection.TotalCollected > Collection.TotalExpected)
        {
            await Shell.Current.DisplayAlert(AppResources.TituloValidacao, AppResources.TituloValidacaoColecionado, "OK");
            return;
        }

        await _collectionService.SaveCollectionAsync(Collection);
        await Shell.Current.GoToAsync("..");
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("Collection", out var obj) && obj is Collection col)
            Collection = col;
        else
            Collection = new Collection();
    }
}
