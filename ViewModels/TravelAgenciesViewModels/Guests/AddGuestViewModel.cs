using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Constants;
using TripBliss.Helpers;
using TripBliss.Models.RequestTravelAgencyGuest;

namespace TripBliss.ViewModels.TravelAgenciesViewModels.Guests
{
    public partial class AddGuestViewModel : BaseViewModel
    {
        #region Prop
        [ObservableProperty]
        TravelAgencyGuestResponse response = new TravelAgencyGuestResponse();
        [ObservableProperty]
        TravelAgencyGuestRequest request = new TravelAgencyGuestRequest();
        [ObservableProperty]
        int adultCount;
        [ObservableProperty]
        int childCount;
        [ObservableProperty]
        int infantCount; 
        #endregion

        public delegate void GuestDelegte();
        public event GuestDelegte GuestClose;
        #region Services
        IGenericRepository Rep;
        readonly Services.Data.ServicesService _service;
        #endregion

        #region Con
        public AddGuestViewModel(IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
        }
        public AddGuestViewModel(TravelAgencyGuestResponse model, IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
            Response = model;
            AdultCount = Convert.ToInt16(model.AdultCount);
            ChildCount = Convert.ToInt16(model.ChildCount);
            InfantCount = Convert.ToInt16(model.InfantCount);
        }
        #endregion

        #region RelayCommand
        [RelayCommand]
        void OnBackPressed()
        {
            App.Current!.MainPage!.Navigation.PopAsync();
        }

        [RelayCommand]
        void AddAdult()
        {
            if (AdultCount >= 0)
            {
                AdultCount += 1;
            }
        }
        [RelayCommand]
        void DeletAdult()
        {
            if (AdultCount > 0)
            {
                AdultCount -= 1;
            }
        }

        [RelayCommand]
        void AddChild()
        {
            if (ChildCount >= 0)
            {
                ChildCount += 1;
            }
        }
        [RelayCommand]
        void DeletChild()
        {
            if (ChildCount > 0)
            {
                ChildCount -= 1;
            }
        }
        [RelayCommand]
        void AddInfant()
        {
            if (InfantCount >= 0)
            {
                InfantCount += 1;
            }
        }
        [RelayCommand]
        void DeletInfant()
        {
            if (InfantCount > 0)
            {
                InfantCount -= 1;
            }
        }

        [RelayCommand]
        async void ApplyClicked(TravelAgencyGuestResponse model)
        {
            Request.Email = model.Email;
            Request.Phone = model.Phone;
            Request.GuestName = model.GuestName;
            Request.Nationality = model.Nationality;
            Request.IDNumber = model.IDNumber;
            Request.AdultCount = AdultCount.ToString();
            Request.ChildCount = ChildCount.ToString();
            Request.InfantCount = InfantCount.ToString();

            await AddGuest();
        }
        #endregion

        async Task AddGuest()
        {
            IsBusy = false;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {

                string UserToken = await _service.UserToken();

                string id = Preferences.Default.Get(ApiConstants.travelAgencyCompanyId, "");
                var json = await Rep.PostTRAsync<TravelAgencyGuestRequest, TravelAgencyGuestResponse>(ApiConstants.GuestApi + $"{id}/TravelAgencyGuest", Request, UserToken);

                if (json.Item1 != null)
                {
                    var toast = Toast.Make("Successfully for Add Request", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                    GuestClose.Invoke();
                    await App.Current!.MainPage!.Navigation.PopAsync();
                }
                else
                {
                    var toast = Toast.Make($"Warning, {json.Item2}", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
            }

            IsBusy = true;
        }
    }
}
