using SQLite;

namespace StampCollectorApp.Models
{
    [Table("StampCollection")]
    public class StampCollection
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public int StampId { get; set; }

        [Indexed]
        public int CollectionId { get; set; }

        public bool OriginalForTheCollection { get; set; }

        public string? Remarks { get; set; }

        // Relacionamentos apenas para navegação (não usados por SQLite-net)
        [Ignore]
        public Stamp? Stamp { get; set; }

        [Ignore]
        public Collection? Collection { get; set; }
    }
}
