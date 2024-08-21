namespace TripBliss.Models;

public record CarBrandResponse
{
    public int Id { get; set; }
    public string? BrandName { get; set; }
    public int CarTypeId { get; set; }
    public string? CarTypename { get; set; }
    public bool Active { get; set; }

}
