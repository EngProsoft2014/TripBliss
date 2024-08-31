namespace TripBliss.Models
{
    public record ResponseWithDistributorHotel
    {
        public int Id { get; set; }
        public int RequestTravelAgencyId { get; set; }
        public int LocationId { get; set; }
        public string? LocationName { get; set; }
        public int HotelId { get; set; }
        public string? HotelName { get; set; }
        public int RoomTypeId { get; set; }
        public string? RoomTypeName { get; set; }
        public int RoomViewId { get; set; }
        public string? RoomViewName { get; set; }
        public int MealId { get; set; }
        public string? MealName { get; set; }
        public int RoomCount { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int NumberOfNights { get; set; }
        public string? Notes { get; set; }
    }
}
