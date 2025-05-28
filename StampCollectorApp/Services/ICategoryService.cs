using StampCollectorApp.Models;

public interface ICategoryService
{
    Task<List<Category>> GetCategoriesAsync();
    Task SaveCategoryAsync(Category category);
    Task DeleteCategoryAsync(Category category);
    Task<bool> CategoryNameExistsAsync(string name, int excludeId = 0);
}