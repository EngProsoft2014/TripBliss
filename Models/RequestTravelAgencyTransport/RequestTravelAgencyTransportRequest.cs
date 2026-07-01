namespace TripBliss.Models
{
    public record RequestTravelAgencyTransportRequest
    {
        public int CarTypeId { get; set; }
        public int CarBrandId { get; set; }
        //public string? CarBrandName { get; set; }
        public int CarModelId { get; set; }
        //public string? CarModelName { get; set; }
        //public int Year { get; set; }
        public DateOnly Date { get; set; }
        public DateTime DateVM { get; set; }
        public TimeSpan Time { get; set; }
        public string? FromLocation { get; set; }
        public string? ToLocation { get; set; }
        public int TransportCount { get; set; }
        public bool IsDriver { get; set; }
        public string? Notes { get; set; }
    }
}
