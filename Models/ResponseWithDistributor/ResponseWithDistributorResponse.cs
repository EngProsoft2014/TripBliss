namespace TripBliss.Models
{
    public record ResponseWithDistributorResponse
    {
        public string? Id { get; set; } 
        public string? RequestTravelAgencyId { get; set; }
        public string? TravelAgencyCompanyName { get; set; }
        public int? TravelAgencyCompanyReview { get; set; }
        public string DistributorCompanyId { get; set; } = string.Empty;
        public string? DistributorCompanyName { get; set; }
        public int? DistributorCompanyReview { get; set; }
        public string? DistributorCompanyPhone { get; set; }
        public string? DistributorCompanyAddress { get; set; }
        public DateOnly ExpireDatePay { get; set; }
        public int TotalPriceDisAccept { get; set; }
        public int TotalPriceAgentAccept { get; set; }
        public string RequestName { get; set; } = string.Empty;
        public int CountAirFlight { get; set; }
        public int CountHotel { get; set; }
        public int CountTransport { get; set; }
        public int CountVisa { get; set; }
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

        public string ChoosenServices { get; set; } = string.Empty;
        public string ChoosenServicesAr { get; set; } = string.Empty;
        public DateTime? CreateDate { get; set; }

        public DateTime? EndRequest { get; set; }
        public bool? IsAlertReview { get; set; }
        public bool? IsLate { get; set; }
        public string ToolTip { get { return IsAlertReview == true ? TripBliss.Resources.Language.AppResources.Please_make_feedback_to_finish_request : ""; } }

    }
}
