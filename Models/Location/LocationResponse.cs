namespace TripBliss.Models
{
    public record LocationResponse
    {
        public int Id { get; set; }
        public string? LocationName { get; set; }
        public bool Active { get; set; }
    }
}
