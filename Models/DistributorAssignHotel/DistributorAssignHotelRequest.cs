namespace TripBliss.Models
{
    public record DistributorAssignHotelRequest
    (
         string DistributorCompanyId,
         int LocationId,
         int HotelId,
         int RoomTypeId,
         int RoomViewId,
         int MealId,
         int Count,
         bool IsTrackCount,
         bool IsTrackingDate
    );
}
