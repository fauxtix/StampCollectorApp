using CommunityToolkit.Maui;
using SQLite;
using StampCollectorApp.Services;
using StampCollectorApp.ViewModels;
using StampCollectorApp.Views;
using Syncfusion.Maui.Core.Hosting;


namespace StampCollectorApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.ConfigureSyncfusionCore();
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzkxMjE3N0AzMjM5MmUzMDJlMzAzYjMyMzkzYlhTeVd0b0xmc2tHeWxhT21Db3lXK2NrZzc4bjFjSm9RWE0rMGVOTnluNms9");

        builder.Services.AddLocalization(options => options.ResourcesPath = "Resources/Languages");
        var savedCulture = Preferences.Get("AppLanguage", null);
        if (string.IsNullOrEmpty(savedCulture))
        {
            savedCulture = System.Globalization.CultureInfo.CurrentCulture.Name;
            Preferences.Set("AppLanguage", savedCulture);
        }
        var culture = new System.Globalization.CultureInfo(savedCulture);
        System.Globalization.CultureInfo.DefaultThreadCurrentCulture = culture;
        System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = culture;

        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "stamps.db3");

        // SQLite connection shared by services
        var sqliteConn = new SQLiteAsyncConnection(dbPath);

        builder.Services.AddSingleton(sqliteConn);

        builder.Services.AddSingleton<IStampService>(sp =>
            new StampService(dbPath));
        builder.Services.AddSingleton<ICategoryService, CategoryService>();
        builder.Services.AddSingleton<ICollectionService, CollectionService>();
        builder.Services.AddSingleton<ICountryService, CountryService>();
        builder.Services.AddSingleton<IExchangeService, ExchangeService>();


        builder.Services.AddTransient<MainViewModel>();
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<AddStampViewModel>();
        builder.Services.AddTransient<AddStampPage>();
        builder.Services.AddTransient<ViewStampImageViewModel>();
        builder.Services.AddTransient<ViewStampImagePage>();

        builder.Services.AddTransient<CategoriesPage>();
        builder.Services.AddTransient<CategoriesViewModel>();
        builder.Services.AddTransient<EditCategoryViewModel>();
        builder.Services.AddTransient<EditCategoryPage>();

        builder.Services.AddTransient<CountriesPage>();
        builder.Services.AddTransient<CountriesViewModel>();
        builder.Services.AddTransient<EditCountryViewModel>();
        builder.Services.AddTransient<EditCountryPage>();

        builder.Services.AddTransient<CollectionsPage>();
        builder.Services.AddTransient<CollectionsViewModel>();
        builder.Services.AddTransient<EditCollectionViewModel>();
        builder.Services.AddTransient<EditCollectionPage>();

        builder.Services.AddTransient<ExchangePage>();
        builder.Services.AddTransient<ExchangeViewModel>();

        builder.Services.AddTransient<DashboardViewModel>();
        builder.Services.AddTransient<DashboardPage>();

        builder.Services.AddSingleton<IDatabaseInitializerService, DatabaseInitializerService>();
        var app = builder.Build();
        return app;
    }
}
