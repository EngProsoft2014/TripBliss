using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripBliss.Models
{
    public record RequestTravelAgencyResponse
    {
        public string Id { get; set; } = string.Empty;
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
        public string ChoosenServicesAr { get; set; } = string.Empty;
        public DateTime? CreateDate { get; set; }
        public DateTime? EndRequest { get; set; }
        public bool? IsAlertReview { get; set; }
        public bool? ActiveDelete { get; set; }
        public string ToolTip { get{ return IsAlertReview == true ? TripBliss.Resources.Language.AppResources.Please_make_feedback_to_finish_request : ""; } }
        public string ToolTipDisAgree { get{ return ResponseWithDistributorCount == 0 ? TripBliss.Resources.Language.AppResources.request_was_rejected_by_all_selected_distributors : ""; } }
        public List<ResponseWithDistributorResponse>? ResponseWithDistributor { get; set; }

    }
}
