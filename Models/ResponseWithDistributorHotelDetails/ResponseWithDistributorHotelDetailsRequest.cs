namespace TripBliss.Models
{
    public record ResponseWithDistributorHotelDetailsRequest
    {
        public string? Id { get; set; }
        public string? ResponseWithDistributorHotelId { get; set; }
        //public int TravelAgencyGuestId { get; set; }
        public string? GuestName { get; set; }
        public string? RoomRef { get; set; }
        public string? Notes { get; set; }
        public string CreatedUser { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
    }
}
