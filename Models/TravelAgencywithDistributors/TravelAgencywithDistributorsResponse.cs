namespace TripBliss.Models
{
    public record TravelAgencywithDistributorsResponse
    {
        public int Id { get; set; }
        public string? TravelAgencyCompanyId { get; set; }
        public string? DistributorCompanyId { get; set; }
        public bool Private { get; set; }
    }
}
