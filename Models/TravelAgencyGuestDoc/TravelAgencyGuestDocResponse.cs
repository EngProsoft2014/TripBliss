namespace TripBliss.Models
{
    public record TravelAgencyGuestDocResponse
    (
         int Id,
         int TravelAgencyGuestId,
         string NameDoc,
         string UploadFile,
         string Notes
    );
}
