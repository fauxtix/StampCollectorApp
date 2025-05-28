using SQLite;
using StampCollectorApp.Models;

public class DatabaseInitializerService
{
    private readonly SQLiteAsyncConnection _db;

    public DatabaseInitializerService(SQLiteAsyncConnection db)
    {
        _db = db;
    }

    public async Task InitializeAsync()
    {
        await _db.CreateTableAsync<Collection>();
        await _db.CreateTableAsync<Tag>();

        // Verifica se a coluna CollectionId existe na tabela Stamps
        var columns = await _db.QueryAsync<TableInfo>("PRAGMA table_info(Stamps)");
        if (!columns.Any(c => c.name == "CollectionId"))
        {
            await _db.ExecuteAsync("ALTER TABLE Stamps ADD COLUMN CollectionId INTEGER");
        }

        // Verifica se a coluna TagId existe na tabela Stamps
        if (!columns.Any(c => c.name == "TagId"))
        {
            await _db.ExecuteAsync("ALTER TABLE Stamps ADD COLUMN TagId INTEGER");
        }
    }
}