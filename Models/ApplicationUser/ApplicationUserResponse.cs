using TripBliss.Constants;

namespace TripBliss.Models;

public record ApplicationUserResponse
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int UserCategory { get; set; }
    public int UserPermision { get; set; }
    public string? Token { get; set; }
    public int ExpiresIn { get; set; }
    public bool IsDisabled { get; set; }
    public List<PermissionsValues> Permissions { get; set; } = [];
    public string TravelAgencyCompanyId { get; set; }
    public TravelAgencyCompanyResponse TravelAgencyCompany { get; set; }
    public string? DistributorCompanyId { get; set; }
    public DistributorCompanyResponse DistributorCompany { get; set; }
}
//{
//    public string? Token { get; set; }
//    public int ExpiresIn { get; set; }

//}
