using SQLite;
using StampCollectorApp.Models;

public class CollectionService : ICollectionService
{
    private readonly SQLiteAsyncConnection _db;

    public CollectionService(SQLiteAsyncConnection db)
    {
        _db = db;
    }

    public async Task<List<Collection>> GetCollectionsAsync()
    {
        var result = await _db.Table<Collection>().OrderBy(c => c.Name).ToListAsync();
        return result;
    }

    public async Task SaveCollectionAsync(Collection collection)
    {
        if (collection.Id == 0)
            await _db.InsertAsync(collection);
        else
            await _db.UpdateAsync(collection);
    }
    public async Task DeleteCollectionAsync(Collection collection) => await _db.DeleteAsync(collection);

    public async Task<bool> CollectionNameExistsAsync(string name, int excludeId = 0)
    {
        var existing = await _db.Table<Collection>()
            .Where(c => c.Name.ToLower() == name.ToLower() && c.Id != excludeId)
            .FirstOrDefaultAsync();
        return existing != null;
    }

}
