namespace TripBliss.Models;

public record AirFlightResponse
{
    public int Id{ get; set; }
    public string? AirLine { get; set; }
    public string? AirLineAr { get; set; }
    public string? AirLineLang { get { return Preferences.Default.Get("Lan", "en") == "ar" ? AirLineAr : AirLine; } }
    public bool Active { get; set; }

    
}
