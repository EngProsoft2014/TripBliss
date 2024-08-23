namespace TripBliss.Models.DistributorAssignVisa
{
    public record DistributorAssignVisaRequest
    {
        public string? DistributorCompanyId { get; set; }
        public int VisaId { get; set; }
        public int PersonCount { get; set; }
    }

}
