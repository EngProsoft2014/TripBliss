namespace TripBliss.Models
{
    public record TravelAgencyCompanyDocRequest
    {
        public string? TravelAgencyCompanyId { get; set; }
        public string? NameDoc { get; set; }
        public string? Notes { get; set; }
        public string? UploadFile { get; set; }
    }
}
