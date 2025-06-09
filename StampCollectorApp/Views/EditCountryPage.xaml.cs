namespace StampCollectorApp.Views;

public partial class EditCountryPage : ContentPage
{
    public EditCountryPage(EditCountryViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}