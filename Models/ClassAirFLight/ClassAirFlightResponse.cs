namespace TripBliss.Models;

public record ClassAirFlightResponse
{
    public int Id { get; set; }
    public string? ClassName { get; set; }
    public string? ClassNameAr { get; set; }
    public string? ClassNameLang { get { return Preferences.Default.Get("Lan", "en") == "ar" ? ClassNameAr : ClassName; } }
    public bool Active { get; set; }

}
