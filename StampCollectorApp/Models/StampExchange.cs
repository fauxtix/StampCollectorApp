using SQLite;

namespace StampCollectorApp.Models
{
    [Table("StampExchange")]
    public class StampExchange
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string StampDescription { get; set; }
        public DateTime ExchangeDate { get; set; }
        public string CollectorName { get; set; }
        public string CollectorContact { get; set; }
        public string? Notes { get; set; }
    }
}