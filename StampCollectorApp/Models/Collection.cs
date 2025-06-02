using SQLite;

namespace StampCollectorApp.Models
{

    public class Collection
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? TotalExpected { get; set; }
        public int? TotalCollected { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? Notes { get; set; }

        [Ignore]
        public List<StampCollection> Stamps { get; set; } = new();
    }
}
