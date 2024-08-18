namespace TripBliss.Models
{
    public record ResponseWithDistributorPaymentRequest
    (
         int ResponseWithDistributorId ,
         int? PaymentMethod ,
         int? dbcr,
         int? AmountPayment ,
         string? Refnumber ,
         string? Notes 
    );
}
