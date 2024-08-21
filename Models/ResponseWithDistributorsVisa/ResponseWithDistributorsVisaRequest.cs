namespace TripBliss.Models.ResponseWithDistributorsVisa
{
    public record ResponseWithDistributorsVisaRequest
    (
         int ResponseWithDistributorId,
         int RequestTravelAgencyVisaId,
         int Price,
         int Total,
         string? Notes,
         bool AcceptPriceDis,
         bool AcceptAgen
    );
}
