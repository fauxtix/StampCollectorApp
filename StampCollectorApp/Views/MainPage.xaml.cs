using StampCollectorApp.Models;
using StampCollectorApp.ViewModels;

namespace StampCollectorApp.Views;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        this.Appearing += async (_, __) => await viewModel.LoadStampsCommand.ExecuteAsync(null);
    }

    private async void OnStampSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Stamp selectedStamp)
        {
            var vm = BindingContext as MainViewModel;
            await vm.EditStampCommand.ExecuteAsync(selectedStamp);
            ((CollectionView)sender).SelectedItem = null; // clear selection
        }
    }
}
