using StampCollectorApp.Resources.Languages;

namespace StampCollectorApp.Views;

public partial class DashboardPage : ContentPage
{
    public DashboardPage(DashboardViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        this.Appearing += async (_, __) => await vm.LoadDashboardCommand.ExecuteAsync(null);
    }

    private async void OnToolbarLanguageClicked(object sender, EventArgs e)
    {
        var idiomas = new[] { "en-US", "pt-PT" };
        var escolha = await DisplayActionSheet(
            AppResources.TituloEscolherIdioma,
            AppResources.TituloCancelar,
            null,
            idiomas);

        if (!string.IsNullOrEmpty(escolha) && escolha != AppResources.TituloCancelar)
        {
            Preferences.Set("AppLanguage", escolha);

            var culture = new System.Globalization.CultureInfo(escolha);
            System.Globalization.CultureInfo.DefaultThreadCurrentCulture = culture;
            System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = culture;

            Application.Current.MainPage = new AppShell(); // ou recarregamento conforme sua arquitetura
        }
    }
}