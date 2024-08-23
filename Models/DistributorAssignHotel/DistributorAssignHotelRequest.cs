namespace TripBliss.Models
{
    public record DistributorAssignHotelRequest
    {
        public string? DistributorCompanyId { get; set; }
        public int LocationId { get; set; }
        public int HotelId { get; set; }
        public int RoomTypeId { get; set; }
        public int RoomViewId { get; set; }
        public int MealId { get; set; }
        public int Count { get; set; }
        public bool IsTrackCount { get; set; }
        public bool IsTrackingDate { get; set; }
    }
}
