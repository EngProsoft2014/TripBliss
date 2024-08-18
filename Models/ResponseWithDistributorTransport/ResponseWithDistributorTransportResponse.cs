namespace TripBliss.Models
{
    public record ResponseWithDistributorTransportResponse
    (
         int Id,
         int ResponseWithDistributorId,
         int RequestTravelAgencyTransportId,
         int Price,
         int Total,
         string? Notes,
         bool AcceptPriceDis,
         bool AcceptAgen
    );
}
