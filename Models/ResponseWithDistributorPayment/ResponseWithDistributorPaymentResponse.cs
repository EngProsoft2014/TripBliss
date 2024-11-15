namespace TripBliss.Models
{
    public record ResponseWithDistributorPaymentResponse
    {
        public string? Id { get; set; }
        public string? ResponseWithDistributorId { get; set; }
        public string? RequestName { get; set; }
        public string? DistributorCompanyName { get; set; }
        public string? TravelAgencyCompanyName { get; set; }
        public int? PaymentMethod { get; set; }
        public int? dbcr { get; set; }
        public int? AmountPayment { get; set; }
        public string? Refnumber { get; set; }
        public string? ImgName { get; set; } = string.Empty;
        public string? UrlImgName { get; set; } = string.Empty;
        public bool? Active { get; set; }
        public string? Notes { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}

