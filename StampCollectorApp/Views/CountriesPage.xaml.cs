using StampCollectorApp.ViewModels;

namespace StampCollectorApp.Views;

public partial class CountriesPage : ContentPage
{
    public CountriesViewModel ViewModel => BindingContext as CountriesViewModel;

    public CountriesPage(CountriesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        this.Appearing += async (_, __) => await viewModel.LoadCountriesCommand.ExecuteAsync(null);
    }
}
