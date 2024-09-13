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
using TripBliss.Models.RequestTravelAgencyGuest;
using TripBliss.Pages.TravelAgenciesPages.Guests;

namespace TripBliss.ViewModels.TravelAgenciesViewModels.Guests
{
    public partial class GuestsViewModel : BaseViewModel
    {
        #region Prop
        [ObservableProperty]
        ObservableCollection<TravelAgencyGuestResponse> guests = new ObservableCollection<TravelAgencyGuestResponse>(); 
        #endregion

        #region Services
        IGenericRepository Rep;
        readonly Services.Data.ServicesService _service;
        #endregion

        #region Con
        public GuestsViewModel(IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
            Init();
        } 
        #endregion



        [RelayCommand]
        async Task BackClicked()
        {
            await App.Current!.MainPage!.Navigation.PopAsync();
        }
        [RelayCommand]
        async Task AddGuest()
        {
            var vm = new AddGuestViewModel(Rep,_service);
            vm.GuestClose += async () =>
            {
                Init();
            };
            var page = new Tr_AddGuestPage();
            page.BindingContext= vm;
            await App.Current!.MainPage!.Navigation.PushAsync(page);
        }

        [RelayCommand]
        public async Task SelectGuest(TravelAgencyGuestResponse guest)
        {
            var vm = new AddGuestViewModel(guest,Rep, _service);
            vm.GuestClose += async () =>
            {
                Init();
            };
            var page = new Tr_AddGuestPage();
            page.BindingContext = vm;
            await App.Current!.MainPage!.Navigation.PushAsync(page);
        }

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
