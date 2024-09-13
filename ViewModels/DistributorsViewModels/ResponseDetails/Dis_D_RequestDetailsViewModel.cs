﻿using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using TripBliss.Constants;
using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.Pages.DistributorsPages;
using TripBliss.Pages.DistributorsPages.ResponseDetailes;

namespace TripBliss.ViewModels.DistributorsViewModels.ResponseDetails
{
    public partial class Dis_D_RequestDetailsViewModel : BaseViewModel
    {

        #region prop
        [ObservableProperty]
        ResponseWithDistributorDetailsResponse response = new ResponseWithDistributorDetailsResponse();
        [ObservableProperty]
        public List<ResponseWithDistributorHotelResponse>? hotels = new List<ResponseWithDistributorHotelResponse>();
        [ObservableProperty]
        public List<ResponseWithDistributorTransportResponse>? transports = new List<ResponseWithDistributorTransportResponse>();
        [ObservableProperty]
        public List<ResponseWithDistributorAirFlightResponse>? airFlights = new List<ResponseWithDistributorAirFlightResponse>();
        [ObservableProperty]
        public List<ResponseWithDistributorVisaResponse>? visas = new List<ResponseWithDistributorVisaResponse>();


        #endregion

        #region Services
        IGenericRepository Rep;
        readonly Services.Data.ServicesService _service;
        #endregion

        #region Cons
        public Dis_D_RequestDetailsViewModel(int ReqId, IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            Lang = Preferences.Default.Get("Lan", "en");
            _service = service;
            Init(ReqId);
        }
        #endregion

        #region Generl RelayCommand
        [RelayCommand]
        async Task BackButton()
        {
            await App.Current!.MainPage!.Navigation.PopAsync();
        }
        #endregion

        #region Hotel RelayCommand
        [RelayCommand]
        async Task AddHotel()
        {
            await App.Current!.MainPage!.Navigation.PushAsync(new HotelServicePage(new Dis_D_HotelServiceViewModel(Rep)));
        }
        [RelayCommand]
        async Task SelectHotel(ResponseWithDistributorHotelResponse model)
        {
            var vm = new Dis_D_HotelServiceViewModel(Response.TotalPayment,model, Rep, _service);
            var page = new HotelServicePage(vm);
            page.BindingContext = vm;
            await App.Current!.MainPage!.Navigation.PushAsync(page);
        }
        #endregion

        #region Transportaiton RelayCommand
        [RelayCommand]
        async Task AddTransportaion()
        {
            await App.Current!.MainPage!.Navigation.PushAsync(new TransportaionServicePage(new Dis_D_TransportaionServiceViewModel(Rep)));
        }
        [RelayCommand]
        async Task SelectTransportaion(ResponseWithDistributorTransportResponse model)
        {
            var vm = new Dis_D_TransportaionServiceViewModel(Response.TotalPayment, model, Rep, _service);
            var page = new TransportaionServicePage(vm);
            page.BindingContext = vm;
            await App.Current!.MainPage!.Navigation.PushAsync(page);
        }
        #endregion

        #region Air Flight RelayCommand
        [RelayCommand]
        async Task AddAirFlight()
        {
            await App.Current!.MainPage!.Navigation.PushAsync(new AirFlightServicePage(new Dis_D_AirFlightServicesViewModel(Rep)));
        }
        [RelayCommand]
        async Task SelectAirFlight(ResponseWithDistributorAirFlightResponse model)
        {
            var vm = new Dis_D_AirFlightServicesViewModel(Response.TotalPayment, model, Rep, _service);
            var page = new AirFlightServicePage(vm);
            page.BindingContext = vm;
            await App.Current!.MainPage!.Navigation.PushAsync(page);
        }
        #endregion

        #region Visa RelayCommand
        [RelayCommand]
        void AddVisa()
        {
            App.Current.MainPage.Navigation.PushAsync(new VisaServicePage(new Dis_D_VisaServiceViewModel(Rep)));
        }
        [RelayCommand]
        async Task SelectVisa(ResponseWithDistributorVisaResponse model)
        {
            var vm = new Dis_D_VisaServiceViewModel(Response.TotalPayment, model, Rep,_service);
            var page = new VisaServicePage(vm);
            page.BindingContext = vm;
            await App.Current!.MainPage!.Navigation.PushAsync(page);
        }
        #endregion

        #region Methodes

        async Task Init(int ReqId)
        {
            UserDialogs.Instance.ShowLoading();
            await GetRequestDetailes(ReqId);
            UserDialogs.Instance.HideHud();
        }
        async Task GetRequestDetailes(int ReqId)
        {

            string DisId = Preferences.Default.Get(ApiConstants.distributorCompanyId, "");
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();

                var json = await Rep.GetAsync<ResponseWithDistributorDetailsResponse>(ApiConstants.ResponseDetailsDistApi + $"{DisId}/ResponseWithDistributor/{ReqId}", UserToken);
                
                if (json != null)
                {
                    Response = json;
                }
            }
        }

       
        #endregion

        #region RelayCommand
        [RelayCommand]
        [Obsolete]
        async Task AddToRequest()
        {
            
            
            IsBusy = false;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {

                string UserToken = await _service.UserToken();

                string id = Preferences.Default.Get(ApiConstants.distributorCompanyId, "");
                var json = await Rep.PostTRAsync<ResponseWithDistributorDetailsResponse, ResponseWithDistributorResponse>(ApiConstants.ResponseDetailsDistApi + $"{id}/ResponseWithDistributor/{Response.Id}", Response, UserToken);

                if (json.Item1 != null)
                {
                    var toast = Toast.Make("Successfully for Add Response", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();

                    Controls.StaticMember.WayOfTab = 0;
                    await App.Current!.MainPage!.Navigation.PushAsync(new HomeDistributorsPage(new Dis_HomeViewModel(Rep, _service), Rep, _service));
                }
                else
                {
                    var toast = Toast.Make($"Warning, {json.Item2}", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
            }

            IsBusy = true;
        } 
        #endregion
    }
}
