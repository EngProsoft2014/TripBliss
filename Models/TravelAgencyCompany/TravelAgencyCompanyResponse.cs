using System.ComponentModel;

namespace TripBliss.Models
{
    public record TravelAgencyCompanyResponse : INotifyPropertyChanged
    {
        public string Id { get; set; }
        public string? CompanyName { get; set; }
        public string? Address { get; set; }
        public string? Website { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Logo { get; set; }
        public string? UrlLogo { get; set; }
        public DateOnly? ExpireDateAcc { get; set; }
        public bool? ShowAllDistributors { get; set; }
        public int? Review { get; set; }
        public int CountReviewToTravelAgency { get; set; }
        public double ReviewToTravelAgency { get; set; }

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
