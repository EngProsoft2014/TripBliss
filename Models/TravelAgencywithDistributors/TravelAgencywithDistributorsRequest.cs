namespace TripBliss.Models
{
    public record TravelAgencywithDistributorsRequest
    {
        public string? TravelAgencyCompanyId { get; set; }
        public string? DistributorCompanyId { get; set; }
        public bool Private { get; set; }
    }
}
