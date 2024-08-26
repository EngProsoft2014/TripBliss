using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripBliss.Models.RequestTravelAgency
{

    public record RequestTravelAgencyRequest
    {
        public List<RequestTravelAgencyHotelRequest>? RequestTravelAgencyHotelRequest{ get; set; } = new List<RequestTravelAgencyHotelRequest>();
        public List<RequestTravelAgencyTransportRequest>? RequestTravelAgencyTransportRequest { get; set; } = new List<RequestTravelAgencyTransportRequest>();
        public List<RequestTravelAgencyAirFlightRequest>? RequestTravelAgencyAirFlightRequest { get; set; } = new List<RequestTravelAgencyAirFlightRequest>();
        public List<RequestTravelAgencyVisaRequest>? RequestTravelAgencyVisaRequest { get; set; } = new List<RequestTravelAgencyVisaRequest>();
        public List<ResponseWithDistributorRequest>? ResponseWithDistributorRequest { get; set; } = new List<ResponseWithDistributorRequest>();
        public bool IsDelete { get; set; } = false;
    }
}
