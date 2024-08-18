namespace TripBliss.Models
{
    public record ResponseWithDistributorHotelRequest
    (
         int ResponseWithDistributorId ,
         int RequestTravelAgencyHotelId ,
         int Price ,
         int Total ,
         string? Notes ,
         bool AcceptPriceDis ,
         bool AcceptAgen 
    );
}
