namespace TripBliss.Models
{
    public record ResponseWithDistributorAirFlightDetailsRequest
    {
        public int ResponseWithDistributorAirFlightId { get; set; }
        public int TravelAgencyGuestId { get; set; }
        public string? Notes { get; set; }
    }
}
