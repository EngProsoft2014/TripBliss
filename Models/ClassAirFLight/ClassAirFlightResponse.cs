namespace TripBliss.Models;

public record ClassAirFlightResponse
{
    public int Id { get; set; }
    public string? ClassName { get; set; }
    public bool Active { get; set; }

}
