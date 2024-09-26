namespace TripBliss.Models
{
    public record ResponseWithDistributorAirFlightDetailsRequest
    {
        public byte[] ImgFile { get; set; } = default!;
        public string? Extension { get; set; }
        //public int ResponseWithDistributorAirFlightId { get; set; }
        //public int TravelAgencyGuestId { get; set; }
        //public string? Notes { get; set; }
    }
}
