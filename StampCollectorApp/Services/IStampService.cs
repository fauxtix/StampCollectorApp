using StampCollectorApp.Models;

namespace StampCollectorApp.Services;

public interface IStampService
{
    Task<List<Stamp>> GetStampsAsync();
    Task SaveStampAsync(Stamp stamp);
    Task DeleteStampAsync(Stamp stamp);
    Task<List<Category>> GetCategoriesAsync();
    Task<Category?> GetCategoryByIdAsync(int id);
    Task SaveCategoryAsync(Category category);
    Task DeleteCategoryAsync(Category category);
    Task EnsurePixabayCategoriesAsync();
}