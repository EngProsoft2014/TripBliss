using Android.Media.TV;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using TripBliss.Constants;
using TripBliss.Helpers;
using TripBliss.Models;

namespace TripBliss.ViewModels.DistributorsViewModels
{
    public partial class Dis_ProviderDetailsViewModel : BaseViewModel
    {
        #region Prop
        [ObservableProperty]
        TravelAgencyCompanyResponse distributorModel = new TravelAgencyCompanyResponse();
        #endregion

        #region Services
        readonly Services.Data.ServicesService _service;
        IGenericRepository Rep;
        #endregion

        #region Cons
        public Dis_ProviderDetailsViewModel(IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
        }

        public Dis_ProviderDetailsViewModel(string id, IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
            Init(id);
        }
        #endregion

        #region Methods
        async void Init(string id)
        {
            await GetCompanyDetiles(id);
        }

        async Task GetCompanyDetiles(string id)
        {
            IsBusy = false;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                
                string UserToken = await _service.UserToken();
                if (!string.IsNullOrEmpty(UserToken))
                {
                    UserDialogs.Instance.ShowLoading();
                    var json = await Rep.GetAsync<TravelAgencyCompanyResponse>(ApiConstants.GetTravelCompanyDetailsApi + id, UserToken);
                    UserDialogs.Instance.HideHud();
                    if (json != null)
                    {
                        if (string.IsNullOrEmpty(json.UrlLogo))
                        {
                            json.UrlLogo = "";
                        }
                        else
                        {
                            json.UrlLogo = $"{Helpers.Utility.ServerUrl}{json.UrlLogo}";
                        }
                        DistributorModel = json;

                    }
                }

            }

            IsBusy = true;
        }

        #endregion

        #region RelayCommand
        [RelayCommand]
        async Task BackPressed()
        {
            await App.Current!.MainPage!.Navigation.PopAsync();
        } 
        #endregion
    }
}
