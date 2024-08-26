

namespace TripBliss.Models.RequestTravelAgency
{
    public record RequestTravelAgencyDetailsResponse
    {
        public int Id { get; set; }
        public string TravelAgencyCompanyId { get; set; }
        public bool IsDelete { get; set; }
        public TravelAgencyCompanyResponse TravelAgencyCompany { get; set; }
        public List<ResponseWithDistributorResponse>? ResponseWithDistributor { get; set; }
        public List<RequestTravelAgencyHotelResponse> RequestTravelAgencyHotel { get; set; }
        public List<RequestTravelAgencyTransportResponse>? RequestTravelAgencyTransport { get; set; }
        public List<RequestTravelAgencyAirFlightResponse>? RequestTravelAgencyAirFlight { get; set; }
        public List<RequestTravelAgencyVisaResponse>? RequestTravelAgencyVisa { get; set; }
    }
}
