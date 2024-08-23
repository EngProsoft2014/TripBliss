namespace TripBliss.Models
{
    public record DistributorCompanyServiceResponse
    {
        public int Id { get; set; }
        public string? DistributorCompanyId { get; set; }
        public int? ServiceType { get; set; }
    }
}
