using StampCollectorApp.Models;

public interface ICollectionService
{
    Task<List<Collection>> GetCollectionsAsync();
    Task SaveCollectionAsync(Collection collection);
    Task DeleteCollectionAsync(Collection collection);
    Task<bool> CollectionNameExistsAsync(string name, int excludeId = 0);
}
