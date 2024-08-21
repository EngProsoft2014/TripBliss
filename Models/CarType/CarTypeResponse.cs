namespace TripBliss.Models;

public record CarTypeResponse
{
    public int Id { get; set; }
    public string? TypeName { get; set; }
    public bool Active { get; set; }


}
