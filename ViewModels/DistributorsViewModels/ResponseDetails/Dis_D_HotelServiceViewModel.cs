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
using TripBliss.Pages.ActivateDetailsPages;
using TripBliss.ViewModels.ActivateViewModels;
using CommunityToolkit.Maui.Alerts;


namespace TripBliss.ViewModels.DistributorsViewModels.ResponseDetails
{
    public partial class Dis_D_HotelServiceViewModel : BaseViewModel
    {
        #region prop
        [ObservableProperty]
        ResponseWithDistributorHotelResponse? hotelService = new ResponseWithDistributorHotelResponse();
        [ObservableProperty]
        int totalPayment = 0;

        public delegate void HotelDelegte(ResponseWithDistributorHotelResponse HotelResponse);
        public event HotelDelegte HotelClose;
        #endregion

        #region Services
        IGenericRepository Rep;
        readonly Services.Data.ServicesService _service;
        #endregion
        public Dis_D_HotelServiceViewModel(IGenericRepository generic)
        {
            Rep = generic;
            
        }
        public Dis_D_HotelServiceViewModel(int payment, ResponseWithDistributorHotelResponse model, IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
            TotalPayment = payment;
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
            bool answer = await App.Current!.MainPage!.DisplayAlert("Question?", "Are You Accept This Price?", "Yes", "No");
            HotelService!.AcceptPriceDis = answer;
            await App.Current!.MainPage!.Navigation.PopAsync();
        }
        [RelayCommand]
        async Task ActiveClicked()
        {
            if (TotalPayment == 0)
            {
                var toast = Toast.Make("The Agency must pay part of the amount due.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else
            {
                var vm = new MainActivateViewModel(HotelService!, Rep, _service);
                var page = new MainActivatePage(vm);
                page.BindingContext = vm;
                await App.Current!.MainPage!.Navigation.PushAsync(page);
            }
            
        }
        #endregion
    }
}
