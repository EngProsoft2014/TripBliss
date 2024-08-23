using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Models.RequestTravelAgencyVisa;

namespace TripBliss.Models.RequestTravelAgency
{

    public record RequestTravelAgencyRequest
    {
        public List<RequestTravelAgencyHotelRequest>? RequestTravelAgencyHotelRequest{ get; set; }
        public List<RequestTravelAgencyTransportRequest>? RequestTravelAgencyTransportRequest { get; set; }
        public List<RequestTravelAgencyAirFlightRequest>? RequestTravelAgencyAirFlightRequest { get; set; }
        public List<RequestTravelAgencyVisaRequest>? RequestTravelAgencyVisaRequest { get; set; }
        public List<ResponseWithDistributorRequest>? ResponseWithDistributorRequest { get; set; }
        public bool IsDelete { get; set; } = false;
    }
}
