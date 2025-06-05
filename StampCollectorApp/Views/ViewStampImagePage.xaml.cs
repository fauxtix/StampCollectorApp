namespace StampCollectorApp.Views;

public partial class ViewStampImagePage : ContentPage
{
    public ViewStampImagePage(ViewStampImageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}