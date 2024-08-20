namespace TripBliss.Models;

public record AirFlightResponse
{
    public int Id{ get; set; }
    public string? AirLine { get; set; }
    public bool Active { get; set; }

    public string? CreatedUser { get; set; }
    public DateTime CreatedDate { get; set; }
    public string? UpdatedUser { get; set; }
    public DateTime? UpdatedDate { get; set; }
}
