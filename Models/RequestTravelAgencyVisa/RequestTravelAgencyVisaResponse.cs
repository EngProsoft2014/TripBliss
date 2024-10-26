namespace TripBliss.Models
{
    public record RequestTravelAgencyVisaResponse
    {
        public string Id { get; set; } = string.Empty;
        public string? RequestTravelAgencyId { get; set; }
        public int VisaId { get; set; }
        public string? VisaName { get; set; }
        public string? VisaNameAr { get; set; }
        public string? VisaNameLang { get { return Preferences.Default.Get("Lan", "en") == "ar" ? VisaNameAr : VisaName; } }
        public int PersonCount { get; set; }
        public string? Notes { get; set; }
    }
}
