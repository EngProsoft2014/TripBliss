namespace TripBliss.Models
{
    public record RequestTravelAgencyRequest
    (
         string TravelAgencyCompanyId ,
         bool IsDelete ,
         List<RequestTravelAgencyHotelRequest> RequestTravelAgencyHotelRequests
    );
}
