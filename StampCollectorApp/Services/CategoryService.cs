using SQLite;
using StampCollectorApp.Models;

namespace StampCollectorApp.Services;

public class CategoryService : ICategoryService
{
    private readonly SQLiteAsyncConnection _database;

    public CategoryService(SQLiteAsyncConnection database)
    {
        _database = database;
    }

    public Task<List<Category>> GetCategoriesAsync() =>
        _database.Table<Category>().OrderBy(c => c.Name).ToListAsync();

    public Task SaveCategoryAsync(Category category) => _database.InsertOrReplaceAsync(category);

    public Task DeleteCategoryAsync(Category category) => _database.DeleteAsync(category);

    public async Task<bool> CategoryNameExistsAsync(string name, int excludeId = 0)
    {
        var existing = await _database.Table<Category>()
            .Where(c => c.Name.ToLower() == name.ToLower() && c.Id != excludeId)
            .FirstOrDefaultAsync();
        return existing != null;
    }
}
