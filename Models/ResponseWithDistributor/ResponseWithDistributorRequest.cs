namespace TripBliss.Models
{
    public record ResponseWithDistributorRequest
    {
        public int RequestTravelAgencyId { get; set; }
        public string? DistributorCompanyId { get; set; }
        public DateOnly ExpireDatePay { get; set; }
        public int TotalPriceDisAccept { get; set; }
        public int TotalPriceAgentAccept { get; set; }
    }
}
