namespace TripBliss.Models.DistributorAssignVisa
{
    public record DistributorAssignVisaResponse
    (
        int Id,
        string DistributorCompanyId,
        int VisaId,
        int PersonCount
    );
}
