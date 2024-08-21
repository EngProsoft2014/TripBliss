namespace TripBliss.Models
{
    public record HotelResponse
    {
        public int Id { get; set; }
        public string? HotelName { get; set; }
        public int LocationId { get; set; }
        public string? LocationName { get; set; }
        public bool Active { get; set; }
    }
}
