namespace TripBliss.Models
{
    public record TravelAgencyGuestDocResponse
    {
        public int Id { get; set; }
        public int TravelAgencyGuestId { get; set; }
        public string? NameDoc { get; set; }
        public string? UploadFile { get; set; }
        public string? Notes { get; set; }
    }
}
