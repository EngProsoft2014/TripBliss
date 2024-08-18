namespace TripBliss.Models
{
    public record TravelAgencyCompanyResponse
    (
         string Id,
         string? CompanyName,
         string? Address,
         string? Website,
         string? Phone,
         string? Email,
         string? Logo,
         DateOnly? ExpireDateAcc,
         bool? ShowAllDistributors,
         int? Review
    );
}
