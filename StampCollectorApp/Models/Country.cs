using SQLite;

namespace StampCollectorApp.Models
{
    public class Country
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
