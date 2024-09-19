namespace TripBliss.Models.ResponseWithDistributorVisaDetails
{
    public record ResponseWithDistributorVisaDetailsRequest
    {
        public int Id { get; set; }
        public int ResponseWithDistributorVisaId { get; set; }
        public byte[] ImgFile { get; set; } = default!;
    }
}
