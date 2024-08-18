namespace TripBliss.Models
{
    public record ResponseWithDistributorHotelDetailsResponse
    (
         int Id,
         int ResponseWithDistributorHotelId,
         int TravelAgencyGuestId,
         string? RoomRef,
         string? Notes
    );
}
