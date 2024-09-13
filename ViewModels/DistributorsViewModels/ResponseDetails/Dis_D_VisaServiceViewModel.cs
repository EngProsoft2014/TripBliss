﻿using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    public partial class Dis_D_VisaServiceViewModel : BaseViewModel
    {
        #region Prop
        [ObservableProperty]
        ResponseWithDistributorVisaResponse moddel = new ResponseWithDistributorVisaResponse();
        [ObservableProperty]
        int totalPayment = 0;
        #endregion

        #region Services
        IGenericRepository Rep;
        readonly Services.Data.ServicesService _service;
        #endregion
        public Dis_D_VisaServiceViewModel(IGenericRepository generic)
        {
            Rep = generic;

        }
        public Dis_D_VisaServiceViewModel(int payment,ResponseWithDistributorVisaResponse model, IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            Moddel = model;
            TotalPayment = payment;
            Lang = Preferences.Default.Get("Lan", "en");
            _service = service;
        }

        #region RelayCommand
        [RelayCommand]
        async Task Apply()
        {
            bool answer = await App.Current!.MainPage!.DisplayAlert("Question?", "Are You Accept This Price?", "Yes", "No");
            Moddel!.AcceptPriceDis = answer;
            await App.Current!.MainPage!.Navigation.PopAsync();
        }
        [RelayCommand]
        async Task BackCLicked()
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
                var vm = new MainActivateViewModel(Moddel, Rep, _service);
                var page = new MainActivatePage(vm);
                page.BindingContext = vm;
                await App.Current!.MainPage!.Navigation.PushAsync(page);
            }
                
        }
        #endregion
    }
}
