using System.ComponentModel;

namespace TripBliss.Models
{
    public class ResponseWithDistributorAirFlightDetailsResponse : INotifyPropertyChanged
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
        public string? ResponseWithDistributorAirFlightId { get; set; }
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

        string? _Extension;
        public string? Extension
        {
            get
            {
                return _Extension;
            }
            set
            {
                _Extension = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Extension"));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
