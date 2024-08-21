namespace TripBliss.Models;

public record CarBrandRequest
{
    public string? BrandName { get; set; }
    public int CarTypeId { get; set; }
}
