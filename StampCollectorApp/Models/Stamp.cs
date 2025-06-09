using SQLite;

namespace StampCollectorApp.Models;

public class Stamp
{
    [PrimaryKey]
    [AutoIncrement]
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Year { get; set; } = DateTime.Now.Year;
    public StampCondition Condition { get; set; }
    public string? ImagePath { get; set; }
    public string? FaceValue { get; set; }
    public string? StampLocation { get; set; }
    public DateTime AcquisitionDate { get; set; }
    public decimal PricePaid { get; set; }
    public bool ForExchange { get; set; }
    public string? Notes { get; set; }
    public int CategoryId { get; set; }
    public int CollectionId { get; set; }
    public int CountryId { get; set; }
    public int TagId { get; set; }

    [Ignore]
    public List<StampCollection> Collections { get; set; } = new();
    [Ignore]
    public List<Country> Countries { get; set; } = new();

    [Ignore]
    public string CountryName { get; set; } = string.Empty;
}

public enum StampCondition
{
    Novo,
    Usado,
    Danificado,
    Com_Charneira,
    Sem_Charneira
}

