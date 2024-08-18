namespace TripBliss.Models
{
    public record DistributorCompanyResponse
    (
         string Id,
         string? CompanyName,
         string? Address,
         string? Website,
         string? Phone,
         string? Email,
         string? Policy,
         string? Logo,
         DateOnly? ExpireDateAcc,
         bool? SendWithAllBulk,
         string? StripeUsername,
         string? StripePassword,
         string? StripeSecretKey,
         int? Review
    );
}
