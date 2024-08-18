namespace TripBliss.Models
{
    public record ResponseWithDistributorPaymentResponse
    (
         int Id,
         int ResponseWithDistributorId,
         int? PaymentMethod,
         int? dbcr,
         int? AmountPayment,
         string? Refnumber,
         string? Notes
    );
}
