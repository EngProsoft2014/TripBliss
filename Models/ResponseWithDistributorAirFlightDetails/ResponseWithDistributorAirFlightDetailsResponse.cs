namespace TripBliss.Models
{
    public record ResponseWithDistributorAirFlightDetailsResponse
    (
         int Id,
         int ResponseWithDistributorAirFlightId,
         int TravelAgencyGuestId,
         string? Notes
    );
}
