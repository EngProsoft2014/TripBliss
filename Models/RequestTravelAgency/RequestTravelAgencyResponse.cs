using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Models.RequestTravelAgencyVisa;

namespace TripBliss.Models.RequestTravelAgency
{
    public record RequestTravelAgencyResponse
    {
         public int Id { get; set; }
         public string? TravelAgencyCompanyId { get; set; }
         public bool IsDelete { get; set; }
         public List<ResponseWithDistributorResponse>? ResponseWithDistributorResponse { get; set; }
         public List<RequestTravelAgencyHotelResponse>? RequestTravelAgencyHotelResponse { get; set; }
         public List<RequestTravelAgencyTransportResponse>? RequestTravelAgencyTransportResponse { get; set; }
         public List<RequestTravelAgencyAirFlightResponse>? RequestTravelAgencyAirFlightResponse { get; set; }
         public List<RequestTravelAgencyVisaResponse>? RequestTravelAgencyVisaResponse { get; set; }
    }
}
