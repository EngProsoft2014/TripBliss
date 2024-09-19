using System.ComponentModel;

namespace TripBliss.Models.ResponseWithDistributorVisaDetails
{
    public class ResponseWithDistributorVisaDetailsResponse : INotifyPropertyChanged
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
        public int? ResponseWithDistributorVisaId { get; set; }
        public string ImgName { get; set; } = string.Empty;
        public string UrlImgName { get; set; } = string.Empty;
        public string? Notes { get; set; }

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
