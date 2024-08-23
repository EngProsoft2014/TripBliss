namespace TripBliss.Models.DistributorAssignVisa
{
    public record DistributorAssignVisaResponse
    {
        public int Id { get; set; }
        public string? DistributorCompanyId { get; set; }
        public int VisaId { get; set; }
        public int PersonCount { get; set; }
    }
}
