namespace TripBliss.Models
{
    public record TravelAgencyGuestRequest
    (
         string TravelAgencyCompanyId ,
         string GuestName ,
         string Nationality ,
         int? TypeIdNumber ,
         string IDNumber ,
         string Phone,
         string Email ,
         string AdultCount ,
         string ChildCount ,
         string InfantCount
    );
}
