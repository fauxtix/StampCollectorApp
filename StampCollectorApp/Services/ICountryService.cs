using StampCollectorApp.Models;

namespace StampCollectorApp.Services
{
    public interface ICountryService
    {
        Task<bool> CountryNameExistsAsync(string name, int excludeId = 0);
        Task DeleteCountryAsync(Country country);
        Task<List<Country>> GetCountriesAsync();
        Task SaveCountryAsync(Country country);
    }
}