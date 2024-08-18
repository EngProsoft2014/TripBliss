namespace TripBliss.Models
{
    public record TravelAgencyGuestDocRequest
    (
         int TravelAgencyGuestId ,
         string NameDoc,
         string UploadFile ,
         string Notes 
    );
}
