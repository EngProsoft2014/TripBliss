using System.Text.Json.Serialization;

namespace TripBlissApi.DTO
{
    public record RequestTravelAgencyRequest
    (  
         List<RequestTravelAgencyHotelRequest>? RequestTravelAgencyHotelRequest, 
         List<RequestTravelAgencyTransportRequest>? RequestTravelAgencyTransportRequest,
         List<RequestTravelAgencyAirFlightRequest>? RequestTravelAgencyAirFlightRequest,
         List<RequestTravelAgencyVisaRequest>? RequestTravelAgencyVisaRequest,
         List<ResponseWithDistributorRequest>? ResponseWithDistributorRequest,
         [property: JsonIgnore] bool IsDelete = false
    );
}
