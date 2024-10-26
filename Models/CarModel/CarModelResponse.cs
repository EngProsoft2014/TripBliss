namespace TripBliss.Models;

public record CarModelResponse
{
    public int Id { get; set; }
    public string? ModelName { get; set; }
    public string? ModelNameAr { get; set; }
    public string? ModelNameLang { get { return Preferences.Default.Get("Lan", "en") == "ar" ? ModelNameAr : ModelName; } }
    public string? CarYear { get; set; }
    public int CarBrandId { get; set; }
    public string? CarBrandName { get; set; }
    public string? CarBrandNameAr { get; set; }
    public string? CarBrandNameLang { get { return Preferences.Default.Get("Lan", "en") == "ar" ? CarBrandNameAr : CarBrandName; } }
    public bool Active { get; set; }

    
}
