namespace TripBliss.Models
{
    public record ResponseWithDistributorAirFlightDetailsResponse
    {
        public int Id { get; set; }
        public int ResponseWithDistributorAirFlightId { get; set; }
        public int TravelAgencyGuestId { get; set; }
        public string? Notes { get; set; }
    }
}
