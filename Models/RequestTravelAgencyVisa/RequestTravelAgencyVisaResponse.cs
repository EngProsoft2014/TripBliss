namespace TripBliss.Models
{
    public record RequestTravelAgencyVisaResponse
    {
        public int Id { get; set; }
        public int RequestTravelAgencyId { get; set; }
        public int VisaId { get; set; }
        public string? VisaName { get; set; }
        public int PersonCount { get; set; }
        public string? Notes { get; set; }
    }
}
