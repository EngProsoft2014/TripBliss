using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Constants;
using TripBliss.Helpers;
using TripBliss.Models;

namespace TripBliss.ViewModels.ActivateViewModels
{
    public partial class TransportActivateViewModel : BaseViewModel
    {
        #region Prop
        [ObservableProperty]
        ResponseWithDistributorTransportDetailsResponse model = new ResponseWithDistributorTransportDetailsResponse();
        [ObservableProperty]
        ObservableCollection<TravelAgencyGuestResponse> guests = new ObservableCollection<TravelAgencyGuestResponse>();
        [ObservableProperty]
        TravelAgencyGuestResponse selectedGuest = new TravelAgencyGuestResponse();
        #endregion

        #region Services
        readonly Services.Data.ServicesService _service;
        IGenericRepository Rep;
        #endregion

        #region Cons
        public TransportActivateViewModel(ResponseWithDistributorTransportDetailsResponse detailsResponse, IGenericRepository generic, Services.Data.ServicesService service)
        {
            _service = service;
            Rep = generic;
            Model = detailsResponse;
            //Init();
        } 
        #endregion

        #region RelayCommand
        [RelayCommand]
        async Task Apply()
        {
            Model.TravelAgencyGuestId = selectedGuest?.Id ?? 0;
            await App.Current!.MainPage!.Navigation.PopAsync();
        }

        [RelayCommand]
        async Task BackClicked()
        {
            await App.Current!.MainPage!.Navigation.PopAsync();
        } 
        #endregion

        #region Methods
        async void Init()
        {
            await GetAllGuests();  
        }

        async Task GetAllGuests()
        {
            IsBusy = true;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string TravelAgencyId = Preferences.Default.Get(ApiConstants.travelAgencyCompanyId, "");
                string DistributorId = Preferences.Default.Get(ApiConstants.distributorCompanyId, "");
                if (!string.IsNullOrEmpty(TravelAgencyId) && string.IsNullOrEmpty(DistributorId))
                {
                    string UserToken = await _service.UserToken();
                    if (!string.IsNullOrEmpty(UserToken))
                    {
                        UserDialogs.Instance.ShowLoading();
                        var json = await Rep.GetAsync<ObservableCollection<TravelAgencyGuestResponse>>(ApiConstants.GuestApi + $"{TravelAgencyId}/TravelAgencyGuest", UserToken);
                        UserDialogs.Instance.HideHud();
                        if (json != null)
                        {
                            Guests!.Clear();
                            Guests = json;
                            SelectedGuest = Guests?.FirstOrDefault(g => g.Id == Model.TravelAgencyGuestId) ?? new TravelAgencyGuestResponse();
                        }
                    }
                }
            }

            IsBusy = false;
        }
        #endregion
    }
}
