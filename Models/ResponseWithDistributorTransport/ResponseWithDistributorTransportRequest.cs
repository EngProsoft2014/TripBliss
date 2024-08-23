namespace TripBliss.Models
{
    public record ResponseWithDistributorTransportRequest
    {
        public int ResponseWithDistributorId { get; set; }
        public int RequestTravelAgencyTransportId { get; set; }
        public int Price { get; set; }
        public int Total { get; set; }
        public string? Notes { get; set; }
        public bool AcceptPriceDis { get; set; }
        public bool AcceptAgen { get; set; }
    }
}
