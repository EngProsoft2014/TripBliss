namespace TripBliss.Models
{
    public record ResponseWithDistributorAirFlightResponse
    (
         int Id,
         int ResponseWithDistributorId,
         int RequestTravelAgencyAirFlightId,
         int PriceAdult,
         int PriceChild,
         int PriceInfant,
         int Total,
         string? Notes,
         bool AcceptPriceDis,
         bool AcceptAgen
    );
}
