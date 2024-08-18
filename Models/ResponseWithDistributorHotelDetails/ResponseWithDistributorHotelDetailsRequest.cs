namespace TripBliss.Models
{
    public record ResponseWithDistributorHotelDetailsRequest
    (
         int ResponseWithDistributorHotelId ,
         int TravelAgencyGuestId ,
         string? RoomRef ,
         string? Notes 
    );
}
