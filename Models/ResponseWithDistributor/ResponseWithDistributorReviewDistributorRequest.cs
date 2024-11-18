using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripBliss.Models.ResponseWithDistributor
{
    public class ResponseWithDistributorReviewDistributorRequest : INotifyPropertyChanged
    {
        int? _ReviewToTravelAgency;
        public int? ReviewToTravelAgency
        {
            get
            {
                return _ReviewToTravelAgency;
            }
            set
            {
                _ReviewToTravelAgency = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ReviewToTravelAgency"));
                }
            }
        }

        string? _ReviewDistributorNote;
        public string? ReviewDistributorNote
        {
            get
            {
                return _ReviewDistributorNote;
            }
            set
            {
                _ReviewDistributorNote = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ReviewDistributorNote"));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
