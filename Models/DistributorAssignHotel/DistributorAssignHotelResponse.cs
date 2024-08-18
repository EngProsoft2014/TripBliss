namespace TripBliss.Models
{
    public record DistributorAssignHotelResponse
    (
         int Id,
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
