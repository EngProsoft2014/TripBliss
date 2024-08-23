namespace TripBliss.Models
{
    public record RequestTravelAgencyAirFlightResponse
    {
        public int Id { get; set; }
        public int RequestTravelAgencyId { get; set; }
        public int AirFlightId { get; set; }
        public string? AirLine { get; set; }
        public int ClassAirFlightId { get; set; }
        public string? ClassName { get; set; }
        public DateTime Date { get; set; }
        public string? AirportFrom { get; set; }
        public string? AirportTo { get; set; }
        public TimeOnly ETA { get; set; }
        public TimeOnly ETD { get; set; }
        public int InfoAdultCount { get; set; }
        public int InfoChildCount { get; set; }
        public int InfoInfantCount { get; set; }
        public int TotalPerson { get; set; }
    }
}
