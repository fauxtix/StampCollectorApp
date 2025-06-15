using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StampCollectorApp.Models;
using StampCollectorApp.Resources.Languages;
using StampCollectorApp.Services;
using StampCollectorApp.Views;
using System.Collections.ObjectModel;


public partial class CollectionsViewModel : ObservableObject
{
    private readonly ICollectionService _collectionService;
    private readonly IStampService _stampService;


    [ObservableProperty] private ObservableCollection<Collection> collections = new();
    [ObservableProperty] private string newCollectionName = string.Empty; // Fix for CS8618

    public CollectionsViewModel(ICollectionService collectionService, IStampService stampService)
    {
        _collectionService = collectionService; // Fix for IDE0290
        _stampService = stampService;
    }

    [RelayCommand]
    public async Task LoadCollections()
    {
        var list = await _collectionService.GetCollectionsAsync();
        Collections.Clear();
        foreach (var col in list)
            Collections.Add(col);
    }

    [RelayCommand]
    public async Task AddCollection()
    {
        await Shell.Current.GoToAsync(nameof(EditCollectionPage));
    }

    [RelayCommand]
    public async Task EditCollection(Collection collection)
    {
        await Shell.Current.GoToAsync($"{nameof(EditCollectionPage)}", new Dictionary<string, object>
        {
            ["Collection"] = collection
        });
    }

    [RelayCommand]
    public async Task DeleteCollection(Collection collection)
    {
        if (collection == null) return;
        bool confirm = await Shell.Current.DisplayAlert(
            AppResources.TituloApagarColecao,
            AppResources.TituloApagarColecaoCaption,
            AppResources.TituloApagar, AppResources.TituloCancelar);

        if (!confirm) return;

        var stamps = await _stampService.GetStampsAsync();
        var toDelete = stamps.Where(s => s.CollectionId == collection.Id).ToList();

        if (toDelete.Count > 0)
        {
            foreach (var stamp in toDelete)
            {
                await _stampService.DeleteStampAsync(stamp);
            }
        }
        else
        {
            await _collectionService.DeleteCollectionAsync(collection);
            Collections.Remove(collection);
        }
    }
}
