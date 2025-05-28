using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StampCollectorApp.Models;
using StampCollectorApp.Services;

public partial class EditCategoryViewModel : ObservableObject, IQueryAttributable
{
    private readonly IStampService _stampService;
    private readonly ICategoryService _categoryService;
    [ObservableProperty]
    private Category category = new();
    public bool IsEditMode => Category != null && Category.Id > 0;

    public EditCategoryViewModel(IStampService stampService, ICategoryService categoryService, Category? category = null)
    {
        _stampService = stampService;
        _categoryService = categoryService;
        Category = category ?? new Category();
    }
    [RelayCommand]
    private async Task Save()
    {
        if (string.IsNullOrWhiteSpace(Category.Name))
        {
            await Shell.Current.DisplayAlert("Validação", "Preencha Categoria, p.f.", "OK");
            return;
        }

        // Unique name validation
        if (await _categoryService.CategoryNameExistsAsync(Category.Name, Category.Id))
        {
            await Shell.Current.DisplayAlert("Validação", "Categoria já existe.", "OK");
            return;
        }
        // Save the category
        await _stampService.SaveCategoryAsync(Category);
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task Delete()
    {
        if (IsEditMode)
        {
            await _stampService.DeleteCategoryAsync(Category);
            await Shell.Current.GoToAsync("..");
        }
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("Category", out var categoryObj) && categoryObj is Category cat)
        {
            Category = cat;
        }
        else
        {
            Category = new Category();
        }
    }
}
