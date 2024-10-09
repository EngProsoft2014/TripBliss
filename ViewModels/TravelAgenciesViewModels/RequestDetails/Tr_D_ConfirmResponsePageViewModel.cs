using CommunityToolkit.Mvvm.Input;
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

namespace TripBliss.ViewModels.TravelAgenciesViewModels.RequestDetails
{
    public partial class Tr_D_ConfirmResponsePageViewModel : BaseViewModel
    {


        #region prop
        [ObservableProperty]
        ResponseWithDistributorDetailsResponse response = new ResponseWithDistributorDetailsResponse();
        [ObservableProperty]
        bool isShowPaymentBtn;

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
            bool result = CheckChooseServices();
            if(result)
            {
                var vm = new Tr_D_PaymentViewModel(Response.Id, Response.TotalPriceAgentAccept, Response.TotalPayment, Rep, _service);
                var page = new PaymentPage(vm);
                page.BindingContext = vm;
                await App.Current!.MainPage!.Navigation.PushAsync(page);
            }
            else
            {
                var toast = Toast.Make("Warning, Please Check to one service or more", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
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
                bool answer = await App.Current!.MainPage!.DisplayAlert("Question?", "Are You Accept This Finall Price?", "Yes", "No");

                if (Connectivity.NetworkAccess == NetworkAccess.Internet && answer)
                {

                    string UserToken = await _service.UserToken();
                    if (Response.TotalPayment > 0)
                    {
                        var toast = Toast.Make($"Warning, This order has already been paid for and cannot be modified.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                        await toast.Show();
                    }
                    else
                    {
                        UserDialogs.Instance.ShowLoading();
                        var json = await Rep.PostTRAsync<ResponseWithDistributorDetailsResponse, ResponseWithDistributorResponse>(ApiConstants.ResponseDetailsDistApi + $"{Response.DistributorCompanyId}/ResponseWithDistributor/{Response.Id}", Response, UserToken);
                        UserDialogs.Instance.HideHud();

                        if (json.Item1 != null)
                        {
                            // this will delete after Apple aproved                      
                            if (Response.TotalPayment == 0)
                            {
                                await AddPayment();
                                Response.TotalPayment += 50;
                            }
                            var toast = Toast.Make("Successfully for Add Response", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                            await toast.Show();

                            IsShowPaymentBtn = true;
                            //Controls.StaticMember.WayOfTab = 0;
                            //await App.Current!.MainPage!.Navigation.PushAsync(new HomeAgencyPage(new Tr_HomeViewModel(Rep, _service), Rep, _service));
                        }
                        else
                        {
                            var toast = Toast.Make($"Warning, {json.Item2}", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                            await toast.Show();
                        }
                    }
                    
                }
            }
            else
            {
                var toast = Toast.Make("Warning, Please Check to one service or more", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }

            IsBusy = true;
        }
        #endregion

        #region Hotel RelayCommand
        [RelayCommand]
        void SelectHotel(ResponseWithDistributorHotelResponse model)
        {

            var vm = new Tr_D_HotelServiceViewModel(Response.TotalPayment, model, Rep, _service);
            var page = new HotelServicePage(vm, Rep);
            page.BindingContext = vm;
            App.Current!.MainPage!.Navigation.PushAsync(page);

        }
        #endregion

        #region Transportaiton RelayCommand
        [RelayCommand]
        async Task SelectTransportaion(ResponseWithDistributorTransportResponse model)
        {
            var vm = new Tr_D_TransportaionServiceViewModel(Response.TotalPayment, model, Rep, _service);
            var page = new TransportaionServicePage(vm, Rep);
            page.BindingContext = vm;
            await App.Current!.MainPage!.Navigation.PushAsync(page);
        }
        #endregion

        #region Air Flight RelayCommand
        [RelayCommand]
        void SelectAirFlight(ResponseWithDistributorAirFlightResponse model)
        {
            var vm = new Tr_D_AirFlightServicesViewModel(Response.TotalPayment, model, Rep, _service);
            var page = new AirFlightServicePage(vm, Rep);
            page.BindingContext = vm;
            App.Current!.MainPage!.Navigation.PushAsync(page);
        }
        #endregion

        #region Visa RelayCommand
        [RelayCommand]
        async Task SelectVisa(ResponseWithDistributorVisaResponse model)
        {
            var vm = new Tr_D_VisaServiceViewModel(Response.TotalPayment, model, Rep, _service);
            var page = new VisaServicePage(vm, Rep);
            page.BindingContext = vm;
            await App.Current!.MainPage!.Navigation.PushAsync(page);
        }
        #endregion

        #region Methodes
        async Task Init(string distId, int ReqId)
        {
            UserDialogs.Instance.ShowLoading();
            await GetRequestDetailes(distId, ReqId);
            UserDialogs.Instance.HideHud();
        }
        async Task GetRequestDetailes(string distId, int ReqId)
        {
            IsBusy = false;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();

                var json = await Rep.GetAsync<ResponseWithDistributorDetailsResponse>(ApiConstants.ResponseDetailsDistApi + $"{distId}/ResponseWithDistributor/{ReqId}", UserToken);

                if (json != null)
                {
                    Response = json;
                    bool result = CheckChooseServices();
                    if(result)
                    {
                        IsShowPaymentBtn = true;
                    }
                    else
                    {
                        IsShowPaymentBtn = false;
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
                    AmountPayment = 50,
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
