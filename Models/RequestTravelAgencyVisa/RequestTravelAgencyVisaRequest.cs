namespace TripBliss.Models
{
    public record RequestTravelAgencyVisaRequest
    {
        public int VisaId { get; set; }
        public int PersonCount { get; set; }
        public DateOnly DateVisa { get; set; }
        public string? Notes { get; set; }
    }
}
