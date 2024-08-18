namespace TripBliss.Models
{
    public record TravelAgencywithDistributorsResponse
    (
         int Id,
         string TravelAgencyCompanyId,
         string DistributorCompanyId,
         bool Private
    );
}
