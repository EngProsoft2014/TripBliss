using System.ComponentModel;

namespace TripBliss.Models
{
    public record ResponseWithDistributorTransportDetailsResponse : INotifyPropertyChanged
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
        public string? ResponseWithDistributorTransportId { get; set; }

        string? _LeaderName;
        public string? LeaderName
        {
            get
            {
                return _LeaderName;
            }
            set
            {
                _LeaderName = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LeaderName"));
                }
            }
        }
        string? _PlateNumber;
        public string? PlateNumber
        {
            get
            {
                return _PlateNumber;
            }
            set
            {
                _PlateNumber = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("PlateNumber"));
                }
            }
        }
        string? _DriverPhone;
        public string? DriverPhone
        {
            get
            {
                return _DriverPhone;
            }
            set
            {
                _DriverPhone = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("DriverPhone"));
                }
            }
        }
        public string? Notes { get; set; }
        public int? CountRow { get; set; }
        public string CreatedUser { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }


        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
