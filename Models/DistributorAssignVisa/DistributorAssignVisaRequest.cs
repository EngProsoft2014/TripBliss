namespace TripBliss.Models.DistributorAssignVisa
{
    public record DistributorAssignVisaRequest
    (
        string DistributorCompanyId,
        int VisaId,
        int PersonCount
    );

}
