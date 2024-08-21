namespace TripBliss.Models
{
    public record DistributorCompanyResponse
    {

        public string? Id { get; set; }
        public string? CompanyName { get; set; }
        public string? Address { get; set; }
        public string? Website { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Policy { get; set; }
        public string? Logo { get; set; }
        public DateTime? ExpireDateAcc { get; set; }
        public bool SendWithAllBulk { get; set; }
        public string? StripeUsername { get; set; }
        public string? StripePassword { get; set; }
        public string? StripeSecretKey { get; set; }
        public int Review { get; set; }
    };
}
