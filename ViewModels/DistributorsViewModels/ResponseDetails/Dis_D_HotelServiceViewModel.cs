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
        ResponseWithDistributorHotelResponse? hotelService = new ResponseWithDistributorHotelResponse();


        #endregion

        IGenericRepository Rep;
        public Dis_D_HotelServiceViewModel(IGenericRepository generic)
        {
            Rep = generic;
            
        }
        public Dis_D_HotelServiceViewModel(ResponseWithDistributorHotelResponse model, IGenericRepository generic)
        {
            Rep = generic;
            Lang = Preferences.Default.Get("Lan", "en");
            HotelService = model;
        }

        #region RelayCommand
        [RelayCommand]
        void BackClicked()
        {
            App.Current.MainPage.Navigation.PopAsync();
        }

       
        //[RelayCommand]
        //void ApplyHotelClicked()
        //{
        //    HotelService.RoomNo = Num;
        //    Hoteles.Add(HotelService);
        //    App.Current.MainPage.Navigation.PopAsync();
        //} 
        #endregion
    }
}
