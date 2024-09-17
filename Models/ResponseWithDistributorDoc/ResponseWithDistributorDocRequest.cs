namespace TripBliss.Models
{
    public record ResponseWithDistributorDocRequest
    {
        //public int ResponseWithDistributorId { get; set; }
        //public string? NameDoc { get; set; }
        //public string? Notes { get; set; }
        //public string? UploadFile { get; set; }

        //public string? NameDoc { get; set; }
        //public string? Notes { get; set; }
        //public IFormFile FileDetails { get; set; } = default!;

        public string? NameDoc { get; set; }
        public string? Notes { get; set; }
        public byte[] FileDetails { get; set; } = default!;
    }
}
