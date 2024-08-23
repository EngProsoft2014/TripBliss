namespace TripBliss.Models
{
    public record TravelAgencyGuestDocRequest
    {
        public int TravelAgencyGuestId { get; set; }
        public string? NameDoc { get; set; }
        public string? UploadFile { get; set; }
        public string? Notes { get; set; }
    }
}
