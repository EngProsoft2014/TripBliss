namespace TripBliss.Models
{
    public record ResponseWithDistributorDocResponse
    {
        public int Id { get; set; }
        public string DistributorCompanyId { get; set; } = string.Empty;
        public string DistributorCompanyName { get; set; } = string.Empty;
        public string? NameDoc { get; set; }
        public string? Notes { get; set; }
        public string? UploadFile { get; set; }
        public string UrlUploadFile { get; set; } = string.Empty;
    }
}
