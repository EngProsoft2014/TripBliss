namespace TripBliss.Models
{
    public record RequestTravelAgencyTransportResponse
    {
        public string Id { get; set; } = string.Empty;
        public string? RequestTravelAgencyId { get; set; }
        public int CarTypeId { get; set; }
        public string? TypeName { get; set; }
        public string? TypeNameAr { get; set; }
        public string? TypeNameLang { get { return Preferences.Default.Get("Lan", "en") == "ar" ? TypeNameAr : TypeName; } }
        public int CarBrandId { get; set; }
        public string? BrandName { get; set; }
        public string? BrandNameAr { get; set; }
        public string? BrandNameLang { get { return Preferences.Default.Get("Lan", "en") == "ar" ? BrandNameAr : BrandName; } }
        public int CarModelId { get; set; }
        public string? ModelName { get; set; }
        public string? ModelNameAr { get; set; }
        public string? ModelNameLang { get { return Preferences.Default.Get("Lan", "en") == "ar" ? ModelNameAr : ModelName; } }
        public DateOnly Date { get; set; }
        public TimeSpan Time { get; set; }
        public string? FromLocation { get; set; }
        public string? ToLocation { get; set; }
        public int TransportCount { get; set; }
        public bool IsDriver { get; set; }
        public string? Notes { get; set; }
    }
}
