namespace TripBliss.Models
{
    public record TravelAgencyCompanyDocRequest
    (
         string TravelAgencyCompanyId ,
         string? NameDoc ,
         string? Notes ,
         string? UploadFile 
    );
}
