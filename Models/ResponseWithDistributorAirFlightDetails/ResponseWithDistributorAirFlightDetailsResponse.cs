using System.ComponentModel;

namespace TripBliss.Models
{
    public class ResponseWithDistributorAirFlightDetailsResponse : INotifyPropertyChanged
    {
        int? _Id;
        public int? Id
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
        public int? ResponseWithDistributorAirFlightId { get; set; }
        int? _TravelAgencyGuestId;
        public int? TravelAgencyGuestId
        {
            get
            {
                return _TravelAgencyGuestId;
            }
            set
            {
                _TravelAgencyGuestId = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("TravelAgencyGuestId"));
                }
            }
        }
        public string? Notes { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
