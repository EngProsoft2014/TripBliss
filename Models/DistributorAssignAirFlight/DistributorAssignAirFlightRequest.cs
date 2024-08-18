namespace TripBliss.Models;

public record DistributorAssignAirFlightRequest
(
    string DistributorCompanyId,
    int AirFlightId,
    int ClassAirFlightId,
    int Count,
    bool IsTrackCount,
    bool IsTrackingDate 
);
