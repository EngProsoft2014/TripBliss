namespace TripBliss.Models
{
    public record RequestTravelAgencyAirFlightResponse
    {
        public string Id { get; set; } = string.Empty;
        public string? RequestTravelAgencyId { get; set; }
        public int AirFlightId { get; set; }
        public string? AirLine { get; set; }
        public string? AirLineAr { get; set; }
        public string? AirLineLang { get { return Preferences.Default.Get("Lan", "en") == "ar" ? AirLineAr : AirLine; } }
        public int ClassAirFlightId { get; set; }
        public string? ClassName { get; set; }
        public string? ClassNameAr { get; set; }
        public string? ClassNameLang { get { return Preferences.Default.Get("Lan", "en") == "ar" ? ClassNameAr : ClassName; } }
        public DateTime Date { get; set; }
        public string? AirportFrom { get; set; }
        public string? AirportTo { get; set; }
        public TimeSpan ETA { get; set; }
        public TimeSpan ETD { get; set; }
        public int InfoAdultCount { get; set; }
        public int InfoChildCount { get; set; }
        public int InfoInfantCount { get; set; }
        public int TotalPerson { get; set; }
        public string? Notes { get; set; }
    }
}
