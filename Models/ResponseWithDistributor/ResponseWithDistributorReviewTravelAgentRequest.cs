using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripBliss.Models.ResponseWithDistributor
{
    public class ResponseWithDistributorReviewTravelAgentRequest : INotifyPropertyChanged
    {
        int? _ReviewToDistributor;
        public int? ReviewToDistributor
        {
            get
            {
                return _ReviewToDistributor;
            }
            set
            {
                _ReviewToDistributor = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ReviewToDistributor"));
                }
            }
        }

        string? _ReviewTravelAgentNote;
        public string? ReviewTravelAgentNote
        {
            get
            {
                return _ReviewTravelAgentNote;
            }
            set
            {
                _ReviewTravelAgentNote = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ReviewTravelAgentNote"));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
