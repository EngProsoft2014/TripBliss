namespace TripBliss.Models
{
    public record DistributorCompanyDocResponse
    {
        public int Id { get; set; }
        public string? DistributorCompanyId { get; set; }
        public string? NameDoc { get; set; }
        public string? Notes { get; set; }
        public string? UploadFile { get; set; }
    }
}
