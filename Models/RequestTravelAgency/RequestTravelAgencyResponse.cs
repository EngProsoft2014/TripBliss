namespace TripBliss.Models
{
    public record RequestTravelAgencyResponse
    (
         int Id,
         string TravelAgencyCompanyId,
         bool IsDelete,
         List<RequestTravelAgencyHotelResponse> RequestTravelAgencyHotelResponse
    );
}
