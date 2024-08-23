namespace TripBliss.Models
{
    public record DistributorCompanyDocRequest
    {
        public string? DistributorCompanyId { get; set; }
        public string? NameDoc { get; set; }
        public string? Notes { get; set; }
        public string? UploadFile { get; set; }
    }
}
