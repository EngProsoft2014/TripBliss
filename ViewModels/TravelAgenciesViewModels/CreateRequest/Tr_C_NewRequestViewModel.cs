using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using Microsoft.VisualBasic;
using Plugin.FirebasePushNotifications;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Constants;
using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.Pages.Shared;
using TripBliss.Pages.TravelAgenciesPages;
using TripBliss.Pages.TravelAgenciesPages.CreateRequest;
using TripBliss.Services.Data;



namespace TripBliss.ViewModels.TravelAgenciesViewModels.CreateRequest
{
    public partial class Tr_C_NewRequestViewModel : BaseViewModel
    {


        #region prop

        [ObservableProperty]
        RequestTravelAgencyRequest requestTravelAgency = new RequestTravelAgencyRequest();
        [ObservableProperty]
        ObservableCollection<DistributorCompanyResponse> distributorCompanies;
        [ObservableProperty]
        ObservableCollection<ResponseWithDistributorRequest> distributorRequests = new ObservableCollection<ResponseWithDistributorRequest>();

        [ObservableProperty]
        ObservableCollection<RequestTravelAgencyAirFlightRequest> lstTravelAgencyAirFlightRequest = new ObservableCollection<RequestTravelAgencyAirFlightRequest>();
        [ObservableProperty]
        ObservableCollection<RequestTravelAgencyAirFlightResponse> lstTravelAgencyAirFlightResponse = new ObservableCollection<RequestTravelAgencyAirFlightResponse>();
        [ObservableProperty]
        ObservableCollection<RequestTravelAgencyHotelRequest> lstTravelAgencyHotelRequest = new ObservableCollection<RequestTravelAgencyHotelRequest>();
        [ObservableProperty]
        ObservableCollection<RequestTravelAgencyHotelResponse> lstTravelAgencyHotelResponse = new ObservableCollection<RequestTravelAgencyHotelResponse>();
        [ObservableProperty]
        ObservableCollection<RequestTravelAgencyVisaRequest> lstTravelAgencyVisaRequest = new ObservableCollection<RequestTravelAgencyVisaRequest>();
        [ObservableProperty]
        ObservableCollection<RequestTravelAgencyVisaResponse> lstTravelAgencyVisaResponse = new ObservableCollection<RequestTravelAgencyVisaResponse>();
        [ObservableProperty]
        ObservableCollection<RequestTravelAgencyTransportRequest> lstTravelAgencyTransportRequest = new ObservableCollection<RequestTravelAgencyTransportRequest>();
        [ObservableProperty]
        ObservableCollection<RequestTravelAgencyTransportResponse> lstTravelAgencyTransportResponse = new ObservableCollection<RequestTravelAgencyTransportResponse>();


        #endregion

        readonly Services.Data.ServicesService _service;
        readonly IGenericRepository Rep;
        readonly Services.Data.SignalRService _signalRService;
        readonly INotificationService _notificationService;
        readonly IFirebasePushNotification _firebasePushNotification;
        public Tr_C_NewRequestViewModel(IGenericRepository GenericRep, Services.Data.ServicesService service, Services.Data.SignalRService signalRService, INotificationService notificationService, IFirebasePushNotification firebasePushNotification)
        {
            Rep = GenericRep;
            _service = service;
            _signalRService = signalRService;
            _notificationService = notificationService;
            _firebasePushNotification = firebasePushNotification; 
            Lang = Preferences.Default.Get("Lan", "en");
            //LoadData();
            //LoadTransportaionData();
            //LoadAirFlightData();
            //LoadVisaData();
        }
        public Tr_C_NewRequestViewModel(ObservableCollection<DistributorCompanyResponse> distributors, IGenericRepository GenericRep, Services.Data.ServicesService service)
        {
            Rep = GenericRep;
            Lang = Preferences.Default.Get("Lan", "en");
            DistributorCompanies = distributors;
            _service = service;
            //LoadData();
            //LoadTransportaionData();
            //LoadAirFlightData();
            //LoadVisaData();
        }

        #region Generl RelayCommand
        [RelayCommand]
        void BackButtonClicked()
        {
            App.Current!.MainPage!.Navigation.PopAsync();
        }
        #endregion

        #region Hotel RelayCommand
        [RelayCommand]
        void AddHotel()
        {
            var vm = new Tr_C_HotelServiceViewModel(Rep, _service);
            void handler(RequestTravelAgencyHotelRequest request, RequestTravelAgencyHotelResponse response)
            {
                try
                {
                    LstTravelAgencyHotelRequest.Add(request);
                    LstTravelAgencyHotelResponse.Add(response);
                }
                catch (Exception ex)
                {
                    // Log exception
                }
                finally
                {
                    vm.HotelClose -= handler; // Unsubscribe after handling
                }
            };

            vm.HotelClose += handler;

            var page = new HotelServicePage(vm, Rep);

            App.Current!.MainPage!.Navigation.PushAsync(page);
        }
        [RelayCommand]
        void SelectHotel(RequestTravelAgencyHotelResponse model)
        {
            var index = LstTravelAgencyHotelResponse.IndexOf(model);
            var vm = new Tr_C_HotelServiceViewModel(model, Rep, _service);
            void handler(RequestTravelAgencyHotelRequest request, RequestTravelAgencyHotelResponse response)
            {
                try
                {
                    LstTravelAgencyHotelRequest.Add(request);
                    LstTravelAgencyHotelResponse.Add(response);
                }
                catch (Exception ex)
                {
                    // Log exception
                }
                finally
                {
                    vm.HotelClose -= handler; // Unsubscribe after handling
                }
            };
            vm.HotelClose += handler;
            var page = new HotelServicePage(vm, Rep);
            page.BindingContext = vm;
            App.Current!.MainPage!.Navigation.PushAsync(page);
        }

        [RelayCommand]
        void DeletHotel(RequestTravelAgencyHotelResponse model)
        {
            int index = LstTravelAgencyHotelResponse.IndexOf(model);
            LstTravelAgencyHotelResponse.Remove(model);
            LstTravelAgencyHotelRequest.RemoveAt(index);
        }
        #endregion

        #region Transportaiton RelayCommand
        [RelayCommand]
        void AddTransportaion()
        {
            var vm = new Tr_C_TransportaionServiceViewModel(Rep, _service);
            vm.TransportClose += (TransportRequest, TransportResponse) =>
            {
                try
                {
                    LstTravelAgencyTransportRequest.Add(TransportRequest);
                    LstTravelAgencyTransportResponse.Add(TransportResponse);
                }
                catch (Exception ex)
                {

                }
            };
            App.Current!.MainPage!.Navigation.PushAsync(new TransportaionServicePage(vm, Rep));
        }
        [RelayCommand]
        void SelectTransportaion(RequestTravelAgencyTransportResponse model)
        {
            var index = LstTravelAgencyTransportResponse.IndexOf(model);
            var vm = new Tr_C_TransportaionServiceViewModel(model, Rep, _service);
            vm.TransportClose += (TransportRequest, TransportResponse) =>
            {
                if (TransportResponse != model)
                {
                    LstTravelAgencyTransportResponse.Remove(model);
                    LstTravelAgencyTransportRequest.Remove(LstTravelAgencyTransportRequest[index]);

                    LstTravelAgencyTransportRequest.Add(TransportRequest);
                    LstTravelAgencyTransportResponse.Add(TransportResponse);
                }

            };
            var page = new TransportaionServicePage(vm, Rep);
            App.Current!.MainPage!.Navigation.PushAsync(page);
        }
        [RelayCommand]
        void DeletTransPortation(RequestTravelAgencyTransportResponse model)
        {
            int index = LstTravelAgencyTransportResponse.IndexOf(model);
            LstTravelAgencyTransportResponse.Remove(model);
            LstTravelAgencyTransportRequest.RemoveAt(index);
        }

        #endregion

        #region Air Flight RelayCommand
        [RelayCommand]
        void AddAirFlight()
        {
            var pageView = new Tr_C_AirFlightServicesViewModel(Rep, _service);
            pageView.AirFlightClose += (AirFlightRequest, AirFlightResponse) =>
            {
                try
                {
                    LstTravelAgencyAirFlightResponse.Add(AirFlightResponse);
                    LstTravelAgencyAirFlightRequest.Add(AirFlightRequest);
                }
                catch (Exception ex)
                {

                }


            };

            App.Current!.MainPage!.Navigation.PushAsync(new AirFlightServicePage(pageView));
        }
        [RelayCommand]
        void SelectAirFlight(RequestTravelAgencyAirFlightResponse Response)
        {
            var index = LstTravelAgencyAirFlightResponse.IndexOf(Response);
            var vm = new Tr_C_AirFlightServicesViewModel(Response, Rep, _service);
            vm.AirFlightClose += (AirFlightRequest, AirFlightResponse) =>
            {
                if (AirFlightResponse != Response)
                {
                    LstTravelAgencyAirFlightResponse.Remove(Response);
                    LstTravelAgencyAirFlightRequest.Remove(LstTravelAgencyAirFlightRequest[index]);

                    LstTravelAgencyAirFlightResponse.Add(AirFlightResponse);
                    LstTravelAgencyAirFlightRequest.Add(AirFlightRequest);
                }

            };
            var page = new AirFlightServicePage(vm);
            App.Current!.MainPage!.Navigation.PushAsync(page);
        }
        [RelayCommand]
        void DeletAirFlight(RequestTravelAgencyAirFlightResponse model)
        {
            int index = LstTravelAgencyAirFlightResponse.IndexOf(model);
            LstTravelAgencyAirFlightResponse.Remove(model);
            LstTravelAgencyAirFlightRequest.RemoveAt(index);

            //LstTravelAgencyAirFlightRequest.Remove(LstTravelAgencyAirFlightRequest.Where(s => s.Id == model.Id).FirstOrDefault()!);
        }
        #endregion

        #region Visa RelayCommand
        [RelayCommand]
        void AddVisa()
        {
            var vm = new Tr_C_VisaServiceViewModel(Rep, _service);
            vm.VisaClose += (VisaRequest, VisaResponse) =>
            {
                try
                {
                    LstTravelAgencyVisaRequest.Add(VisaRequest);
                    LstTravelAgencyVisaResponse.Add(VisaResponse);
                }
                catch (Exception ex)
                {

                }

            };

            App.Current!.MainPage!.Navigation.PushAsync(new VisaServicePage(vm, Rep));
        }
        [RelayCommand]
        void SelectVisa(RequestTravelAgencyVisaResponse model)
        {
            var index = LstTravelAgencyVisaResponse.IndexOf(model);
            var vm = new Tr_C_VisaServiceViewModel(model, Rep, _service);

            vm.VisaClose += (VisaRequest, VisaResponse) =>
            {
                if (model != VisaResponse)
                {
                    LstTravelAgencyVisaResponse.Remove(model);
                    LstTravelAgencyVisaRequest.Remove(LstTravelAgencyVisaRequest[index]);

                    LstTravelAgencyVisaRequest.Add(VisaRequest);
                    LstTravelAgencyVisaResponse.Add(VisaResponse);
                }

            };

            var page = new VisaServicePage(vm, Rep);
            App.Current!.MainPage!.Navigation.PushAsync(page);
        }
        [RelayCommand]
        void DeletVisa(RequestTravelAgencyVisaResponse model)
        {
            int index = LstTravelAgencyVisaResponse.IndexOf(model);
            LstTravelAgencyVisaResponse.Remove(model);
            LstTravelAgencyVisaRequest.RemoveAt(index);
        }
        #endregion

        #region AddRequest RelayCommand
        [RelayCommand]
        async Task AddToRequest()
        {
            if (LstTravelAgencyHotelRequest.Count == 0 & LstTravelAgencyTransportRequest.Count == 0 & LstTravelAgencyAirFlightRequest.Count == 0 & LstTravelAgencyVisaRequest.Count == 0)
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.add_at_least_one_service, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else
            {
                string RequestName = await App.Current!.MainPage!.DisplayPromptAsync(TripBliss.Resources.Language.AppResources.Complete_info, TripBliss.Resources.Language.AppResources.Add_Request_Name, TripBliss.Resources.Language.AppResources.OK, TripBliss.Resources.Language.AppResources.Cancel);
                if (string.IsNullOrEmpty(RequestName))
                {
                    var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Add_Request_name_is_required, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
                else if (RequestName.Length < 2 || RequestName.Length > 25)
                {
                    var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Request_name_required_count, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
                else
                {
                    RequestTravelAgency = new RequestTravelAgencyRequest();

                    RequestTravelAgency.RequestName = RequestName;
                    RequestTravelAgency.EndRequest = Controls.StaticMember.EndRequestStatic;
                    RequestTravelAgency.RequestTravelAgencyHotel = LstTravelAgencyHotelRequest.ToList();
                    RequestTravelAgency.RequestTravelAgencyTransport = LstTravelAgencyTransportRequest.ToList();
                    RequestTravelAgency.RequestTravelAgencyAirFlight = LstTravelAgencyAirFlightRequest.ToList();
                    RequestTravelAgency.RequestTravelAgencyVisa = LstTravelAgencyVisaRequest.ToList();

                    RequestTravelAgency.ResponseWithDistributor = DistributorCompanies.Select(k => new ResponseWithDistributorRequest { DistributorCompanyId = k.Id }).ToList();

                    IsBusy = false;

                    if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                    {

                        string UserToken = await _service.UserToken();

                        string Tr_Id = Preferences.Default.Get(ApiConstants.travelAgencyCompanyId, "");
                        //string Dis_Id = Preferences.Default.Get(ApiConstants.distributorCompanyId, "");
                        UserDialogs.Instance.ShowLoading();
                        var json = await Rep.PostTRAsync<RequestTravelAgencyRequest, RequestTravelAgencyResponse>(ApiConstants.AddRequestApi + $"{Tr_Id}/RequestTravelAgency", RequestTravelAgency, UserToken);
                        UserDialogs.Instance.HideHud();
                        if (json.Item1 != null)
                        {
                            var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Successfully_AddRequest, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                            await toast.Show();

                            // إظهار إشعار للمستخدم الحالي (تأكيد إرسال الطلب)
                            var confirmationNotification = new NotificationDto
                            {
                                Type = "RequestSent",
                                Title = "تم إرسال الطلب",
                                Message = $"تم إرسال طلب {RequestName} إلى {DistributorCompanies.Count} موزع",
                                RequestId = json.Item1.Id,
                                SenderId = Tr_Id != "" ? Tr_Id : "",
                                SenderName = "نظام الإشعارات"
                            };

                            Controls.StaticMember.WayOfTab = 0;
                            await App.Current!.MainPage!.Navigation.PushAsync(new HomeAgencyPage(new Tr_HomeViewModel(Rep, _service, _signalRService, _notificationService, _firebasePushNotification), Rep, _service, _signalRService, _notificationService, _firebasePushNotification));
                        }
                        else
                        {
                            var toast = Toast.Make($"{json.Item2!.errors!.FirstOrDefault().Value}", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                            await toast.Show();
                        }
                    }


                    IsBusy = true;
                }

            }
        }


        #endregion

    }
}
