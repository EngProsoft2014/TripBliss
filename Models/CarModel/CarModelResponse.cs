namespace TripBliss.Models;

public record CarModelResponse
{
    public int Id { get; set; }
    public string? ModelName { get; set; }
    public string? CarYear { get; set; }
    public int CarBrandId { get; set; }
    public string ?CarBrandName { get; set; }
    public bool Active { get; set; }

    
}
