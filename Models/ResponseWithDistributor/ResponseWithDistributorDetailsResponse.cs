using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripBliss.Models
{
    public record ResponseWithDistributorDetailsResponse
    {
        public int Id { get; set; }
        public int RequestTravelAgencyId { get; set; }
        public string? RequestName { get; set; }
        public string? DistributorCompanyId { get; set; }
        public DateOnly ExpireDatePay { get; set; }
        public int TotalPriceDisAccept { get; set; }
        public int TotalPriceAgentAccept { get; set; }
        public DistributorCompanyResponse? DistributorCompany { get; set; }
        public List<ResponseWithDistributorHotelResponse>? ResponseWithDistributorHotel { get; set; }
        public List<ResponseWithDistributorTransportResponse>? ResponseWithDistributorTransport { get; set; }
        public List<ResponseWithDistributorAirFlightResponse>? ResponseWithDistributorAirFlight { get; set; }
        public List<ResponseWithDistributorVisaResponse>? ResponseWithDistributorVisa { get; set; }

    }
}
