namespace TripBliss.Models
{
    public record DistributorAssignCarModelResponse
    (
         int Id ,
         string DistributorCompanyId ,
         int CarTypeId ,
         int CarBrandId ,
         int CarModelId ,
         int Count ,
         bool IsTrackCount,
         bool IsTrackingDate 
    );
}
