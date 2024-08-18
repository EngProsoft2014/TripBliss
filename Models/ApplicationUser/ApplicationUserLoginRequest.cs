namespace TripBliss.Models;

public record ApplicationUserLoginRequest
{
    public string? UserName { get; set; }
    public string? Password { get; set; }
}
