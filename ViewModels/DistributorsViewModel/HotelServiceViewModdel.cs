using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TripBliss.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using TripBliss.Pages.DistributorsPages;

namespace TripBliss.ViewModels.DistributorsViewModel
{
    partial class HotelServiceViewModdel : BaseViewModel
    {
        #region prop
        [ObservableProperty]
        HotelService? hotelService;
        [ObservableProperty]
        ObservableCollection<HotelService> hoteles;
        [ObservableProperty]
        int num;
        #endregion

        public HotelServiceViewModdel()
        {
            Hoteles = new ObservableCollection<HotelService>();
            HotelService = new HotelService();
        }

        #region Methodes
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
        void ApplyHotelClicked()
        {
            HotelService.RoomNo = Num;
            Hoteles.Add(HotelService);
            App.Current.MainPage.Navigation.PopAsync();
        } 
        #endregion
    }
}
