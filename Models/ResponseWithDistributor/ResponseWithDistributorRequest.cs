namespace TripBliss.Models
{
    public record ResponseWithDistributorRequest
    (
         int RequestTravelAgencyId ,
         string DistributorCompanyId ,
         DateOnly ExpireDatePay ,
         int TotalPriceDisAccept ,
         int TotalPriceAgentAccept 
    );
}
