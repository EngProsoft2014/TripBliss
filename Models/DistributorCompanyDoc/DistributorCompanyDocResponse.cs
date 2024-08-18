namespace TripBliss.Models
{
    public record DistributorCompanyDocResponse
    (
         int Id,
         string DistributorCompanyId,
         string? NameDoc,
         string? Notes,
         string? UploadFile
    );
}
