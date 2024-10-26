namespace TripBliss.Models
{
    public record RequestTravelAgencyHotelResponse
    {
        public string Id { get; set; } = string.Empty;
        public string? RequestTravelAgencyId { get; set; }
        public int LocationId { get; set; }
        public string? LocationName { get; set; }
        public string? LocationNameAr { get; set; }
        public string? LocationNameLang { get { return Preferences.Default.Get("Lan", "en") == "ar" ? LocationNameAr : LocationName; } }
        public int HotelId { get; set; }
        public string? HotelName { get; set; }
        public string? HotelNameAr { get; set; }
        public string? HotelNameLang { get { return Preferences.Default.Get("Lan", "en") == "ar" ? HotelNameAr : HotelName; } }
        public int RoomTypeId { get; set; }
        public string? RoomTypeName { get; set; }
        public string? RoomTypeNameAr { get; set; }
        public string? RoomTypeNameLang { get { return Preferences.Default.Get("Lan", "en") == "ar" ? RoomTypeNameAr : RoomTypeName; } }
        public int RoomViewId { get; set; }
        public string? RoomViewName { get; set; }
        public string? RoomViewNameAr { get; set; }
        public string? RoomViewNameLang { get { return Preferences.Default.Get("Lan", "en") == "ar" ? RoomViewNameAr : RoomViewName; } }
        public int MealId { get; set; }
        public string? MealName { get; set; }
        public string? MealNameAr { get; set; }
        public string? MealNameLang { get { return Preferences.Default.Get("Lan", "en") == "ar" ? MealNameAr : MealName; } }
        public int RoomCount { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int NumberOfNights { get; set; }
        public string? Notes { get; set; }
    }
}
