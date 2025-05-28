namespace StampCollectorApp.Views;

public partial class EditCategoryPage : ContentPage
{
    public EditCategoryPage(EditCategoryViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
