using StampCollectorApp.ViewModels;

namespace StampCollectorApp.Views;

public partial class ExchangePage : ContentPage
{
    public ExchangePage(ExchangeViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is ExchangeViewModel vm)
        {
            vm.LoadStampsForExchangeCommand.Execute(null);
            vm.LoadExchangesCommand.Execute(null);
        }
    }
}