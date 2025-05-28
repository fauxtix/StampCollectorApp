using SQLite;
using StampCollectorApp.Models;

namespace StampCollectorApp.Services;


public class TagService : ITagService
{
    private readonly SQLiteAsyncConnection _database;

    public TagService(SQLiteAsyncConnection database)
    {
        _database = database;
    }

    public Task<List<Tag>> GetTagsAsync() =>
        _database.Table<Tag>().OrderBy(c => c.Name).ToListAsync();

    public Task SaveTagAsync(Tag Tag) => _database.InsertOrReplaceAsync(Tag);

    public Task DeleteTagAsync(Tag Tag) => _database.DeleteAsync(Tag);

    public async Task<bool> TagNameExistsAsync(string name, int excludeId = 0)
    {
        var existing = await _database.Table<Tag>()
            .Where(c => c.Name.ToLower() == name.ToLower() && c.Id != excludeId)
            .FirstOrDefaultAsync();
        return existing != null;
    }
}
