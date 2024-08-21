namespace TripBliss.Models
{
    public record DistributorCompanyResponse
    {
        string? Id { get; set; }
        string? CompanyName { get; set; }
        string? Address { get; set; }
        string? Website { get; set; }
        string? Phone { get; set; }
        string? Email { get; set; }
        string? Policy { get; set; }
        string? Logo { get; set; }
        DateOnly? ExpireDateAcc { get; set; }
        bool? SendWithAllBulk { get; set; }
        string? StripeUsername { get; set; }
        string? StripePassword { get; set; }
        string? StripeSecretKey { get; set; }
        int? Review { get; set; }
    };
}
