namespace StampCollectorApp.Views;

public partial class DashboardPage : ContentPage
{
    public DashboardPage(DashboardViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        this.Appearing += async (_, __) => await vm.LoadDashboardCommand.ExecuteAsync(null);
    }
}