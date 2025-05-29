using StampCollectorApp.Views;

namespace StampCollectorApp;

public partial class AppShell : Shell
{
    private static bool _categoriesSeeded = false;
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(AddStampPage), typeof(AddStampPage));
        Routing.RegisterRoute(nameof(EditCategoryPage), typeof(EditCategoryPage));
        Routing.RegisterRoute(nameof(CollectionsPage), typeof(CollectionsPage));
        Routing.RegisterRoute(nameof(EditCollectionPage), typeof(EditCollectionPage));
    }
}
