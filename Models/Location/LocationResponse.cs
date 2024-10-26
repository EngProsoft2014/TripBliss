namespace TripBliss.Models
{
    public record LocationResponse
    {
        public int Id { get; set; }
        public string? LocationName { get; set; }
        public string? LocationNameAr { get; set; }
        public string? LocationNameLang { get { return Preferences.Default.Get("Lan", "en") == "ar" ? LocationNameAr : LocationName; } }
        public bool Active { get; set; }
    }
}
