namespace TripBliss.Models
{
    public record RoomViewResponse
    {
        public int Id { get; set; }
        public string? RoomViewName { get; set; }
        public string? RoomViewNameAr { get; set; }
        public string? RoomViewNameLang { get { return Preferences.Default.Get("Lan", "en") == "ar" ? RoomViewNameAr : RoomViewName; } }
        public bool Active { get; set; }
    }
}
