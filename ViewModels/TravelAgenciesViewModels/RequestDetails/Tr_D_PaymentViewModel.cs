﻿using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using Microsoft.AspNet.SignalR.Client.Http;
using Syncfusion.Maui.Data;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using TripBliss.Constants;
using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.Pages.TravelAgenciesPages;


namespace TripBliss.ViewModels.TravelAgenciesViewModels.RequestDetails
{
    
    public partial class Tr_D_PaymentViewModel : BaseViewModel
    {
        #region Prop
        string ReqId;
        int Totalprice;
        int Totalpayment;
        [ObservableProperty]
        int outStandingprice;
        [ObservableProperty]
        bool isAllPyment;
        [ObservableProperty]
        ObservableCollection<ResponseWithDistributorPaymentResponse> payments;
        #endregion

        #region Services
        IGenericRepository Rep;
        readonly Services.Data.ServicesService _service;
        #endregion

        #region Cons
        public Tr_D_PaymentViewModel(string id,int totalPrice,int totalPayment, IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
            ReqId = id;
            Totalpayment = totalPayment;
            OutStandingprice = totalPrice - totalPayment;
            Totalprice = totalPrice;
            IsAllPyment = true;
            Init();
        }
        #endregion

        [RelayCommand]
        async Task AddPayment()
        {
            bool answer = await App.Current!.MainPage!.DisplayAlert(TripBliss.Resources.Language.AppResources.Question, TripBliss.Resources.Language.AppResources.Are_You_Accept_To_Pay, TripBliss.Resources.Language.AppResources.Yes, TripBliss.Resources.Language.AppResources.No);
            IsBusy = false;

            UserDialogs.Instance.ShowLoading();
            if (Connectivity.NetworkAccess == NetworkAccess.Internet && answer)
            {

                string UserToken = await _service.UserToken();
                ResponseWithDistributorPaymentRequest paymentRequest = new ResponseWithDistributorPaymentRequest
                {
                    AmountPayment = IsAllPyment == true ? OutStandingprice : (Totalprice - Totalpayment - OutStandingprice),
                    PaymentMethod = 1,
                    dbcr = 1,
                    Notes ="",
                    Refnumber = "stetrrcc",
                };

                var json = await Rep.PostTRAsync<ResponseWithDistributorPaymentRequest, ResponseWithDistributorPaymentResponse>(ApiConstants.AllPaymentApi + $"{ReqId}/ResponseWithDistributorPayment", paymentRequest, UserToken);

                if (json.Item1 != null)
                {
                    var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Successfully_for_Paying, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                    Totalpayment = (int)(Totalpayment + paymentRequest.AmountPayment);
                    OutStandingprice = IsAllPyment == true ? 0 : OutStandingprice - paymentRequest.AmountPayment.Value;
                    await GetPayDetailes();     
                }
                else
                {
                    var toast = Toast.Make($"{json.Item2!.errors!.FirstOrDefault().Value}", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
            }
            UserDialogs.Instance.HideHud();
            IsBusy = true;
        }
        [RelayCommand]
        async Task BackButtonClicked()
        {
            await App.Current!.MainPage!.Navigation.PopAsync();
        }

        #region Methods
        async void Init()
        {
            UserDialogs.Instance.ShowLoading();
            await GetPayDetailes();
            UserDialogs.Instance.HideHud();
        }
        async Task GetPayDetailes()
        {
            IsBusy = false;
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();

                var json1 = await Rep.GetAsync<ObservableCollection<ResponseWithDistributorPaymentResponse>>(ApiConstants.AllPaymentApi + $"{ReqId}/ResponseWithDistributorPayment", UserToken);

                if (json1 != null)
                {
                    Payments = json1;
                }
            }
            IsBusy = true;
        } 

        public async Task CalcOutPrice(int CustomPrice)
        {
            if (CustomPrice > Totalprice)
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.This_value_is_greater_than_the_total_price, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (CustomPrice !=0)
            {
                OutStandingprice = Totalprice - Totalpayment - CustomPrice;
                IsAllPyment = false;
            }
            else
            {
                OutStandingprice = Totalprice == Totalpayment ? 0 : Totalprice - Totalpayment;
                IsAllPyment = true;
            }
        }
        #endregion
    }
}
