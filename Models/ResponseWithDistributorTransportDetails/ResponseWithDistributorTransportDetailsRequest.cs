namespace TripBliss.Models
{
    public record ResponseWithDistributorTransportDetailsRequest
    {
        public string? Id { get; set; }
        public string? ResponseWithDistributorTransportId { get; set; }
        //public int TravelAgencyGuestId { get; set; }
        public string? LeaderName { get; set; }
        public string? PlateNumber { get; set; }
        public string? DriverPhone { get; set; }
        public string? Notes { get; set; }
    }
}
