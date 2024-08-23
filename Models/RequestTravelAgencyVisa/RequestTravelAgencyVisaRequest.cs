namespace TripBliss.Models.RequestTravelAgencyVisa
{
    public record RequestTravelAgencyVisaRequest
    {
        public int RequestTravelAgencyId { get; set; }
        public int VisaId { get; set; }
        public int PersonCount { get; set; }
    }
}
