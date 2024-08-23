namespace TripBliss.Models
{
    public record TravelAgencyGuestResponse
    {
        public int Id { get; set; }
        public string? TravelAgencyCompanyId { get; set; }
        public string? GuestName { get; set; }
        public string? Nationality { get; set; }
        public int? TypeIdNumber { get; set; }
        public string? IDNumber { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? AdultCount { get; set; }
        public string? ChildCount { get; set; }
        public string? InfantCount { get; set; }
    }
}
