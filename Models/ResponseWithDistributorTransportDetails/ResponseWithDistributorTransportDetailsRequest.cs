namespace TripBliss.Models
{
    public record ResponseWithDistributorTransportDetailsRequest
    (
         int ResponseWithDistributorTransportId ,
         int TravelAgencyGuestId ,
         string? PlateNumber ,
         string? DriverPhone ,
         string? Notes 
    );
}
