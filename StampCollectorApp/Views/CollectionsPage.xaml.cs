namespace StampCollectorApp.Views;

public partial class CollectionsPage : ContentPage
{
    public CollectionsPage(CollectionsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        this.Appearing += async (_, __) => await viewModel.LoadCollectionsCommand.ExecuteAsync(null);
    }
}
