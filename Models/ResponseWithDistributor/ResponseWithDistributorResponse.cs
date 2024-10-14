namespace TripBliss.Models
{
    public record ResponseWithDistributorResponse
    {
        public int Id { get; set; }
        public int RequestTravelAgencyId { get; set; }
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

        public int ReviewToTravelAgency { get; set; } = 0;
        public string? ReviewDistributorNote { get; set; }
        public string? ReviewUserDistributorName { get; set; }
        public string ChoosenServices { get; set; } = string.Empty;
        public DateTime? CreateDate { get; set; }


    }
}
