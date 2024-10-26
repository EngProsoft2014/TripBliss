namespace TripBliss.Models
{
    public record TravelAgencyCompanyDocRequest
    {
        public string? Id { get; set; }
        public string? NameDoc { get; set; }
        public string? Notes { get; set; }
        public byte[]? ImgFile { get; set; } = default!;
        public string? Extension { get; set; }
    }
}
