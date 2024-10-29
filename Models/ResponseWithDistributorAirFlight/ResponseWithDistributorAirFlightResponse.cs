using System.ComponentModel;

namespace TripBliss.Models
{
    public record ResponseWithDistributorAirFlightResponse : INotifyPropertyChanged
    {
        public string? Id { get; set; }
        public string? ResponseWithDistributorId { get; set; }
        public string? RequestTravelAgencyAirFlightId { get; set; }
        public int PriceAdult { get; set; }
        public int PriceChild { get; set; }
        public int PriceInfant { get; set; }
        int _Total;
        public int Total
        {
            get
            {
                return _Total;
            }
            set
            {
                _Total = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Total"));
                }
            }
        }
        public string? Notes { get; set; }
        bool _AcceptPriceDis;
        public bool AcceptPriceDis
        {
            get
            {
                return _AcceptPriceDis;
            }
            set
            {
                _AcceptPriceDis = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("AcceptPriceDis"));
                }
            }
        }

        bool _AcceptAgen;
        public bool AcceptAgen
        {
            get
            {
                return _AcceptAgen;
            }
            set
            {
                _AcceptAgen = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("AcceptAgen"));
                }
            }
        }
        public string CreatedUser { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public RequestTravelAgencyAirFlightResponse RequestTravelAgencyAirFlight { get; set; } = default!;

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
