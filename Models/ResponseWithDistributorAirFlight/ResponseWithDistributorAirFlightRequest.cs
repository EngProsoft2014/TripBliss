﻿namespace TripBliss.Models
{
    public record ResponseWithDistributorAirFlightRequest
    {
        public string? Id { get; set; }
        public string? ResponseWithDistributorId { get; set; }
        public int RequestTravelAgencyAirFlightId { get; set; }
        public int PriceAdult { get; set; }
        public int PriceChild { get; set; }
        public int PriceInfant { get; set; }
        public int Total { get; set; }
        public string? Notes { get; set; }
        public bool AcceptPriceDis { get; set; }
        public bool AcceptAgen { get; set; }
        public string CreatedUser { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
    }
}
