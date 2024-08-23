namespace TripBliss.Models;

public record DistributorAssignAirFlightResponse
{
    public int Id { get; set; }
    public string? DistributorCompanyId { get; set; }
    public int AirFlightId { get; set; }
    public int ClassAirFlightId { get; set; }
    public int Count { get; set; }
    public bool IsTrackCount { get; set; }
    public bool IsTrackingDate { get; set; }
}
