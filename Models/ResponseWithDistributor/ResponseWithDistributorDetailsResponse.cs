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
        public string RequestName { get; set; } = string.Empty;
        public string DistributorCompanyId { get; set; } = string.Empty;
        public DateOnly ExpireDatePay { get; set; }
        public int TotalPriceDisAccept { get; set; }
        public int TotalPriceAgentAccept { get; set; }
        public int CountPayment { get; set; }
        public int TotalPayment { get; set; }
        public DistributorCompanyResponse DistributorCompany { get; set; } = default!;
        public List<ResponseWithDistributorHotelResponse>? ResponseWithDistributorHotel { set; get; } = [];
        public List<ResponseWithDistributorTransportResponse>? ResponseWithDistributorTransport { set; get; } = [];
        public List<ResponseWithDistributorAirFlightResponse>? ResponseWithDistributorAirFlight { set; get; } = [];
        public List<ResponseWithDistributorVisaResponse>? ResponseWithDistributorVisa { set; get; } = [];

    }
}
