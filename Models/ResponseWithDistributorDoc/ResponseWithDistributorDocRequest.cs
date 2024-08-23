namespace TripBliss.Models
{
    public record ResponseWithDistributorDocRequest
    {
        public int ResponseWithDistributorId { get; set; }
        public string? NameDoc { get; set; }
        public string? Notes { get; set; }
        public string? UploadFile { get; set; }
    }
}
