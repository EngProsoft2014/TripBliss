using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripBliss.Models
{
    public record ResponseWithDistributorDetailsResponse
    {
        public string? Id { get; set; }
        public string? RequestTravelAgencyId { get; set; }
        public string RequestName { get; set; } = string.Empty;
        public string DistributorCompanyId { get; set; } = string.Empty;
        public DateOnly ExpireDatePay { get; set; }
        public int TotalPriceDisAccept { get; set; }
        public int TotalPriceAgentAccept { get; set; }
        public int CountPayment { get; set; }
        public int TotalPayment { get; set; }
        public int ReviewToDistributor { get; set; }
        public string? ReviewTravelAgentNote { set; get; }
        public string? ReviewUserTravelAgentName { set; get; }
        public DateTime? ReviewTravelAgentDateTime { get; set; }
        public bool ReviewTravelAgentShow { get; set; }

        public int ReviewToTravelAgency { get; set; } = 0;
        public string? ReviewDistributorNote { get; set; }
        public string? ReviewUserDistributorName { get; set; }
        public DateTime? ReviewDistributorDateTime { get; set; }
        public bool ReviewDistributorShow { get; set; }

        public bool? IsHistory { get; set; }

        public DistributorCompanyResponse DistributorCompany { get; set; } = default!;
        public List<ResponseWithDistributorHotelResponse>? ResponseWithDistributorHotel { set; get; } = [];
        public List<ResponseWithDistributorTransportResponse>? ResponseWithDistributorTransport { set; get; } = [];
        public List<ResponseWithDistributorAirFlightResponse>? ResponseWithDistributorAirFlight { set; get; } = [];
        public List<ResponseWithDistributorVisaResponse>? ResponseWithDistributorVisa { set; get; } = [];
    }
}
