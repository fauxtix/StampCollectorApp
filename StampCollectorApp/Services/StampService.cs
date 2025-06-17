using SQLite;
using StampCollectorApp.Models;
using System.Text.Json;

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
        }

        // Paging/filtering now done at DB level
        public async Task<List<Stamp>> GetStampsAsync(string? searchQuery = null, bool? onlyForExchange = null, int page = 1, int pageSize = 20, CancellationToken cancellationToken = default)
        {
            var query = _db.Table<Stamp>();

            if (onlyForExchange.HasValue && onlyForExchange.Value)
                query = query.Where(s => s.ForExchange);

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                query = query.Where(s =>
                    (s.Name != null && s.Name.Contains(searchQuery)) ||
                    s.Year.ToString().Contains(searchQuery) ||
                    s.Condition.ToString().Replace("_", " ").Contains(searchQuery)
                );
            }

            int skip = (page - 1) * pageSize;
            query = query.Skip(skip).Take(pageSize);

            return await query.ToListAsync();
        }

        public async Task<int> GetStampsCountAsync(string? searchQuery = null, bool? onlyForExchange = null, CancellationToken cancellationToken = default)
        {
            var query = _db.Table<Stamp>();

            if (onlyForExchange.HasValue && onlyForExchange.Value)
                query = query.Where(s => s.ForExchange);

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                query = query.Where(s =>
                    (s.Name != null && s.Name.Contains(searchQuery)) ||
                    s.Year.ToString().Contains(searchQuery) ||
                    s.Condition.ToString().Replace("_", " ").Contains(searchQuery)
                );
            }
            var _count = await query.CountAsync();
            return _count;
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
        public async Task<List<Country>> GetCountriesAsync()
        {
            var result = await _db.Table<Country>().ToListAsync();
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


        public async Task<bool> CanAddStampToCollectionAsync(int colecaoId)
        {
            var colecao = await _db.Table<Collection>().Where(c => c.Id == colecaoId).FirstOrDefaultAsync();
            if (colecao == null) return false;

            var selosOriginais = await _db.Table<StampCollection>()
                .Where(sc => sc.CollectionId == colecaoId && sc.OriginalForTheCollection)
                .CountAsync();

            return selosOriginais < colecao.TotalExpected;
        }

        public async Task<List<WikiStamps>> SearchWikiStampsAsync(string country, string year, string keyword)
        {
            var token = "YOUR_API_KEY";
            var url = $"https://colnect.com/api/v3/stamps/search?country={country}&year={year}&text={keyword}&token={token}";

            using var client = new HttpClient();
            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return new List<WikiStamps>();

            var json = await response.Content.ReadAsStringAsync();

            var raw = JsonSerializer.Deserialize<JsonElement>(json);
            var list = new List<WikiStamps>();

            foreach (var item in raw.GetProperty("items").EnumerateArray())
            {
                list.Add(new WikiStamps
                {
                    Name = item.GetProperty("name").GetString() ?? "",
                    Year = item.GetProperty("year").GetString() ?? "",
                    ImageUrl = item.GetProperty("image_url").GetString() ?? ""
                });
            }

            return list;
        }

        public async Task<List<string>> GetStampImagesByCountryCategoryAsync(string country, int limit = 10)
        {
            var httpClient = new HttpClient();

            string category = $"Category:Postage_stamps_of_{country.Replace(" ", "_")}";

            string url = $"https://commons.wikimedia.org/w/api.php?action=query&format=json&list=categorymembers&cmtitle={Uri.EscapeDataString(category)}&cmtype=file&cmlimit={limit}";

            var response = await httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return new List<string>();

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);

            var images = new List<string>();

            if (doc.RootElement.TryGetProperty("query", out var query) &&
                query.TryGetProperty("categorymembers", out var members))
            {
                foreach (var member in members.EnumerateArray())
                {
                    if (member.TryGetProperty("title", out var titleProp))
                    {
                        string fileTitle = titleProp.GetString()!; // e.g. "File:Stamp_of_Portugal.png"

                        string infoUrl = $"https://commons.wikimedia.org/w/api.php?action=query&format=json&titles={Uri.EscapeDataString(fileTitle)}&prop=imageinfo&iiprop=url";

                        var infoResponse = await httpClient.GetAsync(infoUrl);
                        if (!infoResponse.IsSuccessStatusCode)
                            continue;

                        var infoJson = await infoResponse.Content.ReadAsStringAsync();
                        using var infoDoc = JsonDocument.Parse(infoJson);

                        var pages = infoDoc.RootElement.GetProperty("query").GetProperty("pages");
                        foreach (var page in pages.EnumerateObject())
                        {
                            if (page.Value.TryGetProperty("imageinfo", out var imageInfo))
                            {
                                string imageUrl = imageInfo[0].GetProperty("url").GetString()!;
                                images.Add(imageUrl);
                            }
                        }
                    }
                }
            }

            return images;
        }

        public async Task<bool> AnyStampsForExchangeAsync()
        {
            var result = await _db.Table<Stamp>().Where(s => s.ForExchange).FirstOrDefaultAsync();
            return result != null;
        }
    }
}
