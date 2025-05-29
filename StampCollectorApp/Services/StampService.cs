using SQLite;
using StampCollectorApp.Models;

namespace StampCollectorApp.Services
{
    public class StampService : IStampService
    {
        private readonly SQLiteAsyncConnection _db;

        private static readonly string[] PixabayCategories = new[]
{
    "backgrounds", "fashion", "nature", "science", "education", "feelings", "health", "people",
    "religion", "places", "animals", "industry", "computer", "food", "sports", "transportation",
    "travel", "buildings", "business", "music"
};

        public StampService(string dbPath)
        {
            _db = new SQLiteAsyncConnection(dbPath);
            _db.CreateTableAsync<Stamp>().Wait();
            _db.CreateTableAsync<Category>().Wait();
        }

        public async Task MigrateConditionToEnumAsync()
        {
            // 1. Create new table with enum Condition (as INTEGER)
            await _db.ExecuteAsync(@"
        CREATE TABLE IF NOT EXISTS Stamp_New (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Name TEXT,
            Country TEXT,
            Year INTEGER,
            Condition INTEGER,
            ImagePath TEXT,
            CategoryId INTEGER
        );
    ");

            // 2. Copy and convert data
            // 2. Copy and convert data
            var oldStamps = await _db.QueryAsync<dynamic>("SELECT * FROM Stamp");
            foreach (var old in oldStamps)
            {
                StampCondition cond = StampCondition.Usado;
                if (old.Condition != null)
                {
                    Enum.TryParse<StampCondition>(old.Condition.ToString(), out cond);
                }

                await _db.ExecuteAsync(
                    "INSERT INTO Stamp_New (Id, Name, Country, Year, Condition, ImagePath, CategoryId) VALUES (?, ?, ?, ?, ?, ?, ?)",
                    old.Id, old.Name, old.Country, old.Year, (int)cond, old.ImagePath, old.CategoryId
                );
            }

            // 3. Drop old table and rename new table
            await _db.ExecuteAsync("DROP TABLE IF EXISTS Stamp;");
            await _db.ExecuteAsync("ALTER TABLE Stamp_New RENAME TO Stamp;");
        }


        public async Task<List<Stamp>> GetStampsAsync()
        {
            var result = await _db.Table<Stamp>().ToListAsync();
            return result;
        }

        public Task InsertStampAsync(Stamp stamp) => _db.InsertAsync(stamp);

        public Task UpdateStampAsync(Stamp stamp) => _db.UpdateAsync(stamp);

        public Task DeleteStampAsync(Stamp stamp) => _db.DeleteAsync(stamp);

        public async Task SaveStampAsync(Stamp stamp)
        {
            if (stamp.Id == 0)
                await InsertStampAsync(stamp);
            else
                await UpdateStampAsync(stamp);
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            var result = await _db.Table<Category>().ToListAsync();
            return result;
        }
        public async Task<List<Collection>> GetCollectionsAsync()
        {
            var result = await _db.Table<Collection>().ToListAsync();
            return result;
        }


        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            var result = await _db.Table<Category>().FirstOrDefaultAsync(c => c.Id == id);
            return result;
        }

        public Task SaveCategoryAsync(Category category) => category.Id == 0 ? _db.InsertAsync(category) : _db.UpdateAsync(category);

        public Task DeleteCategoryAsync(Category category) => _db.DeleteAsync(category);

        public async Task EnsurePixabayCategoriesAsync()
        {
            foreach (var cat in PixabayCategories)
            {
                var exists = await _db.Table<Category>().Where(c => c.Name == cat).FirstOrDefaultAsync();
                if (exists == null)
                    await _db.InsertAsync(new Category { Name = cat });
            }
        }
    }
}
