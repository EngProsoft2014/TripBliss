namespace TripBliss.Models
{
    public record HotelRequest
    {
        public string? HotelName { get; set; }
        public int LocationId { get; set; }
    }
}
