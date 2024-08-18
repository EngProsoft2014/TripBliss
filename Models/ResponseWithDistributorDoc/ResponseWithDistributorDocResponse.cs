namespace TripBliss.Models
{
    public record ResponseWithDistributorDocResponse
    (
         int Id,
         int ResponseWithDistributorId,
         string? NameDoc,
         string? Notes,
         string? UploadFile
    );
}
