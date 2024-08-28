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
        public DateOnly ExpireDatePay { get; set; }
        public int TotalPriceDisAccept { get; set; }
        public int TotalPriceAgentAccept { get; set; }
        public string RequestName { get; set; } = string.Empty;
    }
}
