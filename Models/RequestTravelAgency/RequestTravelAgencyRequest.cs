using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TripBliss.Models.RequestTravelAgency
{

    public record RequestTravelAgencyRequest
    {
        public List<RequestTravelAgencyHotelRequest>? RequestTravelAgencyHotel { get; set; } = new List<RequestTravelAgencyHotelRequest>();
        public List<RequestTravelAgencyTransportRequest>? RequestTravelAgencyTransport { get; set; } = new List<RequestTravelAgencyTransportRequest>();
        public List<RequestTravelAgencyAirFlightRequest>? RequestTravelAgencyAirFlight { get; set; } = new List<RequestTravelAgencyAirFlightRequest>();
        public List<RequestTravelAgencyVisaRequest>? RequestTravelAgencyVisa{ get; set; } = new List<RequestTravelAgencyVisaRequest>();
        public List<ResponseWithDistributorRequest>? ResponseWithDistributor { get; set; } = new List<ResponseWithDistributorRequest>();

    }
}
