﻿using System.ComponentModel;
using TripBliss.Models.ResponseWithDistributor;

namespace TripBliss.Models
{
    public record DistributorCompanyResponse : INotifyPropertyChanged
    {

        public string Id { get; set; }
        public string? CompanyName { get; set; }
        string _Address;
        public string Address
        {
            get
            {
                return _Address;
            }
            set
            {
                _Address = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Address"));
                }
            }
        }
        public string locationlatitude { get; set; }
        public string locationlongitude { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalcodeZIP { get; set; }
        public string? Website { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Policy { get; set; }
        public string? Logo { get; set; }
        public string UrlLogo { get; set; } = string.Empty;
        public DateOnly? ExpireDateAcc { get; set; }
        public bool? SendWithAllBulk { get; set; }
        public string? StripeUsername { get; set; }
        public string? StripePassword { get; set; }
        public string? StripeSecretKey { get; set; }
        public string? BankName { get; set; }
        public string? BankAccounNumber { get; set; }
        public int? Review { get; set; }
        public byte[]? ImgFile { get; set; }
        public string? Extension { get; set; } = string.Empty;
        public int CountReviewToDistributor { get; set; }
        public double ReviewToDistributor { get; set; }
        public IList<ResponseWithDistributorReviewTravelAgentResponse> responseWithDistributorReviewTravelAgentResponses { get; set; } = [];

        ImageSource? _ImageFile;
        public ImageSource? ImageFile
        {
            get
            {
                return _ImageFile;
            }
            set
            {
                _ImageFile = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ImageFile"));
                }
            }
        }

        bool? _Favourite;
        public bool? Favourite
        {
            get
            {
                return _Favourite;
            }
            set
            {
                _Favourite = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Favourite"));
                }
            }
        }

        bool _IsSelected;
        public bool IsSelected
        {
            get
            {
                return _IsSelected;
            }
            set
            {
                _IsSelected = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("IsSelected"));
                }
            }
        }
        

        public event PropertyChangedEventHandler? PropertyChanged;
    };
}
