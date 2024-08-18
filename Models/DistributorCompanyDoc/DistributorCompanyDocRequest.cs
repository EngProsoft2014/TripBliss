namespace TripBliss.Models
{
    public record DistributorCompanyDocRequest
    (
         string DistributorCompanyId ,
         string? NameDoc ,
         string? Notes ,
         string? UploadFile 
    );
}
