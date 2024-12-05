using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.Pages.ActivateDetailsPages;
using TripBliss.ViewModels.ActivateViewModels;

namespace TripBliss.ViewModels.DistributorsViewModels.ResponseDetails
{
    public partial class Dis_D_TransportaionServiceViewModel : BaseViewModel
    {
        #region Prop
        [ObservableProperty]
        ResponseWithDistributorTransportResponse serviceModdel = new ResponseWithDistributorTransportResponse();
        [ObservableProperty]
        int totalPayment = 0;
        [ObservableProperty]
        int totalPaymentNotActive = 0;
        [ObservableProperty]
        bool isRequestHistory;
        #endregion

        #region Services
        IGenericRepository Rep;
        readonly Services.Data.ServicesService _service;
        #endregion

        #region Const
        public Dis_D_TransportaionServiceViewModel(IGenericRepository generic)
        {
            Rep = generic;
        }
        public Dis_D_TransportaionServiceViewModel(bool _IsRequestHistory, int payment, int paymentNotActive, ResponseWithDistributorTransportResponse model, IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
            ServiceModdel = model;
            TotalPayment = payment;
            TotalPaymentNotActive = paymentNotActive;
            IsRequestHistory = _IsRequestHistory;
            Lang = Preferences.Default.Get("Lan", "en");
        } 
        #endregion

        #region RelayCommand
        [RelayCommand]
        async Task OnApply()
        {
            if (ServiceModdel != null)
            {
                bool answer = await App.Current!.MainPage!.DisplayAlert(TripBliss.Resources.Language.AppResources.Question, TripBliss.Resources.Language.AppResources.Are_You_Accept_This_Price, TripBliss.Resources.Language.AppResources.Yes, TripBliss.Resources.Language.AppResources.No);
                ServiceModdel!.Total = ServiceModdel.Price * ServiceModdel.RequestTravelAgencyTransport.TransportCount;
                ServiceModdel!.AcceptPriceDis = ServiceModdel.Price == 0 ? false : answer;
                await App.Current!.MainPage!.Navigation.PopAsync();
            }
        }

        [RelayCommand]
        async Task OnBackButtonClicked()
        {
            
            await App.Current!.MainPage!.Navigation.PopAsync();
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
                var vm = new MainActivateViewModel(false, IsRequestHistory, ServiceModdel, Rep, _service);
                var page = new MainActivatePage(vm);
                page.BindingContext = vm;
                await App.Current!.MainPage!.Navigation.PushAsync(page);
            }
                
        }
        #endregion
    }
}
