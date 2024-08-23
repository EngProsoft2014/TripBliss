namespace TripBliss.Models
{
    public record DistributorCompanyServiceRequest
    {
        public string? DistributorCompanyId { get; set; }
        public int? ServiceType { get; set; }
    }
}
