namespace TripBliss.Models.Visa
{
    public record VisaResponse
    {
        public int Id { get; set; }
        public string? VisaName { get; set; }
        public bool Active { get; set; }
    }
}
