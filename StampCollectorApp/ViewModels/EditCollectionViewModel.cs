using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StampCollectorApp.Models;

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
            await Shell.Current.DisplayAlert("Validação", "O nome da coleção não pode estar vazio.", "OK");
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
