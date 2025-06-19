using StampCollectorApp.Resources.Languages;
using StampCollectorApp.Services;
using StampCollectorApp.Views;

namespace StampCollectorApp;

public partial class AppShell : Shell
{
    private readonly IStampService _stampService;

    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(AddStampPage), typeof(AddStampPage));
        Routing.RegisterRoute(nameof(EditCategoryPage), typeof(EditCategoryPage));
        Routing.RegisterRoute(nameof(EditCountryPage), typeof(EditCountryPage));
        Routing.RegisterRoute(nameof(CollectionsPage), typeof(CollectionsPage));
        Routing.RegisterRoute(nameof(EditCollectionPage), typeof(EditCollectionPage));
        Routing.RegisterRoute(nameof(ViewStampImagePage), typeof(ViewStampImagePage));
        Routing.RegisterRoute(nameof(ExchangePage), typeof(ExchangePage));
        Routing.RegisterRoute(nameof(DashboardPage), typeof(DashboardPage));

        _stampService = App.Services.GetService<IStampService>();
        this.Navigating += OnShellNavigating;
    }

    private async void OnShellNavigating(object sender, ShellNavigatingEventArgs e)
    {
        // Only intercept navigation to ExchangePage
        if (e.Target.Location.OriginalString.Contains(nameof(ExchangePage), StringComparison.OrdinalIgnoreCase))
        {
            if (_stampService != null && !await _stampService.AnyStampsForExchangeAsync())
            {
                e.Cancel(); // Stop navigating to ExchangePage

                // Show alert
                await Current.DisplayAlert(
                    AppResources.TituloAlerta,
                    AppResources.TituloSemSelosParaTroca,
                    "OK"
                );

                // Immediately redirect to a safe, neutral page (e.g., MainPage or Home)
                // Replace "MainPage" with your actual main/root route if different
                await Task.Delay(1); // Ensure alert is shown before navigating
                await GoToAsync("//MainPage");
            }
        }
    }
}