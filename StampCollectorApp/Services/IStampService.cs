using StampCollectorApp.Models;

namespace StampCollectorApp.Services;

public interface IStampService
{

    // New: support filters and paging
    Task<List<Stamp>> GetStampsAsync(string? searchQuery = null, bool? onlyForExchange = null, int page = 1, int pageSize = 20, CancellationToken cancellationToken = default);
    Task<int> GetStampsCountAsync(string? searchQuery = null, bool? onlyForExchange = null, CancellationToken cancellationToken = default);

    Task<List<Stamp>> GetStampsAsync();
    Task SaveStampAsync(Stamp stamp);
    Task DeleteStampAsync(Stamp stamp);

    Task<List<Category>> GetCategoriesAsync();
    Task<Category?> GetCategoryByIdAsync(int id);
    Task SaveCategoryAsync(Category category);
    Task DeleteCategoryAsync(Category category);
    Task EnsurePixabayCategoriesAsync();
    Task<List<Collection>> GetCollectionsAsync();
    Task<List<WikiStamps>> SearchWikiStampsAsync(string country, string year, string keyword);
    Task<List<string>> GetStampImagesByCountryCategoryAsync(string country, int limit = 10);
    Task<List<Country>> GetCountriesAsync();
}