namespace TripBliss.Models
{
    public record ResponseWithDistributorAirFlightResponse
    {
        public int Id { get; set; }
        public int ResponseWithDistributorId { get; set; }
        public int RequestTravelAgencyAirFlightId { get; set; }
        public int PriceAdult { get; set; }
        public int PriceChild { get; set; }
        public int PriceInfant { get; set; }
        public int Total { get; set; }
        public string? Notes { get; set; }
        public bool AcceptPriceDis { get; set; }
        public bool AcceptAgen { get; set; }
    }
}
