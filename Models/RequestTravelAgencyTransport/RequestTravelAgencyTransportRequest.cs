﻿namespace TripBliss.Models
{
    public record RequestTravelAgencyTransportRequest
    {
        public int? RequestTravelAgencyId { get; set; }
        public int CarTypeId { get; set; }
        public int CarBrandId { get; set; }
        public int CarModelId { get; set; }
        public DateTime Date { get; set; }
        public TimeOnly Time { get; set; }
        public string? FromLocation { get; set; }
        public string? ToLocation { get; set; }
        public int TransportCount { get; set; }
        public bool IsDriver { get; set; }
    }
}
