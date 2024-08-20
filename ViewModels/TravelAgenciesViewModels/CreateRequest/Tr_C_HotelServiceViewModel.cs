using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TripBliss.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using TripBliss.Helpers;
using TripBliss.Constants;


namespace TripBliss.ViewModels.TravelAgenciesViewModels.CreateRequest
{
    public partial class Tr_C_HotelServiceViewModel : BaseViewModel
    {
        #region prop
        [ObservableProperty]
        HotelServiceModel? hotelService;
        [ObservableProperty]
        ObservableCollection<HotelServiceModel> hoteles;
        [ObservableProperty]
        int num;
        [ObservableProperty]
        ObservableCollection<LocationResponse> locations = new ObservableCollection<LocationResponse>();

        #endregion


        readonly Services.Data.ServicesService _service;
        IGenericRepository Rep;

        public Tr_C_HotelServiceViewModel(IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
            Hoteles = new ObservableCollection<HotelServiceModel>();
            HotelService = new HotelServiceModel();
            _service = service;
        }
        public Tr_C_HotelServiceViewModel(HotelServiceModel model, IGenericRepository generic)
        {
            Rep = generic;
            Hoteles = new ObservableCollection<HotelServiceModel>();
            HotelService = new HotelServiceModel();
            HotelService = model;
            Lang = Preferences.Default.Get("Lan", "en");
        }

        async void GetLocation()
        {
            IsBusy = true;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();

                var json = await Rep.GetAsync<ObservableCollection<LocationResponse>>(ApiConstants.GetAllLocationsApi, UserToken);

                if (json != null)
                {
                    Locations = json;
                }
            }

            IsBusy = false;
        }

        #region RelayCommand
        [RelayCommand]
        void AddRoom()
        {
            Num += 1;
        }

        [RelayCommand]
        void DeletRoom()
        {
            Num -= 1;
        }
        [RelayCommand]
        async void BackClicked()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

        [RelayCommand]
        async void ApplyHotelClicked()
        {
            HotelService.RoomNo = Num;
            Hoteles.Add(HotelService);
            await App.Current.MainPage.Navigation.PopAsync();
        }
        #endregion
    }
}
