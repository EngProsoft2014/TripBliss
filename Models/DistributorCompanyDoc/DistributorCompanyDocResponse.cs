using System.ComponentModel;

namespace TripBliss.Models
{
    public record DistributorCompanyDocResponse : INotifyPropertyChanged
    {
        public string? Id { get; set; }
        public string DistributorCompanyId { get; set; } = string.Empty;
        public string DistributorCompanyName { get; set; } = string.Empty;
        public string? NameDoc { get; set; }
        public string? Notes { get; set; }
        public string ImgFile { get; set; } = default!;
        public string? UploadFile { get; set; }
        public string UrlUploadFile { get; set; } = string.Empty;
        public bool? UploadFileModify { get; set; }

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
