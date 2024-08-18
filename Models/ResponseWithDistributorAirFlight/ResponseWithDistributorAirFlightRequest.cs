namespace TripBliss.Models
{
    public record ResponseWithDistributorAirFlightRequest
    (
         int ResponseWithDistributorId ,
         int RequestTravelAgencyAirFlightId ,
         int PriceAdult ,
         int PriceChild ,
         int PriceInfant ,
         int Total ,
         string? Notes ,
         bool AcceptPriceDis ,
         bool AcceptAgen 
    );
}
