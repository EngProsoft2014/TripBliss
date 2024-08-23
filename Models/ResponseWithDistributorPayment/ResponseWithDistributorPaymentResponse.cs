namespace TripBliss.Models
{
    public record ResponseWithDistributorPaymentResponse
    {
        public int Id { get; set; }
        public int ResponseWithDistributorId { get; set; }
        public int? PaymentMethod { get; set; }
        public int? dbcr { get; set; }
        public int? AmountPayment { get; set; }
        public string? Refnumber { get; set; }
        public string? Notes { get; set; }
    }
}
