namespace TripBliss.Models;

public record CarBrandResponse
{
    public int Id { get; set; }
    public string? BrandName { get; set; }
    public string? BrandNameAr { get; set; }
    public string? BrandNameLang { get { return Preferences.Default.Get("Lan", "en") == "ar" ? BrandNameAr : BrandName; } }
    public int CarTypeId { get; set; }
    public string? CarTypename { get; set; }
    public string? CarTypenameAr { get; set; }
    public string? CarTypenameLang { get { return Preferences.Default.Get("Lan", "en") == "ar" ? CarTypenameAr : CarTypename; } }
    public bool Active { get; set; }

}
