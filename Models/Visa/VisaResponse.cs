namespace TripBliss.Models.Visa
{
    public record VisaResponse
    {
        public int Id { get; set; }
        public string? VisaName { get; set; }
        public string? VisaNameAr { get; set; }
        public string? VisaNameLang { get { return Preferences.Default.Get("Lan", "en") == "ar" ? VisaNameAr : VisaName; } }
        public bool Active { get; set; }
    }
}
