using SQLite;
using StampCollectorApp.Models;

namespace StampCollectorApp.Services;

public class CountryService : ICountryService
{
    private readonly SQLiteAsyncConnection _database;

    public CountryService(SQLiteAsyncConnection database)
    {
        _database = database;
    }

    public async Task<List<Country>> GetCountriesAsync()
    {
        var result = await _database.Table<Country>().OrderBy(c => c.Name).ToListAsync();
        return result;
    }

    public async Task SaveCountryAsync(Country country)
    {
        try
        {
            if (country.Id == 0)
                await _database.InsertAsync(country);
            else
                await _database.UpdateAsync(country);

        }
        catch (Exception ex)
        {
            var x = ex.Message;
        }
    }


    public async Task DeleteCountryAsync(Country country) => await _database.DeleteAsync(country);

    public async Task<bool> CountryNameExistsAsync(string name, int excludeId = 0)
    {
        var existing = await _database.Table<Country>()
            .Where(c => c.Name.ToLower() == name.ToLower() && c.Id != excludeId)
            .FirstOrDefaultAsync();
        return existing != null;
    }
}
