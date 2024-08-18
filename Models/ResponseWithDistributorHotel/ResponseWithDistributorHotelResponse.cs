namespace TripBliss.Models
{
    public record ResponseWithDistributorHotelResponse
    (
         int Id,
         int ResponseWithDistributorId,
         int RequestTravelAgencyHotelId,
         int Price,
         int Total,
         string? Notes,
         bool AcceptPriceDis,
         bool AcceptAgen
    );
}
