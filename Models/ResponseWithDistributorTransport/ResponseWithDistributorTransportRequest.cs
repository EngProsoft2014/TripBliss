namespace TripBliss.Models
{
    public record ResponseWithDistributorTransportRequest
    (
         int ResponseWithDistributorId ,
         int RequestTravelAgencyTransportId ,
         int Price ,
         int Total ,
         string? Notes ,
         bool AcceptPriceDis,
         bool AcceptAgen 
    );
}
