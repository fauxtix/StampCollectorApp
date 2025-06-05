using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

public partial class ViewStampImageViewModel : ObservableObject, IQueryAttributable
{
    [ObservableProperty]
    private string imagePath;

    [RelayCommand]
    private async Task Close()
    {
        await Shell.Current.GoToAsync("..");
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("ImagePath", out var path) && path is string s)
            ImagePath = Uri.UnescapeDataString(s);
    }
}
