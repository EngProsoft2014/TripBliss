namespace TripBliss.Models
{
    public record ResponseWithDistributorTransportDetailsResponse
    (
         int Id,
         int ResponseWithDistributorTransportId,
         int TravelAgencyGuestId,
         string? PlateNumber,
         string? DriverPhone,
         string? Notes
    );
}
