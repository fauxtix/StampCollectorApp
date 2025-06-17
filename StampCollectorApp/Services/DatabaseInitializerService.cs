using SQLite;
using StampCollectorApp.Models;

public class DatabaseInitializerService : IDatabaseInitializerService
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

    public async Task RecreateTablesAsync()
    {
        //// Apagar na ordem inversa das dependências
        //await _db.DropTableAsync<StampCollection>();
        ////await _db.DropTableAsync<Category>();
        //await _db.DropTableAsync<Country>();
        //await _db.DropTableAsync<Stamp>();
        //await _db.DropTableAsync<Collection>();
        //await _db.DropTableAsync<Tag>();
        try
        {
            // Criar na ordem correta
            //await _db.CreateTableAsync<Tag>();
            //await _db.CreateTableAsync<Stamp>();
            await _db.CreateTableAsync<StampExchange>();
            //await _db.CreateTableAsync<Category>();
            //await _db.CreateTableAsync<Country>();
            //await _db.CreateTableAsync<Collection>();
            //await _db.CreateTableAsync<StampCollection>();
        }
        catch (Exception ex)
        {
            var x = ex.Message;

            throw;
        }
    }
    public async Task ClearDataAsync()
    {
        await _db.DeleteAllAsync<Stamp>();
        await _db.DeleteAllAsync<Collection>();
        await _db.DeleteAllAsync<StampCollection>();
        await _db.DeleteAllAsync<Tag>();
    }
}