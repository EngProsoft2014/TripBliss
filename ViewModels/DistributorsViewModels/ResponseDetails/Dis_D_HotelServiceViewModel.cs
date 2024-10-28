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
        bool isRequestHistory;
        [ObservableProperty]
        int totalPayment = 0;
        int Oldprice;

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
        public Dis_D_HotelServiceViewModel(bool _IsRequestHistory,int payment, ResponseWithDistributorHotelResponse model, IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
            IsRequestHistory = _IsRequestHistory;
            TotalPayment = payment;
            Lang = Preferences.Default.Get("Lan", "en");
            HotelService = model;
            Oldprice = model.Price;
        }

        #region RelayCommand
        [RelayCommand]
        async Task BackClicked()
        {
            HotelService!.Price = Oldprice;
            await App.Current!.MainPage!.Navigation.PopAsync();
        }


        [RelayCommand]
        async Task ApplyHotelClicked(ResponseWithDistributorHotelResponse HotelResponseModel)
        {
            if (Constants.Permissions.CheckPermission(Constants.Permissions.Accept_Price))
            {
                bool answer = await App.Current!.MainPage!.DisplayAlert(TripBliss.Resources.Language.AppResources.Question, TripBliss.Resources.Language.AppResources.Are_You_Accept_This_Price, TripBliss.Resources.Language.AppResources.Yes, TripBliss.Resources.Language.AppResources.No);
                if (answer)
                {
                    HotelService!.Total = HotelService.Price * HotelService.RequestTravelAgencyHotel.RoomCount;
                    HotelService!.AcceptPriceDis = answer;
                    await App.Current!.MainPage!.Navigation.PopAsync();
                }
                else
                {
                    HotelService!.Total = HotelService.Price * HotelService.RequestTravelAgencyHotel.RoomCount;
                    HotelService!.Price = Oldprice;
                    await App.Current!.MainPage!.Navigation.PopAsync();
                } 
            }
            else
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.PermissionAlert, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
        }
        [RelayCommand]
        async Task ActiveClicked()
        {
            if (TotalPayment == 0)
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Agency_must_pay_part_of_the_amount_due, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else
            {
                var vm = new MainActivateViewModel(false, IsRequestHistory, HotelService!, Rep, _service);
                var page = new MainActivatePage(vm);
                page.BindingContext = vm;
                await App.Current!.MainPage!.Navigation.PushAsync(page);
            }
            
        }
        #endregion
    }
}
