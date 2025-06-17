using SQLite;
using StampCollectorApp.Models;

namespace StampCollectorApp.Services
{
    public class ExchangeService : IExchangeService
    {
        private readonly SQLiteAsyncConnection _db;

        public ExchangeService(SQLiteAsyncConnection db)
        {
            _db = db;
            //_db.CreateTableAsync<StampExchange>();
        }

        public Task<List<StampExchange>> GetAllAsync() =>
            _db.Table<StampExchange>().OrderByDescending(x => x.ExchangeDate).ToListAsync();

        public Task AddExchangeAsync(StampExchange exchange) =>
            _db.InsertAsync(exchange);
    }
}