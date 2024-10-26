namespace TripBliss.Models
{
    public record TravelAgencywithDistributorsResponse
    {
        public string? Id { get; set; }
        public string? TravelAgencyCompanyId { get; set; }
        public string? DistributorCompanyId { get; set; }
        public bool Private { get; set; }
        public DistributorCompanyResponse? DistributorCompany { get; set; }
        public TravelAgencyCompanyResponse? TravelAgencyCompany { get; set; }
    }
}
