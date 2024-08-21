namespace TripBliss.Models;

public record CarModelRequest
{
    public string? ModelName { get; set; }
    public string? CarYear { get; set; }
    public int CarBrandId { get; set; }
}


