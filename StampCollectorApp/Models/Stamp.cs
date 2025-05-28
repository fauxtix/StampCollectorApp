using SQLite;

namespace StampCollectorApp.Models;

public class Stamp
{
    [PrimaryKey]
    [AutoIncrement]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Country { get; set; }
    public int Year { get; set; } = DateTime.Now.Year;
    public StampCondition Condition { get; set; }
    public string? ImagePath { get; set; }
    public int CategoryId { get; set; }
    public int CollectionId { get; set; }
    public int TagId { get; set; }
}

public enum StampCondition
{
    Novo,
    Usado,
    Danificado,
    Com_Charneira,
    Sem_Charneira
}

