namespace TripBliss.Models
{
    public record MealResponse
    {
        public int Id { get; set; }
        public string? MealName { get; set; }
        public bool Active { get; set; }
    }
}
