using CommunityToolkit.Mvvm.ComponentModel;
using TripBliss.Constants;

namespace TripBliss.ViewModels
{
    public partial class BaseViewModel: ObservableObject
    {
        public BaseViewModel()
        {
            Lang = Preferences.Default.Get("Lan", "en");
            Review = Preferences.Default.Get(ApiConstants.review, int.Parse("0"));
            IsBusy = true;
            checkTOD();
        }

        [ObservableProperty]
        public bool isBusy = true;

        [ObservableProperty]
        public string lang;
        [ObservableProperty]
        string tOD;
        [ObservableProperty]
        int review;

        void checkTOD()
        {
            string TId = Preferences.Default.Get(ApiConstants.travelAgencyCompanyId, "");
            string DId = Preferences.Default.Get(ApiConstants.distributorCompanyId, "");
            
            if (!string.IsNullOrEmpty(TId))
            {
                TOD = "T";
            }
            else if(!string.IsNullOrEmpty(DId))
            {
                TOD = "D";
            }
        }
    }
}
