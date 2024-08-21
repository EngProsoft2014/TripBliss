namespace TripBliss.Models
{
    public record RoomTypeResponse
    {
        public int Id { get; set; }
        public string? RoomTypeName { get; set; }
        public bool Active { get; set; }
    }
}
