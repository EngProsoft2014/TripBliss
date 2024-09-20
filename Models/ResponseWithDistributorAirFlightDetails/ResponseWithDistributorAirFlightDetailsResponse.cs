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
        public string ImgName { get; set; } = string.Empty;
        public string UrlImgName { get; set; } = string.Empty;
        public int UserCategory { get; set; }
        public string? TravelAgencyCompanyName { get; set; } = string.Empty;
        public string? DistributorCompanyName { get; set; } = string.Empty;

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

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
