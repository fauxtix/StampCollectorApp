using SQLite;

namespace StampCollectorApp.Models
{

    public class Collection
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
