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

        #endregion

        IGenericRepository Rep;
        public Tr_C_HotelServiceViewModel(IGenericRepository generic)
        {
            Rep = generic;
            Hoteles = new ObservableCollection<HotelServiceModel>();
            HotelService = new HotelServiceModel();
        }
        public Tr_C_HotelServiceViewModel(HotelServiceModel model , IGenericRepository generic)
        {
            Rep = generic;
            Hoteles = new ObservableCollection<HotelServiceModel>();
            HotelService = new HotelServiceModel();
            HotelService = model;
            Lang = Preferences.Default.Get("Lan", "en");
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
