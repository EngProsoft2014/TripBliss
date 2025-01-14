namespace TripBliss.Models
{
    public record HotelResponse
    {
        public int Id { get; set; }
        public string? HotelName { get; set; }
        public string? HotelNameAr { get; set; }
        public string? HotelNameLang { get { return Preferences.Default.Get("Lan", "en") == "ar" ? HotelNameAr : HotelName; } }
        public int LocationId { get; set; }
        public string? LocationName { get; set; }
        public string? LocationNameAr { get; set; }
        public string? LocationNameLang { get { return Preferences.Default.Get("Lan", "en") == "ar" ? LocationNameAr : LocationName; } }
        public bool Active { get; set; }
  
        public string Address { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public Location MPosition { get { return (!string.IsNullOrEmpty(Lat) && !string.IsNullOrEmpty(Lng)) ? new Location(double.Parse(Lat), double.Parse(Lng)) : new Location(22.651246, 39.811955); } set { } }
    }
}
