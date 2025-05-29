using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StampCollectorApp.Models;
using StampCollectorApp.Views;
using System.Collections.ObjectModel;

public partial class CollectionsViewModel : ObservableObject
{
    private readonly ICollectionService _collectionService;

    [ObservableProperty] private ObservableCollection<Collection> collections = new();
    [ObservableProperty] private string newCollectionName;

    public CollectionsViewModel(ICollectionService collectionService)
    {
        _collectionService = collectionService;
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
            "Apagar Coleção",
            "Tem a certeza que deseja apagar esta coleção?",
            "Apagar", "Cancelar");
        if (!confirm) return;
        await _collectionService.DeleteCollectionAsync(collection);
        Collections.Remove(collection);
    }
}
