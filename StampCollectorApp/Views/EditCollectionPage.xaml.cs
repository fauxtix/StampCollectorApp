namespace StampCollectorApp.Views;

public partial class EditCollectionPage : ContentPage
{
    public EditCollectionPage(EditCollectionViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
