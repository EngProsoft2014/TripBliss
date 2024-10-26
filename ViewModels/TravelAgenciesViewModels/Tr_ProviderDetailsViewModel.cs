using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using TripBliss.Constants;
using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.Pages.Shared;

namespace TripBliss.ViewModels.TravelAgenciesViewModels
{
    public partial class Tr_ProviderDetailsViewModel : BaseViewModel
    {
        #region Prop
        [ObservableProperty]
        DistributorCompanyResponse distributorModel = new DistributorCompanyResponse();
        #endregion

        #region Services
        readonly Services.Data.ServicesService _service;
        IGenericRepository Rep;
        #endregion

        #region Cons
        public Tr_ProviderDetailsViewModel()
        {

        }

        public Tr_ProviderDetailsViewModel(string id, IGenericRepository generic, Services.Data.ServicesService service)
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
            IsBusy = true;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();
                if (!string.IsNullOrEmpty(UserToken))
                {
                    UserDialogs.Instance.ShowLoading();
                    var json = await Rep.GetAsync<DistributorCompanyResponse>(ApiConstants.GetDistCompanyDetailsApi + id, UserToken);
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

            IsBusy = false;
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
