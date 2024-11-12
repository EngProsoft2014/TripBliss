using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using CommunityToolkit.Maui.Alerts;
using System.Collections.ObjectModel;
using TripBliss.Constants;
using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.Models.ResponseWithDistributorVisaDetails;
using TripBliss.Pages.ActivateDetailsPages;
using TripBliss.Pages.Shared;
using Syncfusion.Maui.DataSource.Extensions;

namespace TripBliss.ViewModels.ActivateViewModels
{
    public partial class MainActivateViewModel : BaseViewModel
    {
        string Id;
        string DisId;
        [ObservableProperty]
        ObservableCollection<ResponseWithDistributorHotelDetailsResponse> activeHotels = new ObservableCollection<ResponseWithDistributorHotelDetailsResponse>();
        [ObservableProperty]
        ObservableCollection<ResponseWithDistributorVisaDetailsResponse> activeVisa = new ObservableCollection<ResponseWithDistributorVisaDetailsResponse>();
        [ObservableProperty]
        ObservableCollection<ResponseWithDistributorAirFlightDetailsResponse> activeAirFlight = new ObservableCollection<ResponseWithDistributorAirFlightDetailsResponse>();
        [ObservableProperty]
        ObservableCollection<ResponseWithDistributorTransportDetailsResponse> activeTransport = new ObservableCollection<ResponseWithDistributorTransportDetailsResponse>();

        [ObservableProperty]
        bool isRequestHistoryTR;
        [ObservableProperty]
        bool isRequestHistoryDS;

        [ObservableProperty]
        bool isRequestHistory;

        #region Services
        IGenericRepository Rep;
        readonly Services.Data.ServicesService _service;
        #endregion

        #region Cons
        public MainActivateViewModel(bool _IsRequestHistoryTR, bool _IsRequestHistoryDS, ResponseWithDistributorHotelResponse model, IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
            Id = model.Id;
            DisId = model.ResponseWithDistributorId;
            IsRequestHistoryTR = _IsRequestHistoryTR;
            IsRequestHistoryDS = _IsRequestHistoryDS;
            CheckRequestHistory(_IsRequestHistoryTR, _IsRequestHistoryDS);
            UserDialogs.Instance.ShowLoading();
            GetAllRooms(model.ResponseWithDistributorId, model.Id);
            UserDialogs.Instance.HideHud();
        }

        public MainActivateViewModel(bool _IsRequestHistoryTR, bool _IsRequestHistoryDS, ResponseWithDistributorTransportResponse model, IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
            Id = model.Id;
            DisId = model.ResponseWithDistributorId;
            IsRequestHistoryTR = _IsRequestHistoryTR;
            IsRequestHistoryDS = _IsRequestHistoryDS;
            CheckRequestHistory(_IsRequestHistoryTR, _IsRequestHistoryDS);
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
            //var vm = new HotelActivateViewModel(IsRequestHistoryTR, IsRequestHistoryDS,model, Rep, _service);
            //var page = new HotelServicesActivateDetails();
            //page.BindingContext = vm;
            //await App.Current!.MainPage!.Navigation.PushAsync(page);

            var vm = new HotelActivateViewModel(IsRequestHistoryTR, IsRequestHistoryDS, model, Rep, _service);
            vm.HotelDetailsClose += (item) =>
            {
                try
                {
                    ActiveHotels.ToList().ForEach(f =>
                    {
                        if (f != null && ((f.CountRow != null && item.CountRow != null && f.CountRow == item.CountRow) || (!string.IsNullOrEmpty(f!.Id) && !string.IsNullOrEmpty(item.Id) && f!.Id == item.Id)))
                        {
                            f.GuestName = item.GuestName;
                            f.RoomRef = item.RoomRef;
                        }
                    });
                }
                catch (Exception ex)
                {

                }

            };
            var page = new HotelServicesActivateDetails();
            page.BindingContext = vm;
            await App.Current!.MainPage!.Navigation.PushAsync(page);
        }
        [RelayCommand]
        public async Task SelectTransportaion(ResponseWithDistributorTransportDetailsResponse model)
        {
            //var vm = new TransportActivateViewModel(IsRequestHistoryTR, IsRequestHistoryDS, model, Rep, _service);
            //var page = new TransportServicesActivateDetails();
            //page.BindingContext = vm;
            //await App.Current!.MainPage!.Navigation.PushAsync(page);

            var vm = new TransportActivateViewModel(IsRequestHistoryTR, IsRequestHistoryDS, model, Rep, _service);
            vm.TransportDetailsClose += (item) =>
            {
                try
                {
                    ActiveTransport.ToList().ForEach(f =>
                    {
                        if (f != null && ((f.CountRow != null && item.CountRow != null && f.CountRow == item.CountRow) || (!string.IsNullOrEmpty(f!.Id) && !string.IsNullOrEmpty(item.Id) &&  f!.Id == item.Id)))
                        {
                            f.LeaderName = item.LeaderName;
                            f.PlateNumber = item.PlateNumber;
                        }
                    });
                }
                catch (Exception ex)
                {

                }

            };
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
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.PermissionAlert, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
        }
        #endregion


        public async Task GetAllRooms(string DisId, string Id)
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
                        int count = 0;
                        foreach (var item in json)
                        {
                            if (item.Id == null)
                            {
                                count++;
                                item.CountRow = count;
                            }
                        }
                        ActiveHotels = json;
                    }
                }

            }


            IsBusy = true;
        }

        public async Task GetAllTransport(string DisId, string Id)
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

                        int count = 0;
                        foreach (var item in json)
                        {
                            if(item.Id == null)
                            {
                                count ++;
                                item.CountRow = count;
                            }
                        }
                        ActiveTransport = json;
                        
                    }
                }

            }


            IsBusy = true;
        }

        public async Task GetAllAirFlight(string DisId, string Id)
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

        public async Task GetAllVisa(string DisId, string Id)
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
                    var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Successfully_AddResponse, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                    await GetAllRooms(DisId,Id);
                }
                else
                {
                    var toast = Toast.Make($"{json.Item2!.errors!.FirstOrDefault().Value}", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
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
                    var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Successfully_AddResponse, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                    await GetAllTransport(DisId, Id);
                }
                else
                {
                    var toast = Toast.Make($"{json.Item2!.errors!.FirstOrDefault().Value}", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
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
                    var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Successfully_AddResponse, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                    await GetAllAirFlight(DisId, Id);
                }
                else
                {
                    var toast = Toast.Make($"{json.Item2!.errors!.FirstOrDefault().Value}", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
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
                    var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Successfully_AddResponse, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                    await GetAllVisa(DisId, Id);
                }
                else
                {
                    var toast = Toast.Make($"{json.Item2!.errors!.FirstOrDefault().Value}", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
            }


            IsBusy = true;
        }

        void CheckRequestHistory(bool _IsRequestHistoryTR, bool _IsRequestHistoryDS)
        {
            string Tr_Id = Preferences.Default.Get(ApiConstants.travelAgencyCompanyId, "");
            string Ds_Id = Preferences.Default.Get(ApiConstants.distributorCompanyId, "");

            if(_IsRequestHistoryTR == true && _IsRequestHistoryDS == false && !string.IsNullOrEmpty(Tr_Id))
            {
                IsRequestHistory = true;
            }
            else if(_IsRequestHistoryDS == true && _IsRequestHistoryTR == false && !string.IsNullOrEmpty(Ds_Id))
            {
                IsRequestHistory = true;
            }
            else
            {
                IsRequestHistory = false;
            }
        }
    }
}
