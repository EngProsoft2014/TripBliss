namespace TripBliss.Models.ResponseWithDistributorsVisa
{
    public record ResponseWithDistributorsVisaResponse
    (
         int Id,
         int ResponseWithDistributorId,
         int RequestTravelAgencyVisaId,
         int Price,
         int Total,
         string? Notes,
         bool AcceptPriceDis,
         bool AcceptAgen
    );
}
