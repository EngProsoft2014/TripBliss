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


namespace TripBliss.ViewModels.DistributorsViewModels.ResponseDetails
{
    public partial class Dis_D_HotelServiceViewModel : BaseViewModel
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
        public Dis_D_HotelServiceViewModel(IGenericRepository generic)
        {
            Rep = generic;
            Hoteles = new ObservableCollection<HotelServiceModel>();
            HotelService = new HotelServiceModel();
        }
        public Dis_D_HotelServiceViewModel(HotelServiceModel model, IGenericRepository generic)
        {
            Rep = generic;
            Lang = Preferences.Default.Get("Lan", "en");
            Hoteles = new ObservableCollection<HotelServiceModel>();
            HotelService = new HotelServiceModel();
            HotelService = model;
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
        void BackClicked()
        {
            App.Current.MainPage.Navigation.PopAsync();
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
