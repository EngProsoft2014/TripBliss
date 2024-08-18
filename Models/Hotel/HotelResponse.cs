namespace TripBliss.Models
{
    public record HotelResponse
    (
         int Id,
         string HotelName,
         int LocationId,
         string LocationName,
         bool Active
    );
}
