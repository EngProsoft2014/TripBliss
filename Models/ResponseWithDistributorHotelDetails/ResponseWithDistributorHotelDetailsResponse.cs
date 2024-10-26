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

        string? _GuestName;
        public string? GuestName
        {
            get
            {
                return _GuestName;
            }
            set
            {
                _GuestName = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("GuestName"));
                }
            }
        }
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
        public int? CountRow { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
