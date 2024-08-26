using System.ComponentModel;

namespace TripBliss.Models
{
    public record RequestTravelAgencyAirFlightRequest : INotifyPropertyChanged
    {
        //public int Id { get; set; }
        public int AirFlightId { get; set; }
        public int ClassAirFlightId { get; set; }
        public DateTime Date { get; set; }
        public string AirportFrom { get; set; }
        public string AirportTo { get; set; }
        public TimeSpan ETA { get; set; }
        public TimeSpan ETD { get; set; }
        int _InfoAdultCount;
        public int InfoAdultCount
        {
            get
            {
                return _InfoAdultCount;
            }
            set
            {
                _InfoAdultCount = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("InfoAdultCount"));
                }
            }
        }

        int _InfoChildCount;
        public int InfoChildCount
        {
            get
            {
                return _InfoChildCount;
            }
            set
            {
                _InfoChildCount = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("InfoChildCount"));
                }
            }
        }

        int _InfoInfantCount;
        public int InfoInfantCount
        {
            get
            {
                return _InfoInfantCount;
            }
            set
            {
                _InfoInfantCount = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("InfoInfantCount"));
                }
            }
        }
        public int TotalPerson { get; set; }
        public string? Notes { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
    };
}
