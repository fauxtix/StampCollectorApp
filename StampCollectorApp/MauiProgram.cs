﻿using CommunityToolkit.Maui;
using SQLite;
using StampCollectorApp.Services;
using StampCollectorApp.ViewModels;
using StampCollectorApp.Views;

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

        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "stamps.db3");

        // SQLite connection shared by services
        var sqliteConn = new SQLiteAsyncConnection(dbPath);

        builder.Services.AddSingleton(sqliteConn);

        builder.Services.AddSingleton<IStampService>(sp =>
            new StampService(dbPath));

        builder.Services.AddSingleton<ICategoryService, CategoryService>();
        builder.Services.AddSingleton<ICollectionService, CollectionService>();

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

        builder.Services.AddTransient<CollectionsPage>();
        builder.Services.AddTransient<CollectionsViewModel>();
        builder.Services.AddTransient<EditCollectionViewModel>();
        builder.Services.AddTransient<EditCollectionPage>();


        builder.Services.AddSingleton<IDatabaseInitializerService, DatabaseInitializerService>();
        return builder.Build();


    }
}
