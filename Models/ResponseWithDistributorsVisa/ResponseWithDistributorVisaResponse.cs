﻿using System.ComponentModel;

namespace TripBliss.Models
{
    public record ResponseWithDistributorVisaResponse : INotifyPropertyChanged
    {

        public string? Id { get; set; }
        public string? ResponseWithDistributorId { get; set; }
        public string? RequestTravelAgencyVisaId { get; set; }
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
        public RequestTravelAgencyVisaResponse RequestTravelAgencyVisa { get; set; } = default!;

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}

