namespace TripBliss.Models
{
    public record MealResponse
    {
        public int Id { get; set; }
        public string? MealName { get; set; }
        public string? MealNameAr { get; set; }
        public string? MealNameLang { get { return Preferences.Default.Get("Lan", "en") == "ar" ? MealNameAr : MealName; } }
        public bool Active { get; set; }
    }
}
