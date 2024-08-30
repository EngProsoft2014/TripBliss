using System.ComponentModel;

namespace TripBliss.Models
{
    public record ResponseWithDistributorTransportResponse : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int ResponseWithDistributorId { get; set; }
        public int RequestTravelAgencyTransportId { get; set; }
        int _Price;
        public int Price
        {
            get
            {
                return _Price;
            }
            set
            {
                _Price = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Price"));
                }
            }
        }
        public int Total { get; set; }
        public string? Notes { get; set; }
        public bool AcceptPriceDis { get; set; }
        public bool AcceptAgen { get; set; }
        public RequestTravelAgencyTransportResponse RequestTravelAgencyTransport { get; set; } = default!;
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
