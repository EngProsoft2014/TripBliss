using System.ComponentModel;

namespace TripBliss.Models
{
    public record ResponseWithDistributorHotelDetailsResponse : INotifyPropertyChanged
    {
        string? _Id;
        public string? Id
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Id"));
                }
            }
        }
        public string? ResponseWithDistributorHotelId { get; set; }
        public string? GuestName { get; set; }
        string? _RoomRef;
        public string? RoomRef
        {
            get
            {
                return _RoomRef;
            }
            set
            {
                _RoomRef = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("RoomRef"));
                }
            }
        }
        public string? Notes { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
