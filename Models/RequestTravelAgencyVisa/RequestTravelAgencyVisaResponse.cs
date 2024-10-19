namespace TripBliss.Models
{
    public record RequestTravelAgencyVisaResponse
    {
        public string Id { get; set; } = string.Empty;
        public string? RequestTravelAgencyId { get; set; }
        public int VisaId { get; set; }
        public string? VisaName { get; set; }
        public int PersonCount { get; set; }
        public string? Notes { get; set; }
    }
}
