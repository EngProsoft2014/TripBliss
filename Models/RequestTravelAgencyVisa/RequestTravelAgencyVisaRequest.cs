namespace TripBliss.Models.RequestTravelAgencyVisa
{
    public record RequestTravelAgencyVisaRequest
    {
        public int VisaId { get; set; }
        public int PersonCount { get; set; }
        public string? Notes { get; set; }
    }
}
