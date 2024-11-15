namespace TripBliss.Models
{
    public record ResponseWithDistributorPaymentRequest
    {
        public int? PaymentMethod { get; set; }
        public int? dbcr { get; set; }
        public int? AmountPayment { get; set; }
        public string? Refnumber { get; set; }
        public byte[]? ImgFile { get; set; }
        public string? Extension { get; set; } = string.Empty;
        public string? Cvc { get; set; }
        public string? CardholderName { get; set; }
        public int? ExpirationMonth { get; set; }
        public int? ExpirationYear { get; set; }
        public string? CardNumber { get; set; }
        public string? Notes { get; set; }

    }
}
