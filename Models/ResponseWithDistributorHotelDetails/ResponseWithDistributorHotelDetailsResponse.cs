namespace TripBliss.Models
{
    public record ResponseWithDistributorHotelDetailsResponse
    {
        public int Id { get; set; }
        public int ResponseWithDistributorHotelId { get; set; }
        public int TravelAgencyGuestId { get; set; }
        public string? RoomRef { get; set; }
        public string? Notes { get; set; }
    }
}
