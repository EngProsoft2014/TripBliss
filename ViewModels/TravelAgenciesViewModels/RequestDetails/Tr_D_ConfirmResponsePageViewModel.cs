﻿using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Models;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using Microsoft.VisualBasic;
using TripBliss.Pages.TravelAgenciesPages.RequestDetails;
using TripBliss.Helpers;
using TripBliss.ViewModels.TravelAgenciesViewModels.CreateRequest;
using Controls.UserDialogs.Maui;
using TripBliss.Constants;
using CommunityToolkit.Maui.Alerts;
using TripBliss.Pages.TravelAgenciesPages;
using Mopups.Services;
using TripBliss.Models.ResponseWithDistributor;
using Newtonsoft.Json;

namespace TripBliss.ViewModels.TravelAgenciesViewModels.RequestDetails
{
    public partial class Tr_D_ConfirmResponsePageViewModel : BaseViewModel
    {
        #region prop
        [ObservableProperty]
        ResponseWithDistributorDetailsResponse response = new ResponseWithDistributorDetailsResponse();
        [ObservableProperty]
        bool isShowPaymentBtn;
        [ObservableProperty]
        bool isShowReviewBtn;
        [ObservableProperty]
        bool isRequestHistory;
        #endregion

        #region Services
        IGenericRepository Rep;
        readonly Services.Data.ServicesService _service;
        #endregion

        #region Cons
        public Tr_D_ConfirmResponsePageViewModel(ResponseWithDistributorResponse distributorResponse, IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
            _ = Init(distributorResponse.DistributorCompanyId, distributorResponse.Id);
        }
        #endregion

        #region Generl RelayCommand
        [RelayCommand]
        async Task BackButtonClicked()
        {
            await App.Current!.MainPage!.Navigation.PopAsync();
        }
        [RelayCommand]
        async Task PaymentClicked()
        {
            if (Constants.Permissions.CheckPermission(Constants.Permissions.Payment))
            {
                bool result = CheckChooseServices();
                if (result)
                {
                    var vm = new Tr_D_PaymentViewModel(Response.Id, Response.TotalPriceAgentAccept, Response.TotalPayment, Rep, _service);
                    var page = new PaymentPage(vm);
                    page.BindingContext = vm;
                    await App.Current!.MainPage!.Navigation.PushAsync(page);
                }
                else
                {
                    var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Check_to_one_service_or_more, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
            }
            else
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.PermissionAlert, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
        }
        [RelayCommand]
        async Task Apply()
        {
            IsBusy = false;

            bool result = CheckChooseServices();

            if (result)
            {
                if (Response.TotalPayment > 0)
                {
                    await Init(Response.DistributorCompanyId, Response.Id);
                    var toast = Toast.Make(TripBliss.Resources.Language.AppResources.already_paid_cannot_be_modified, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
                else
                {
                    bool answer = await App.Current!.MainPage!.DisplayAlert(TripBliss.Resources.Language.AppResources.Question, TripBliss.Resources.Language.AppResources.AreYouAcceptThisFinallPrice, TripBliss.Resources.Language.AppResources.Yes, TripBliss.Resources.Language.AppResources.No);

                    if (Connectivity.NetworkAccess == NetworkAccess.Internet && answer)
                    {

                        string UserToken = await _service.UserToken();

                        UserDialogs.Instance.ShowLoading();
                        var json = await Rep.PostTRAsync<ResponseWithDistributorDetailsResponse, ResponseWithDistributorResponse>(ApiConstants.ResponseDetailsDistApi + $"{Response.DistributorCompanyId}/ResponseWithDistributor/{Response.Id}", Response, UserToken);
                        UserDialogs.Instance.HideHud();

                        if (json.Item1 != null)
                        {
                            var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Successfully_AddResponse, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                            await toast.Show();

                            IsShowPaymentBtn = true;
                            //Controls.StaticMember.WayOfTab = 0;
                            //await App.Current!.MainPage!.Navigation.PushAsync(new HomeAgencyPage(new Tr_HomeViewModel(Rep, _service), Rep, _service));
                        }
                        else
                        {
                            var toast = Toast.Make($"{json.Item2!.errors!.FirstOrDefault().Value}", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                            await toast.Show();
                        }
                    }
                }
            }
            else
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Check_to_one_service_or_more, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }

            IsBusy = true;
        }

        [RelayCommand]
        async Task ReviewClicked()
        {
            IsBusy = false;

            var vieModel = new Tr_D_ReviewViewModel(Rep, _service);
            var page = new Pages.TravelAgenciesPages.RequestDetails.Tr_ReviewPopup(vieModel);
            vieModel.ReviewClose += async (model) =>
            {
                if (model != null)
                {
                    UserDialogs.Instance.ShowLoading();
                    string UserToken = await _service.UserToken();
                    var json = await Rep.PostTRAsync<ResponseWithDistributorReviewTravelAgentRequest, string>(string.Format($"Distributor/{Response.DistributorCompanyId}/ResponseWithDistributor/{Response.Id}/ReviewToDistributor"), model, UserToken);
                    UserDialogs.Instance.HideHud();
                    if (json.Item1 == null && json.Item2 == null)
                    {
                        await App.Current!.MainPage!.Navigation.PushAsync(new HomeAgencyPage(new Tr_HomeViewModel(Rep, _service), Rep, _service));
                        var toast = Toast.Make(TripBliss.Resources.Language.AppResources.ReviewSuccess, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                        await toast.Show();
                    }
                    else
                    {
                        var toast = Toast.Make($"{json.Item2!.errors!.FirstOrDefault().Value}", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                        await toast.Show();
                    }
                }
                await MopupService.Instance.PopAsync();
            };

            await MopupService.Instance.PushAsync(page);

            IsBusy = true;
        }
        #endregion

        #region Hotel RelayCommand
        [RelayCommand]
        void SelectHotel(ResponseWithDistributorHotelResponse model)
        {

            var vm = new Tr_D_HotelServiceViewModel(IsRequestHistory, Response.TotalPayment, model, Rep, _service);
            var page = new HotelServicePage(vm, Rep);
            page.BindingContext = vm;
            App.Current!.MainPage!.Navigation.PushAsync(page);

        }
        #endregion

        #region Transportaiton RelayCommand
        [RelayCommand]
        async Task SelectTransportaion(ResponseWithDistributorTransportResponse model)
        {
            var vm = new Tr_D_TransportaionServiceViewModel(IsRequestHistory, Response.TotalPayment, model, Rep, _service);
            var page = new TransportaionServicePage(vm, Rep);
            page.BindingContext = vm;
            await App.Current!.MainPage!.Navigation.PushAsync(page);
        }
        #endregion

        #region Air Flight RelayCommand
        [RelayCommand]
        void SelectAirFlight(ResponseWithDistributorAirFlightResponse model)
        {
            var vm = new Tr_D_AirFlightServicesViewModel(IsRequestHistory, Response.TotalPayment, model, Rep, _service);
            var page = new AirFlightServicePage(vm, Rep);
            page.BindingContext = vm;
            App.Current!.MainPage!.Navigation.PushAsync(page);
        }
        #endregion

        #region Visa RelayCommand
        [RelayCommand]
        async Task SelectVisa(ResponseWithDistributorVisaResponse model)
        {
            var vm = new Tr_D_VisaServiceViewModel(IsRequestHistory, Response.TotalPayment, model, Rep, _service);
            var page = new VisaServicePage(vm, Rep);
            page.BindingContext = vm;
            await App.Current!.MainPage!.Navigation.PushAsync(page);
        }
        #endregion

        #region Methodes
        async Task Init(string distId, string ReqId)
        {
            UserDialogs.Instance.ShowLoading();
            await GetRequestDetailes(distId, ReqId);
            UserDialogs.Instance.HideHud();
        }
        async Task GetRequestDetailes(string distId, string ReqId)
        {
            IsBusy = false;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();

                var json = await Rep.GetAsync<ResponseWithDistributorDetailsResponse>(ApiConstants.ResponseDetailsDistApi + $"{distId}/ResponseWithDistributor/{ReqId}", UserToken);

                if (json != null)
                {
                    Response = json;
                    
                    if (!string.IsNullOrEmpty(Preferences.Default.Get(ApiConstants.travelAgencyCompanyId, "")) && !string.IsNullOrEmpty(Response.ReviewUserTravelAgentName))
                    {
                        IsRequestHistory = true;
                    }
                    else
                    {
                        IsRequestHistory = false;

                        bool result = CheckChooseServices();
                        if (result)
                        {
                            IsShowPaymentBtn = true;
                        }
                        else
                        {
                            IsShowPaymentBtn = false;
                        }
                        CheckShowReview();
                    }

                }
            }

            IsBusy = true;
        }

        bool CheckChooseServices()
        {
            var ResponseAirFlt = Response?.ResponseWithDistributorAirFlight?.Where(x => x.AcceptAgen == true).FirstOrDefault();
            var ResponseHotel = Response?.ResponseWithDistributorHotel?.Where(x => x.AcceptAgen == true).FirstOrDefault();
            var ResponseTrans = Response?.ResponseWithDistributorTransport?.Where(x => x.AcceptAgen == true).FirstOrDefault();
            var ResponseVisa = Response?.ResponseWithDistributorVisa?.Where(x => x.AcceptAgen == true).FirstOrDefault();

            if (ResponseAirFlt != null || ResponseHotel != null || ResponseTrans != null || ResponseVisa != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        void CheckShowReview()
        {
            if (Response?.TotalPriceAgentAccept > 0 && Response?.TotalPriceAgentAccept == Response?.TotalPayment)
            {
                if(string.IsNullOrEmpty(Response?.ReviewUserTravelAgentName))
                {
                    IsShowReviewBtn = true;
                    IsShowPaymentBtn = false;
                }
                else
                {
                    IsShowReviewBtn = false;
                    IsShowPaymentBtn = false;
                }
            }
            else
            {
                IsShowReviewBtn = false;
                IsShowPaymentBtn = true;
            }
        }

        // this will delete after Apple aproved
        async Task AddPayment()
        {

            IsBusy = false;

            UserDialogs.Instance.ShowLoading();
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {

                string UserToken = await _service.UserToken();
                ResponseWithDistributorPaymentRequest paymentRequest = new ResponseWithDistributorPaymentRequest
                {
                    AmountPayment = 1,
                    PaymentMethod = 1,
                    dbcr = 1,
                    Notes = "",
                    Refnumber = "stetrrcc",
                };

                var json = await Rep.PostTRAsync<ResponseWithDistributorPaymentRequest, ResponseWithDistributorPaymentResponse>(ApiConstants.AllPaymentApi + $"{Response.Id}/ResponseWithDistributorPayment", paymentRequest, UserToken);

                if (json.Item1 != null)
                {

                }
                else
                {

                }
            }
            UserDialogs.Instance.HideHud();
            IsBusy = true;
        }
        #endregion


    }
}
