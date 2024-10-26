using System.ComponentModel;

namespace TripBliss.Models
{
    public record TravelAgencyCompanyDocResponse : INotifyPropertyChanged
    {
        public string? Id { get; set; }
        public string TravelAgencyCompanyId { get; set; } = string.Empty;
        public string TravelAgencyCompanyName { get; set; } = string.Empty;
        public string? NameDoc { get; set; }
        public string? Notes { get; set; } 

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

        public string ImgFile { get; set; } = default!;
        public string? UploadFile { get; set; }
        public string UrlUploadFile { get; set; } = string.Empty;
        public bool? UploadFileModify { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
