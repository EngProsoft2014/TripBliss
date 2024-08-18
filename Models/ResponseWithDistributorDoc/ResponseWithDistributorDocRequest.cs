namespace TripBliss.Models
{
    public record ResponseWithDistributorDocRequest
    (
         int ResponseWithDistributorId ,
         string? NameDoc ,
         string? Notes ,
         string? UploadFile 
    );
}
