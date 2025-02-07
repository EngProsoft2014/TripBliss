﻿using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using GoogleApi.Entities.Translate.Common.Enums;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Constants;
using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.Pages.Shared;

namespace TripBliss.ViewModels.DistributorsViewModels.ResponseDetails
{
    public partial class Dis_D_PaymentViewModel : BaseViewModel
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
        [ObservableProperty]
        ResponseWithDistributorPaymentResponse onePayment;
        [ObservableProperty]
        ResponseWithDistributorResponse response;
        [ObservableProperty]
        string complaintVm;

        [ObservableProperty]
        string cardNumber;

        [ObservableProperty]
        string holderName;

        [ObservableProperty]
        string expirationDate;

        [ObservableProperty]
        string cvv;

        [ObservableProperty]
        int paymentNotActive;

        [ObservableProperty]
        bool isPaymentNotActive;

        [ObservableProperty]
        bool isPayAndAmounTrue = true;
        #endregion

        #region Services
        IGenericRepository Rep;
        readonly Services.Data.ServicesService _service;
        ResponseWithDistributorResponse _distributorResponse;
        #endregion

        #region Cons
        public Dis_D_PaymentViewModel(ResponseWithDistributorResponse model, IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
            Response = model;

            //IsPaymentNotActive = model.CountPaymentNotActive > 0 ? true : false;
            IsAllPyment = true;

            Init(model);
        }
        #endregion

        [RelayCommand]
        async Task AddPayment()
        {
            bool answer = await App.Current!.MainPage!.DisplayAlert(TripBliss.Resources.Language.AppResources.Question, TripBliss.Resources.Language.AppResources.Are_You_Accept_To_Pay, TripBliss.Resources.Language.AppResources.Yes, TripBliss.Resources.Language.AppResources.No);
            IsBusy = false;


            if (Connectivity.NetworkAccess == NetworkAccess.Internet && answer)
            {

                string UserToken = await _service.UserToken();
                ResponseWithDistributorPaymentRequest paymentRequest = new ResponseWithDistributorPaymentRequest
                {
                    AmountPayment = IsAllPyment == true ? OutStandingprice : (Totalprice - Totalpayment - OutStandingprice - PaymentNotActive),
                    PaymentMethod = 1,
                    dbcr = 1,
                    Notes = "",
                    Refnumber = new Guid().ToString(),
                };

                UserDialogs.Instance.ShowLoading();
                var json = await Rep.PostTRAsync<ResponseWithDistributorPaymentRequest, ResponseWithDistributorPaymentResponse>(ApiConstants.AllPaymentApi + $"{ReqId}/ResponseWithDistributorPayment", paymentRequest, UserToken);
                UserDialogs.Instance.HideHud();

                if (json.Item1 != null)
                {
                    var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Successfully_for_Paying, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                    Totalpayment = (int)(Totalpayment + paymentRequest.AmountPayment);
                    //OutStandingprice = IsAllPyment == true ? 0 : OutStandingprice - paymentRequest.AmountPayment.Value;
                    await GetPayDetailes();
                }
                else
                {
                    var toast = Toast.Make($"{json.Item2!.errors!.FirstOrDefault().Value}", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
            }

            IsBusy = true;
        }

        [RelayCommand]
        async Task ApplyComplaint(ResponseWithDistributorPaymentResponse response)
        {
            IsBusy = false;

            if (!string.IsNullOrEmpty(ComplaintVm))
            {
                string UserToken = await _service.UserToken();

                response.Complaint = ComplaintVm;
                UserDialogs.Instance.ShowLoading();
                var json = await Rep.PostTRAsync<ResponseWithDistributorPaymentResponse,ResponseWithDistributorPaymentResponse>(ApiConstants.AllPaymentApi + $"{response.ResponseWithDistributorId}/ResponseWithDistributorPayment/{response.Id}/Update", response, UserToken);
                UserDialogs.Instance.HideHud();

                if(json.Item1 == null && json.Item2 ==null)
                {
                    var toast = Toast.Make(TripBliss.Resources.Language.AppResources.ErrorTryAgain, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
                else if (json.Item1 != null)
                {
                    var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Complaint_added_successfully, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();

                    await MopupService.Instance.PopAsync();
                }
                else
                {
                    var toast = Toast.Make($"{json.Item2!.errors!.FirstOrDefault().Value}", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
            }
            else
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_Complaint, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }

            IsBusy = true;
        }

        [RelayCommand]
        async Task OpenFullScreenImage(string imgURL)
        {
            if (imgURL.Contains(".pdf"))
            {
                await App.Current!.MainPage!.Navigation.PushAsync(new PdfViewerPage(imgURL));
            }
            else
            {
                if (string.IsNullOrEmpty(imgURL))
                {
                    var toast = Toast.Make(TripBliss.Resources.Language.AppResources.No_image_here, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
                else
                {
                    ImageSource sou = ImageSource.FromUri(new Uri(imgURL));
                    IsBusy = false;
                    //UserDialogs.Instance.ShowLoading();
                    await App.Current!.MainPage!.Navigation.PushAsync(new Pages.MainPopups.FullScreenImagePopup(sou));
                    //UserDialogs.Instance.HideHud();
                    IsBusy = true;
                }

            }

        }

        [RelayCommand]
        async Task OpenComplaint(ResponseWithDistributorPaymentResponse payment)
        {
            IsBusy = false;
            OnePayment = payment;
            ComplaintVm = !string.IsNullOrEmpty(payment.Complaint) ? payment.Complaint : "";
            var page = new Pages.DistributorsPages.ResponseDetailes.Dis_ComplaintPopup(this);
            await MopupService.Instance.PushAsync(page);
            IsBusy = true;
        }


        [RelayCommand]
        async Task BackButtonClicked()
        {
            //new Tr_D_ConfirmResponsePageViewModel(_distributorResponse, Rep, _service);
            await App.Current!.MainPage!.Navigation.PopAsync();
            //await App.Current!.MainPage!.Navigation.PushAsync(new ConfirmResponsePage(new Tr_D_ConfirmResponsePageViewModel(_distributorResponse, Rep, _service), Rep));
            //App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 2]);
        }

        #region Methods
        async void Init(ResponseWithDistributorResponse model)
        {
            UserDialogs.Instance.ShowLoading();  
            ReqId = model?.Id!;
            Totalpayment = model!.TotalPayment!;
            OutStandingprice = model.TotalPriceAgentAccept - (model.TotalPayment! + model.TotalPaymentNotActive);
            Totalprice = model.TotalPriceAgentAccept;
            PaymentNotActive = model.TotalPaymentNotActive;

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
                    await ShowPandingTransactions();
                }
            }

            IsBusy = true;
        }

        async Task ShowPandingTransactions()
        {
            if(Payments.Count > 0)
            {
                var obj = Payments.Where(x => x.Active == false).FirstOrDefault();
                if (obj != null)
                {
                    IsPaymentNotActive = true;
                }
                else
                {
                    IsPaymentNotActive = false;
                }
            }
        }

        public async Task CalcOutPrice(int CustomPrice)
        {
            if (CustomPrice > (Totalprice - PaymentNotActive))
            {
                IsPayAndAmounTrue = false;
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.This_value_is_greater_than_the_total_price, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (CustomPrice == 0 && CustomPrice == (Totalprice - PaymentNotActive))
            {
                IsPayAndAmounTrue = false;
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.There_are_no_amounts_due, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (CustomPrice != 0)
            {
                IsPayAndAmounTrue = true;
                OutStandingprice = Totalprice - Totalpayment - CustomPrice - PaymentNotActive;
                IsAllPyment = false;
            }
            else
            {
                IsPayAndAmounTrue = true;
                OutStandingprice = Totalprice == Totalpayment ? 0 : Totalprice - Totalpayment - PaymentNotActive;
                IsAllPyment = true;
            }
        }

        #endregion


        [RelayCommand]
        async Task IsActivePayBank(ResponseWithDistributorPaymentResponse model)
        {
            IsBusy = false;
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                bool answer = await App.Current!.MainPage!.DisplayAlert(TripBliss.Resources.Language.AppResources.Question, TripBliss.Resources.Language.AppResources.Are_recieved_your_bank, TripBliss.Resources.Language.AppResources.Yes, TripBliss.Resources.Language.AppResources.No);
                if (answer)
                {
                    string UserToken = await _service.UserToken();

                    var json1 = await Rep.PutAsync<string>(ApiConstants.PaymentActive + $"{model.ResponseWithDistributorId}/ResponseWithDistributorPayment/{model.Id}/ToggleActive", null, UserToken);

                    if (string.IsNullOrEmpty(json1))
                    {
                        model.Active = true;
                        await ShowPandingTransactions();
                        var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Active_For_Pay_Bank, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                        await toast.Show();
                    }
                    else
                    {
                        var toast1 = Toast.Make(TripBliss.Resources.Language.AppResources.ErrorTryAgain, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                        await toast1.Show();
                    }
                }
            }

            IsBusy = true;
        }
    }
}
