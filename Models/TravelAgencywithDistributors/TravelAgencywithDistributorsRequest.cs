namespace TripBliss.Models
{
    public record TravelAgencywithDistributorsRequest
    (
         string TravelAgencyCompanyId ,
         string DistributorCompanyId ,
         bool Private 
    );
}
