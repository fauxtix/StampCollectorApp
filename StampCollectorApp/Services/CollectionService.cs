using SQLite;
using StampCollectorApp.Models;

namespace StampCollectorApp.Services;


public class CollectionService : ICollectionService
{
    private readonly SQLiteAsyncConnection _database;

    public CollectionService(SQLiteAsyncConnection database)
    {
        _database = database;
    }

    public Task<List<Collection>> GetCollectionsAsync() =>
        _database.Table<Collection>().OrderBy(c => c.Name).ToListAsync();

    public Task SaveCollectionAsync(Collection Collection) => _database.InsertOrReplaceAsync(Collection);

    public Task DeleteCollectionAsync(Collection Collection) => _database.DeleteAsync(Collection);

    public async Task<bool> CollectionNameExistsAsync(string name, int excludeId = 0)
    {
        var existing = await _database.Table<Collection>()
            .Where(c => c.Name.ToLower() == name.ToLower() && c.Id != excludeId)
            .FirstOrDefaultAsync();
        return existing != null;
    }
}
