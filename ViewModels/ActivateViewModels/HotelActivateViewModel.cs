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
    public partial class HotelActivateViewModel : BaseViewModel
    {
        #region prop
        [ObservableProperty]
        ResponseWithDistributorHotelDetailsResponse detailsResponse = new ResponseWithDistributorHotelDetailsResponse();
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
        public HotelActivateViewModel(ResponseWithDistributorHotelDetailsResponse model, IGenericRepository generic, Services.Data.ServicesService service)
        {
            _service = service;
            Rep = generic;
            DetailsResponse = model;
            Init();
        } 
        #endregion

        #region RelayCommand
        [RelayCommand]
        async Task ApplyClicked()
        {
            DetailsResponse.TravelAgencyGuestId = selectedGuest?.Id ?? null;
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
            SelectedGuest = Guests.FirstOrDefault(g=>g.Id == DetailsResponse.TravelAgencyGuestId) ?? new TravelAgencyGuestResponse();
        }
        async Task GetAllGuests()
        {
            IsBusy = true;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string id = Preferences.Default.Get(ApiConstants.travelAgencyCompanyId, "");
                string UserToken = await _service.UserToken();
                if (!string.IsNullOrEmpty(UserToken))
                {
                    UserDialogs.Instance.ShowLoading();
                    var json = await Rep.GetAsync<ObservableCollection<TravelAgencyGuestResponse>>(ApiConstants.GuestApi + $"{id}/TravelAgencyGuest", UserToken);
                    UserDialogs.Instance.HideHud();
                    if (json != null)
                    {
                        Guests!.Clear();
                        Guests = json;
                    }
                }

            }

            IsBusy = false;
        } 
        #endregion
    }
}
