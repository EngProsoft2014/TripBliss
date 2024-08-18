namespace TripBliss.Models;

public record DistributorAssignAirFlightResponse
(
     int Id,
     string DistributorCompanyId,
     int AirFlightId ,
     int ClassAirFlightId,
     int Count ,
     bool IsTrackCount ,
     bool IsTrackingDate 
);
