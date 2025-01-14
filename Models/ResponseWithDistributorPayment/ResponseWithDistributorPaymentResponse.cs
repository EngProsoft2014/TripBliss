using System.ComponentModel;

namespace TripBliss.Models
{
    public record ResponseWithDistributorPaymentResponse : INotifyPropertyChanged
    {
        public string? Id { get; set; }
        public string? ResponseWithDistributorId { get; set; }
        public string? RequestName { get; set; }
        public string? DistributorCompanyName { get; set; }
        public string? TravelAgencyCompanyName { get; set; }
        public int? PaymentMethod { get; set; }
        public int? dbcr { get; set; }
        public int? AmountPayment { get; set; }
        public string? Refnumber { get; set; }
        public string? ImgName { get; set; } = string.Empty;
        public string? UrlImgName { get; set; } = string.Empty;
        public string? UrlImgNameVM { get { return Helpers.Utility.ServerUrl + UrlImgName; } }
        bool? _Active;
        public bool? Active
        {
            get
            {
                return _Active;
            }
            set
            {
                _Active = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Active"));
                }
            }
        }
        public string? Notes { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? Complaint { get; set; }

        public bool? IsComplaintTR { get{ return !string.IsNullOrEmpty(Complaint) && Active == false ? true : false; }}

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}

