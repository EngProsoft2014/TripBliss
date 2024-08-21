namespace TripBliss.Models;

public record AirFlightResponse
{
    public int Id{ get; set; }
    public string? AirLine { get; set; }
    public bool Active { get; set; }

    
}
