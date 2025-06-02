
public interface IDatabaseInitializerService
{
    Task ClearDataAsync();
    Task InitializeAsync();
    Task RecreateTablesAsync();
}