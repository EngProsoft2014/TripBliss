namespace TripBliss.Models
{
    public record ResponseWithDistributorHotelResponse
    {
        public int Id { get; set; }
        public int ResponseWithDistributorId { get; set; }
        public int RequestTravelAgencyHotelId { get; set; }
        public int Price { get; set; }
        public int Total { get; set; }
        public string? Notes { get; set; }
        public bool AcceptPriceDis { get; set; }
        public bool AcceptAgen { get; set; }
        public RequestTravelAgencyHotelResponse RequestTravelAgencyHotel { get; set; } = default!;
    }
}
