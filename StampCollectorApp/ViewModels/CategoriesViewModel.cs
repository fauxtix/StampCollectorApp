using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StampCollectorApp.Models;
using StampCollectorApp.Resources.Languages;
using StampCollectorApp.Services;
using StampCollectorApp.Views;
using System.Collections.ObjectModel;

namespace StampCollectorApp.ViewModels;

public partial class CategoriesViewModel : ObservableObject
{
    private readonly ICategoryService _categoryService;
    private readonly IStampService _stampService;

    [ObservableProperty] private ObservableCollection<Category> categories = new();
    [ObservableProperty] private string newCategoryName;

    public CategoriesViewModel(ICategoryService categoryService, IStampService stampService)
    {
        _categoryService = categoryService;
        _stampService = stampService;
    }

    [RelayCommand]
    public async Task LoadCategories()
    {
        var cats = await _categoryService.GetCategoriesAsync();
        Categories.Clear();
        foreach (var cat in cats)
            Categories.Add(cat);
    }

    [RelayCommand]
    private async Task AddCategory()
    {
        await Shell.Current.GoToAsync(nameof(EditCategoryPage));
    }

    [RelayCommand]
    private async Task EditCategory(Category category)
    {
        await Shell.Current.GoToAsync($"{nameof(EditCategoryPage)}", new Dictionary<string, object>
        {
            ["Category"] = category
        });
    }

    [RelayCommand]
    public async Task DeleteCategory(Category category)
    {
        if (category == null) return;

        bool confirm = await Shell.Current.DisplayAlert(
            AppResources.TituloApagarCategoria,
            AppResources.TituloApagarCategoriaCaption,
            AppResources.TituloApagar, AppResources.TituloCancelar);

        if (!confirm)
            return;

        var stamps = await _stampService.GetStampsAsync();
        var toDelete = stamps.Where(s => s.CategoryId == category.Id).ToList();
        if (toDelete.Count > 0)
        {
            foreach (var stamp in toDelete)
                await _stampService.DeleteStampAsync(stamp);
        }
        else
        {
            await _categoryService.DeleteCategoryAsync(category);
            Categories.Remove(category);
        }
    }
}
