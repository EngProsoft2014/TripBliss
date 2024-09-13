namespace TripBliss.Models
{
    public record ResponseWithDistributorPaymentResponse
    {
        public int Id { get; set; }
        public int ResponseWithDistributorId { get; set; }
        public string? RequestName { get; set; }
        public string? DistributorCompanyName { get; set; }
        public string? TravelAgencyCompanyName { get; set; }
        public int? PaymentMethod { get; set; }
        public int? dbcr { get; set; }
        public int? AmountPayment { get; set; }
        public string? Refnumber { get; set; }
        public string? Notes { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}

