using StampCollectorApp.Models;

namespace StampCollectorApp.Services
{
    public interface IExchangeService
    {
        Task AddExchangeAsync(StampExchange exchange);
        Task<List<StampExchange>> GetAllAsync();
    }
}