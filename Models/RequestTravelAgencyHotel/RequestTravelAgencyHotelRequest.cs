using System.ComponentModel;

namespace TripBliss.Models
{
    public record RequestTravelAgencyHotelRequest : INotifyPropertyChanged
    {
        public int RequestTravelAgencyId { get; set; }
        public int LocationId { get; set; }
        public int HotelId { get; set; }
        public int RoomTypeId { get; set; }
        public int RoomViewId { get; set; }
        public int MealId { get; set; }
        public int RoomCount
                {
                    get
                    {
                        return _RoomCount;
                    }
                    set
                    {
                        _RoomCount = value;
                        if (PropertyChanged != null)
                        {
                            PropertyChanged(this, new PropertyChangedEventArgs("RoomCount"));
                        }
                    }
                }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int NumberOfNights { get; set; }

        int _RoomCount;
        

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
