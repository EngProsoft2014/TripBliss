namespace TripBliss.Models
{
    public record TravelAgencyCompanyDocResponse
    {
        public int Id { get; set; }
        public string? TravelAgencyCompanyId { get; set; }
        public string? NameDoc { get; set; }
        public string? Extension { get; set; }
        public string? Notes { get; set; }
        public string? ImgFile { get; set; }
        public string? UploadFile { get; set; }
        public string? UrlUploadFile { get; set; }
    }
}
