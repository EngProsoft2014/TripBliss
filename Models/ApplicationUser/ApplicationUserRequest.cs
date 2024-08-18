

namespace TripBliss.Models;
//ahmed fayez
public record ApplicationUserRequest
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int UserPermision { get; set; }
    public int UserCategory { get; set; }
    public string? TravelAgencyCompanyId { get; set; }
    public TravelAgencyCompanyRequest? TravelAgencyCompany { get; set; }
    public string? DistributorCompanyId { get; set; }
    public DistributorCompanyRequest? DistributorCompany { get; set; }
}
