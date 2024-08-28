namespace TripBliss.Models
{
    public record RequestTravelAgencyTransportResponse
    {
        public int Id { get; set; }
        public int? RequestTravelAgencyId { get; set; }
        public int CarTypeId { get; set; }
        public string? TypeName { get; set; }
        public int CarBrandId { get; set; }
        public string? BrandName { get; set; }
        public int CarModelId { get; set; }
        public string? ModelName { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public string? FromLocation { get; set; }
        public string? ToLocation { get; set; }
        public int TransportCount { get; set; }
        public bool IsDriver { get; set; }
        public string? Notes { get; set; }
    }
}
