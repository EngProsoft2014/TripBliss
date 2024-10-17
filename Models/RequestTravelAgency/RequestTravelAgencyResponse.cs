using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripBliss.Models
{
    public record RequestTravelAgencyResponse
    {
        public int Id { get; set; }
        public string TravelAgencyCompanyId { get; set; } = string.Empty;
        public string? TravelAgencyCompanyName { get; set; }
        public int? TravelAgencyCompanyReview { get; set; }
        public string RequestName { get; set; } = string.Empty;
        public bool IsDelete { get; set; }
        public int ResponseWithDistributorCount { get; set; }
        public int TotalPriceDisAcceptCount { get; set; }
        public int TotalPriceAgentAcceptCount { get; set; }
        public int CountAirFlight { get; set; }
        public int CountHotel { get; set; }
        public int CountTransport { get; set; }
        public int CountVisa { get; set; }
        public string ChoosenServices { get; set; } = string.Empty;
        public DateTime? CreateDate { get; set; }
        public DateTime? EndRequest { get; set; }
        public bool? IsAlertReview { get; set; }
        //public bool IsShowExpiredTemplete { get { return EndRequest != null && EndRequest > DateTime.Now.AddDays(3) ? true : false; } }
        public string ToolTip { get{ return IsAlertReview == true ? "Please make feedback to finish request" : ""; } }
        public List<ResponseWithDistributorResponse>? ResponseWithDistributor { get; set; }

    }
}
