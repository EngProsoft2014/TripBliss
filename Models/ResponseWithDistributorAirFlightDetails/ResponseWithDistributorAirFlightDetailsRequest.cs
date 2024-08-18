namespace TripBliss.Models
{
    public record ResponseWithDistributorAirFlightDetailsRequest
    (
         int ResponseWithDistributorAirFlightId ,
         int TravelAgencyGuestId ,
         string? Notes 
    );
}
