using StampCollectorApp.ViewModels;

namespace StampCollectorApp.Views;

public partial class CategoriesPage : ContentPage
{
    public CategoriesViewModel ViewModel => BindingContext as CategoriesViewModel;

    public CategoriesPage(CategoriesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        this.Appearing += async (_, __) => await viewModel.LoadCategoriesCommand.ExecuteAsync(null);
    }
}
