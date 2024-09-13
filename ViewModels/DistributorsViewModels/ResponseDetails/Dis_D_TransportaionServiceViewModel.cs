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
        public Dis_D_TransportaionServiceViewModel(int payment, ResponseWithDistributorTransportResponse model, IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
            ServiceModdel = model;
            TotalPayment = payment;
            Lang = Preferences.Default.Get("Lan", "en");
        } 
        #endregion

        #region RelayCommand
        [RelayCommand]
        async Task OnApply()
        {
            if (ServiceModdel != null)
            {
                bool answer = await App.Current!.MainPage!.DisplayAlert("Question?", "Are You Accept This Price?", "Yes", "No");
                ServiceModdel!.AcceptPriceDis = answer;
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
                var toast = Toast.Make("The Agency must pay part of the amount due.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else
            {
                var vm = new MainActivateViewModel(ServiceModdel, Rep, _service);
                var page = new MainActivatePage(vm);
                page.BindingContext = vm;
                await App.Current!.MainPage!.Navigation.PushAsync(page);
            }
                
        }
        #endregion
    }
}
