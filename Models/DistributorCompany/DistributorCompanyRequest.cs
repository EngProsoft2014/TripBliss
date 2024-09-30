namespace TripBliss.Models
{
    public record DistributorCompanyRequest
    {
        public string? CompanyName { get; set; }
        public string? Address { get; set; }
        public string? Website { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Policy { get; set; }
        public string? Logo { get; set; }
        public DateOnly? ExpireDateAcc { get; set; }
        public string? StripeUsername { get; set; }
        public string? StripePassword { get; set; }
        public string? StripeSecretKey { get; set; }
        public byte[]? ImgFile { get; set; }
        public string? Extension { get; set; } = string.Empty;
        public int? Review { get; set; } = 0;
        public bool? SendWithAllBulk { get; set; } = true;

    }
}
