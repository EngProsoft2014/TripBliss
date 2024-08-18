namespace TripBliss.Models
{
    public record TravelAgencyGuestResponse
    (
         int Id,
         string TravelAgencyCompanyId,
         string GuestName,
         string Nationality,
         int? TypeIdNumber,
         string IDNumber,
         string Phone,
         string Email,
         string AdultCount,
         string ChildCount,
         string InfantCount
    );
}
