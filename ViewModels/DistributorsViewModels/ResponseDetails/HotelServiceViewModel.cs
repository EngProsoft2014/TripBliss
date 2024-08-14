﻿using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TripBliss.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;


namespace TripBliss.ViewModels.DistributorsViewModels.ResponseDetails
{
    partial class HotelServiceViewModel : BaseViewModel
    {
        #region prop
        [ObservableProperty]
        HotelServiceModel? hotelService;
        [ObservableProperty]
        ObservableCollection<HotelServiceModel> hoteles;
        [ObservableProperty]
        int num;
        
        #endregion

        public HotelServiceViewModel()
        {
            Hoteles = new ObservableCollection<HotelServiceModel>();
            HotelService = new HotelServiceModel();
        }
        public HotelServiceViewModel(HotelServiceModel model)
        {
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
