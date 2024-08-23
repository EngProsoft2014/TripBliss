namespace TripBliss.Models
{
    public record ResponseWithDistributorTransportDetailsResponse
    {
        public int Id { get; set; }
        public int ResponseWithDistributorTransportId { get; set; }
        public int TravelAgencyGuestId { get; set; }
        public string? PlateNumber { get; set; }
        public string? DriverPhone { get; set; }
        public string? Notes { get; set; }
    }
}
