namespace TripBliss.Models
{
    public record ResponseWithDistributorResponse
    (
         int Id,
         int RequestTravelAgencyId,
         string DistributorCompanyId,
         DateOnly ExpireDatePay,
         int TotalPriceDisAccept,
         int TotalPriceAgentAccept
    );
}
