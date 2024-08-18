namespace TripBliss.Models
{
    public record TravelAgencyCompanyDocResponse
    (
         int Id,
         string TravelAgencyCompanyId,
         string? NameDoc,
         string? Notes,
         string? UploadFile
    );
}
