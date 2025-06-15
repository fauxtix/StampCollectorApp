using StampCollectorApp.Models;
using StampCollectorApp.Resources.Languages;
using StampCollectorApp.ViewModels;

namespace StampCollectorApp.Views;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        this.Appearing += async (_, __) => await viewModel.LoadStampsCommand.ExecuteAsync(null);
    }

    private async void OnStampSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Stamp selectedStamp)
        {
            var vm = BindingContext as MainViewModel;
            await vm.EditStampCommand.ExecuteAsync(selectedStamp);
            ((CollectionView)sender).SelectedItem = null; // clear selection
        }
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
