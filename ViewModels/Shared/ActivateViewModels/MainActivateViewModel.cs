﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using CommunityToolkit.Maui.Alerts;
using System.Collections.ObjectModel;
using TripBliss.Constants;
using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.Models.ResponseWithDistributorVisaDetails;
using TripBliss.Pages.ActivateDetailsPages;

namespace TripBliss.ViewModels.ActivateViewModels
{
    public partial class MainActivateViewModel : BaseViewModel
    {
        int Id;
        int DisId;
        [ObservableProperty]
        ObservableCollection<ResponseWithDistributorHotelDetailsResponse> activeHotels = new ObservableCollection<ResponseWithDistributorHotelDetailsResponse>();
        [ObservableProperty]
        ObservableCollection<ResponseWithDistributorVisaDetailsResponse> activeVisa = new ObservableCollection<ResponseWithDistributorVisaDetailsResponse>();
        [ObservableProperty]
        ObservableCollection<ResponseWithDistributorAirFlightDetailsResponse> activeAirFlight = new ObservableCollection<ResponseWithDistributorAirFlightDetailsResponse>();
        [ObservableProperty]
        ObservableCollection<ResponseWithDistributorTransportDetailsResponse> activeTransport = new ObservableCollection<ResponseWithDistributorTransportDetailsResponse>();

        #region Services
        IGenericRepository Rep;
        readonly Services.Data.ServicesService _service;
        #endregion

        #region Cons
        public MainActivateViewModel(ResponseWithDistributorHotelResponse model, IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
            Id = model.Id;
            DisId = model.ResponseWithDistributorId;
            UserDialogs.Instance.ShowLoading();
            GetAllRooms(model.ResponseWithDistributorId, model.Id);
            UserDialogs.Instance.HideHud();
        }

        public MainActivateViewModel(ResponseWithDistributorTransportResponse model, IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
            Id = model.Id;
            DisId = model.ResponseWithDistributorId;
            UserDialogs.Instance.ShowLoading();
            GetAllTransport(model.ResponseWithDistributorId, model.Id);
            UserDialogs.Instance.HideHud();
        }

        public MainActivateViewModel(ResponseWithDistributorAirFlightResponse model, IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
            Id = model.Id;
            DisId = model.ResponseWithDistributorId;
            UserDialogs.Instance.ShowLoading();
            GetAllAirFlight( model.ResponseWithDistributorId , model.Id);
            UserDialogs.Instance.HideHud();
        }

        public MainActivateViewModel(ResponseWithDistributorVisaResponse model, IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
            Id = model.Id;
            DisId = model.ResponseWithDistributorId;
            UserDialogs.Instance.ShowLoading();
            GetAllVisa( model.ResponseWithDistributorId , model.Id);
            UserDialogs.Instance.HideHud();
        }
        #endregion

        #region Generl RelayCommand
        [RelayCommand]
        async Task BackButton()
        {
            await App.Current!.MainPage!.Navigation.PopAsync();
        }

        [RelayCommand]
        public async Task SelectHotel(ResponseWithDistributorHotelDetailsResponse model)
        {
            var vm = new HotelActivateViewModel(model, Rep, _service);
            var page = new HotelServicesActivateDetails();
            page.BindingContext = vm;
            await App.Current!.MainPage!.Navigation.PushAsync(page);
        }
        [RelayCommand]
        public async Task SelectTransportaion(ResponseWithDistributorTransportDetailsResponse model)
        {
            var vm = new TransportActivateViewModel(model, Rep, _service);
            var page = new TransportServicesActivateDetails();
            page.BindingContext = vm;
            await App.Current!.MainPage!.Navigation.PushAsync(page);
        }
        //[RelayCommand]
        //public async Task SelectAirFlight(ResponseWithDistributorAirFlightDetailsResponse model)
        //{
        //    var vm = new AirFlightActivateViewModel(model, Rep, _service);
        //    var page = new AirFlightServicesActivateDetails();
        //    page.BindingContext = vm;
        //    await App.Current!.MainPage!.Navigation.PushAsync(page);
        //}
        [RelayCommand]
        public async Task SelectVisa(ResponseWithDistributorVisaDetailsResponse model)
        {
            //var vm = new VisaActivateViewModel(model, Rep, _service);
            //var page = new VisaServicesActivateDetails();
            //page.BindingContext = vm;
            //await App.Current!.MainPage!.Navigation.PushAsync(page);
        }
        [RelayCommand]
        public async Task AddToRequest()
        {
            if (Constants.Permissions.CheckPermission(Constants.Permissions.Edit_Service))
            {
                if (ActiveHotels.Count > 0)
                    await AddHotelData();
                else if (ActiveTransport.Count > 0)
                    await AddTransportData();
                else if (ActiveAirFlight.Count > 0)
                    await AddAirFlightData();
                else if (ActiveVisa.Count > 0)
                    await AddVisaData();
            }
            else
            {
                var toast = Toast.Make("Permission not allowed for this action.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
        }
        #endregion


        public async Task GetAllRooms(int DisId, int Id)
        {
            IsBusy = false;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string id = Preferences.Default.Get(ApiConstants.distributorCompanyId, "");
                string UserToken = await _service.UserToken();
                if (!string.IsNullOrEmpty(UserToken))
                {
                    UserDialogs.Instance.ShowLoading();
                    var json = await Rep.GetAsync<ObservableCollection<ResponseWithDistributorHotelDetailsResponse>>(ApiConstants.HotelActive + $"{DisId}/{Id}", UserToken);
                    UserDialogs.Instance.HideHud();
                    if (json != null)
                    {
                        ActiveHotels!.Clear();
                        ActiveHotels = json;
                    }
                }

            }

            IsBusy = true;
        }

        public async Task GetAllTransport( int DisId, int Id)
        {
            IsBusy = false;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string id = Preferences.Default.Get(ApiConstants.distributorCompanyId, "");
                string UserToken = await _service.UserToken();
                if (!string.IsNullOrEmpty(UserToken))
                {
                    UserDialogs.Instance.ShowLoading();
                    var json = await Rep.GetAsync<ObservableCollection<ResponseWithDistributorTransportDetailsResponse>>(ApiConstants.TransportActive + $"{DisId}/{Id}", UserToken);
                    UserDialogs.Instance.HideHud();
                    if (json != null)
                    {
                        ActiveTransport!.Clear();
                        ActiveTransport = json;
                    }
                }

            }

            IsBusy = true;
        }

        public async Task GetAllAirFlight(int DisId, int Id)
        {
            IsBusy = false;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string id = Preferences.Default.Get(ApiConstants.distributorCompanyId, "");
                string UserToken = await _service.UserToken();
                if (!string.IsNullOrEmpty(UserToken))
                {
                    UserDialogs.Instance.ShowLoading();
                    var json = await Rep.GetAsync<ObservableCollection<ResponseWithDistributorAirFlightDetailsResponse>>(ApiConstants.AirFlightActive + $"{DisId}/{Id}", UserToken);
                    UserDialogs.Instance.HideHud();
                    if (json != null)
                    {
                        ActiveAirFlight!.Clear();
                        ActiveAirFlight = json;
                    }
                }

            }

            IsBusy = true;
        }

        public async Task GetAllVisa(int DisId, int Id)
        {
            IsBusy = false;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string id = Preferences.Default.Get(ApiConstants.distributorCompanyId, "");
                string UserToken = await _service.UserToken();
                if (!string.IsNullOrEmpty(UserToken))
                {
                    UserDialogs.Instance.ShowLoading();
                    var json = await Rep.GetAsync<ObservableCollection<ResponseWithDistributorVisaDetailsResponse>>(ApiConstants.VisaActive + $"{DisId}/{Id}", UserToken);
                    UserDialogs.Instance.HideHud();
                    if (json != null)
                    {
                        ActiveVisa!.Clear();
                        ActiveVisa = json;
                    }
                }

            }

            IsBusy = true;
        }

        public async Task AddHotelData()
        {
            IsBusy = false;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {

                string UserToken = await _service.UserToken();

                string id = Preferences.Default.Get(ApiConstants.distributorCompanyId, "");
                var json = await Rep.PostTRAsync<ObservableCollection<ResponseWithDistributorHotelDetailsResponse>, ObservableCollection<ResponseWithDistributorHotelDetailsResponse>>(ApiConstants.HotelActive + $"{DisId}/{Id}", ActiveHotels, UserToken);

                if (json.Item1 != null)
                {
                    var toast = Toast.Make("Successfully for Add Response", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                    await GetAllRooms(DisId,Id);
                }
                else
                {
                    var toast = Toast.Make($"Warning, {json.Item2}", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
            }

            IsBusy = true;
        }

        public async Task AddTransportData()
        {
            IsBusy = false;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {

                string UserToken = await _service.UserToken();

                string id = Preferences.Default.Get(ApiConstants.distributorCompanyId, "");
                var json = await Rep.PostTRAsync<ObservableCollection<ResponseWithDistributorTransportDetailsResponse>, ObservableCollection<ResponseWithDistributorTransportDetailsResponse>>(ApiConstants.TransportActive + $"{DisId}/{Id}", ActiveTransport, UserToken);

                if (json.Item1 != null)
                {
                    var toast = Toast.Make("Successfully for Add Response", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                    await GetAllTransport(DisId, Id);
                }
                else
                {
                    var toast = Toast.Make($"Warning, {json.Item2}", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
            }

            IsBusy = true;
        }

        public async Task AddAirFlightData()
        {
            IsBusy = false;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {

                string UserToken = await _service.UserToken();

                string id = Preferences.Default.Get(ApiConstants.distributorCompanyId, "");
                var json = await Rep.PostTRAsync<ObservableCollection<ResponseWithDistributorAirFlightDetailsResponse>, ObservableCollection<ResponseWithDistributorAirFlightDetailsResponse>>(ApiConstants.AirFlightActive + $"{DisId}/{Id}", ActiveAirFlight, UserToken);

                if (json.Item1 != null)
                {
                    var toast = Toast.Make("Successfully for Add Response", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                    await GetAllAirFlight(DisId, Id);
                }
                else
                {
                    var toast = Toast.Make($"Warning, {json.Item2}", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
            }

            IsBusy = true;
        }

        public async Task AddVisaData()
        {
            IsBusy = false;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {

                string UserToken = await _service.UserToken();

                string id = Preferences.Default.Get(ApiConstants.distributorCompanyId, "");
                var json = await Rep.PostTRAsync<ObservableCollection<ResponseWithDistributorVisaDetailsResponse>, ObservableCollection<ResponseWithDistributorVisaDetailsResponse>>(ApiConstants.VisaActive + $"{DisId}/{Id}", ActiveVisa, UserToken);

                if (json.Item1 != null)
                {
                    var toast = Toast.Make("Successfully for Add Response", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                    await GetAllVisa(DisId, Id);
                }
                else
                {
                    var toast = Toast.Make($"Warning, {json.Item2}", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
            }

            IsBusy = true;
        }
    }
}
