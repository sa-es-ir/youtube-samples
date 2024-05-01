namespace EFQueryOptimization.Entities;

public class Company
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Country { get; set; }

    public string Description { get; set; }

    public DateTime FoundedAt { get; set; }
}