namespace TripBliss.Models;

public record CarTypeResponse
{
    public int Id { get; set; }
    public string? TypeName { get; set; }
    public string? TypeNameAr { get; set; }
    public string? TypeNameLang { get { return Preferences.Default.Get("Lan", "en") == "ar" ? TypeNameAr : TypeName; } }
    public bool Active { get; set; }


}
