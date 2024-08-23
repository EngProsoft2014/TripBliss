namespace TripBliss.Models
{
    public record ResponseWithDistributorHotelDetailsRequest
    {
        public int ResponseWithDistributorHotelId { get; set; }
        public int TravelAgencyGuestId { get; set; }
        public string? RoomRef { get; set; }
        public string? Notes { get; set; }
    }
}
