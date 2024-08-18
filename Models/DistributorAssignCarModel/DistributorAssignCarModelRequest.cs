namespace TripBliss.Models
{
    public record DistributorAssignCarModelRequest
    (
         string DistributorCompanyId ,
         int CarTypeId ,
         int CarBrandId ,
         int CarModelId ,
         int Count ,
         bool IsTrackCount ,
         bool IsTrackingDate 
    );
}
