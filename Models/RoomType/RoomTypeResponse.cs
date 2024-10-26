namespace TripBliss.Models
{
    public record RoomTypeResponse
    {
        public int Id { get; set; }
        public string? RoomTypeName { get; set; }
        public string? RoomTypeNameAr { get; set; }
        public string? RoomTypeNameLang { get { return Preferences.Default.Get("Lan", "en") == "ar" ? RoomTypeNameAr : RoomTypeName; } }
        public bool Active { get; set; }
    }
}
