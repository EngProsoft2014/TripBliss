namespace TripBliss.Models.ResponseWithDistributorVisaDetails
{
    public record ResponseWithDistributorVisaDetailsResponse
    {
        public int Id { get; set; }
        public int ResponseWithDistributorVisaId { get; set; }
        public int TravelAgencyGuestId { get; set; }
        public string? Notes { get; set; }
    }
}
