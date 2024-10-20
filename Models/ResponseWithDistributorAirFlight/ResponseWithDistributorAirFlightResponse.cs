﻿using System.ComponentModel;

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
        public bool AcceptPriceDis { get; set; }
        public bool AcceptAgen { get; set; }
        public RequestTravelAgencyAirFlightResponse RequestTravelAgencyAirFlight { get; set; } = default!;

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
