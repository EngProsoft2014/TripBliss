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

        public delegate void HotelDelegte(ResponseWithDistributorHotelResponse HotelResponse);
        public event HotelDelegte HotelClose;
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
        async Task BackClicked()
        {
            await App.Current!.MainPage!.Navigation.PopAsync();
        }


        [RelayCommand]
        async Task ApplyHotelClicked(ResponseWithDistributorHotelResponse HotelResponseModel)
        {
            
            await App.Current!.MainPage!.Navigation.PopAsync();
        }
        #endregion
    }
}
