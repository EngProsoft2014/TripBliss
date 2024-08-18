namespace TripBliss.Models
{
    public record RequestTravelAgencyTransportRequest
    (
         int? RequestTravelAgencyId ,
         int CarTypeId ,
         int CarBrandId ,
         int CarModelId ,
         DateOnly Date ,
         TimeOnly Time ,
         string FromLocation ,
         string ToLocation ,
         int TransportCount ,
         bool IsDriver 
    );
}
