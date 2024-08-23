namespace TripBlissApi.DTO
{
    public record RequestTravelAgencyResponse
    {
         public int Id { get; set },
         public string TravelAgencyCompanyId { get; set },
         public bool IsDelete { get; set },
         public List<ResponseWithDistributorResponse>? ResponseWithDistributorResponse { get; set },
         public List<RequestTravelAgencyHotelResponse> RequestTravelAgencyHotelResponse { get; set },  
         public List<RequestTravelAgencyTransportResponse>? RequestTravelAgencyTransportResponse { get; set },
         public List<RequestTravelAgencyAirFlightResponse>? RequestTravelAgencyAirFlightResponse { get; set },
         public List<RequestTravelAgencyVisaResponse>? RequestTravelAgencyVisaResponse { get; set }
    };
}
