namespace TripBliss.Models
{
    public record RequestTravelAgencyHotelRequest
    {
        public int RequestTravelAgencyId { get; set; }
        public int LocationId { get; set; }
        public int HotelId { get; set; }
        public int RoomTypeId { get; set; }
        public int RoomViewId { get; set; }
        public int MealId { get; set; }
        public int RoomCount { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int NumberOfNights { get; set; }
    }
}
