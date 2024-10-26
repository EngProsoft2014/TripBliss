namespace TripBliss.Models
{
    public record DistributorCompanyServiceResponse
    {
        public string? Id { get; set; }
        public string? DistributorCompanyId { get; set; }
        public int? ServiceType { get; set; }
    }
}
