namespace TripBliss.Models
{
    public record RoomViewResponse
    {
        public int Id { get; set; }
        public string? RoomViewName { get; set; }
        public bool Active { get; set; }
    }
}
