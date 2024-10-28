namespace TripBliss.Models
{
    public record ResponseWithDistributorTransportRequest
    {
        public string? Id { get; set; }
        public string? ResponseWithDistributorId { get; set; }
        public int RequestTravelAgencyTransportId { get; set; }
        public int Price { get; set; }
        public int Total { get; set; }
        public string? Notes { get; set; }
        public bool AcceptPriceDis { get; set; }
        public bool AcceptAgen { get; set; }
        public string CreatedUser { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public RequestTravelAgencyTransportRequest RequestTravelAgencyTransport { get; set; }
    }
}
