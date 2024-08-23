namespace TripBliss.Models.ResponseWithDistributorVisaDetails
{
    public record ResponseWithDistributorVisaDetailsRequest
    {
        public int ResponseWithDistributorVisaId { get; set; }
        public int TravelAgencyGuestId { get; set; }
        public string? Notes { get; set; }
    }
}
