using System.ComponentModel;
using TripBliss.Models.ResponseWithDistributor;

namespace TripBliss.Models
{
    public record TravelAgencyCompanyResponse : INotifyPropertyChanged
    {
        public string? Id { get; set; }
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
        public string? Logo { get; set; }
        public string UrlLogo { get; set; } = string.Empty;
        public DateOnly? ExpireDateAcc { get; set; }
        public bool? ShowAllDistributors { get; set; }
        public int? Review { get; set; }
        public int CountReviewToTravelAgency { get; set; }
        public double ReviewToTravelAgency { get; set; }
        public IList<ResponseWithDistributorReviewDistributorResponse> responseWithDistributorReviewDistributorResponses { get; set; } = [];

        public byte[]? ImgFile { get; set; }
        public string? Extension { get; set; } = string.Empty;

        ImageSource? _ImageFile;

        public event PropertyChangedEventHandler? PropertyChanged;

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


    }
}
