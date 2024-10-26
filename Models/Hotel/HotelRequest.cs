namespace TripBliss.Models
{
    public record HotelRequest
    {
        public string? HotelName { get; set; }
        public string? HotelNameAr { get; set; }
        public int LocationId { get; set; }
    }
}
