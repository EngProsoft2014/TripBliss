using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using Mopups.Services;
using TripBliss.Constants;
using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.Models.ResponseWithDistributor;
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
        [ObservableProperty]
        bool isShowReviewBtn;
        [ObservableProperty]
        bool isRequestHistory;
        [ObservableProperty]
        string requestId;

        #endregion

        #region Services
        IGenericRepository Rep;
        readonly Services.Data.ServicesService _service;
        #endregion

        #region Cons
        public Dis_D_RequestDetailsViewModel(string ReqId, IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            Lang = Preferences.Default.Get("Lan", "en");
            _service = service;
            RequestId = ReqId;
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
            var vm = new Dis_D_HotelServiceViewModel(IsRequestHistory,Response.TotalPayment,model, Rep, _service);
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
            var vm = new Dis_D_TransportaionServiceViewModel(IsRequestHistory,Response.TotalPayment, model, Rep, _service);
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
            var vm = new Dis_D_AirFlightServicesViewModel(IsRequestHistory, Response.TotalPayment, model, Rep, _service);
            var page = new AirFlightServicePage(vm);
            page.BindingContext = vm;
            await App.Current!.MainPage!.Navigation.PushAsync(page);
        }
        #endregion

        #region Visa RelayCommand
        [RelayCommand]
        void AddVisa()
        {
            App.Current!.MainPage!.Navigation.PushAsync(new VisaServicePage(new Dis_D_VisaServiceViewModel(Rep)));
        }
        [RelayCommand]
        async Task SelectVisa(ResponseWithDistributorVisaResponse model)
        {
            var vm = new Dis_D_VisaServiceViewModel(IsRequestHistory, Response.TotalPayment, model, Rep,_service);
            var page = new VisaServicePage(vm);
            page.BindingContext = vm;
            await App.Current!.MainPage!.Navigation.PushAsync(page);
        }
        #endregion

        #region Methodes

        async void Init(string ReqId)
        {
            UserDialogs.Instance.ShowLoading();
            await GetRequestDetailes(ReqId);
            UserDialogs.Instance.HideHud();
        }

        async Task GetRequestDetailes(string ReqId)
        {

            string DisId = Preferences.Default.Get(ApiConstants.distributorCompanyId, "");
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();

                var json = await Rep.GetAsync<ResponseWithDistributorDetailsResponse>(ApiConstants.ResponseDetailsDistApi + $"{DisId}/ResponseWithDistributor/{ReqId}", UserToken);
                
                if (json != null)
                {
                    Response = json;

                    if(!string.IsNullOrEmpty(DisId) && !string.IsNullOrEmpty(Response.ReviewUserDistributorName))
                    {
                        IsRequestHistory = true;
                    }
                    else
                    {
                        IsRequestHistory = false;
                        CheckShowReview();
                    }    
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

            var ResponseAirFlt = Response?.ResponseWithDistributorAirFlight?.Where(x => x.AcceptPriceDis == true).FirstOrDefault();
            var ResponseHotel = Response?.ResponseWithDistributorHotel?.Where(x => x.AcceptPriceDis == true).FirstOrDefault();
            var ResponseTrans = Response?.ResponseWithDistributorTransport?.Where(x => x.AcceptPriceDis == true).FirstOrDefault();
            var ResponseVisa = Response?.ResponseWithDistributorVisa?.Where(x => x.AcceptPriceDis == true).FirstOrDefault();

            if (ResponseAirFlt != null || ResponseHotel != null || ResponseTrans != null || ResponseVisa != null)
            {
                bool answer = await App.Current!.MainPage!.DisplayAlert(TripBliss.Resources.Language.AppResources.Question, TripBliss.Resources.Language.AppResources.AreYouAcceptThisFinallPrice, TripBliss.Resources.Language.AppResources.Yes, TripBliss.Resources.Language.AppResources.No);

                if (Connectivity.NetworkAccess == NetworkAccess.Internet && answer)
                {

                    string UserToken = await _service.UserToken();

                    string id = Preferences.Default.Get(ApiConstants.distributorCompanyId, "");

                    UserDialogs.Instance.ShowLoading();
                    //var json = await Rep.PostTRAsync<ResponseWithDistributorDetailsResponse, ResponseWithDistributorResponse>(ApiConstants.ResponseDetailsDistApi + $"{id}/ResponseWithDistributor/{Response.Id}", Response, UserToken);
                    var json = await Rep.PostTRAsync<ResponseWithDistributorDetailsResponse, ResponseWithDistributorDetailsResponse>(ApiConstants.ResponseDetailsDistApi + $"{id}/ResponseWithDistributor/{Response.Id}", Response, UserToken);
                    UserDialogs.Instance.HideHud();

                    if (json.Item1 != null)
                    {
                        var toast = Toast.Make(TripBliss.Resources.Language.AppResources.AddResponseSuccess, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                        await toast.Show();

                        Response = new ResponseWithDistributorDetailsResponse();
                        Response = json.Item1;

                        //Controls.StaticMember.WayOfTab = 0;
                        //await App.Current!.MainPage!.Navigation.PushAsync(new HomeDistributorsPage(new Dis_HomeViewModel(Rep, _service), Rep, _service));
                    }
                    else
                    {
                        var toast = Toast.Make($"{json.Item2!.errors!.FirstOrDefault().Value}", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                        await toast.Show();
                    }
                }
            }
            else
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.PutPriceAlert, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }             

            IsBusy = true;
        }


        [RelayCommand]
        [Obsolete]
        async Task ReviewClicked()
        {
            IsBusy = false;

            bool answer = await App.Current!.MainPage!.DisplayAlert(TripBliss.Resources.Language.AppResources.Question, TripBliss.Resources.Language.AppResources.MakeReviewAndFinishRequest, TripBliss.Resources.Language.AppResources.Yes, TripBliss.Resources.Language.AppResources.No);
            if (answer)
            {
                var vieModel = new Dis_ReviewViewModel(Rep, _service);
                var page = new Pages.DistributorsPages.ResponseDetailes.Ds_ReviewPopup(vieModel);
                vieModel.ReviewClose += async (model) =>
                {
                    if (model != null)
                    {
                        UserDialogs.Instance.ShowLoading();
                        string UserToken = await _service.UserToken();
                        var json = await Rep.PostTRAsync<ResponseWithDistributorReviewDistributorRequest, string>(string.Format($"/Distributor/{Response.DistributorCompanyId}/ResponseWithDistributor/{Response.Id}/ReviewToTravelAgency"), model, UserToken);
                        UserDialogs.Instance.HideHud();
                        if (json.Item1 == null && json.Item2 == null)
                        {
                            await App.Current!.MainPage!.Navigation.PushAsync(new HomeDistributorsPage(new Dis_HomeViewModel(Rep, _service), Rep, _service));
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
            }

            IsBusy = true;
        }

        [RelayCommand]
        [Obsolete]
        async Task DeleteRequest()
        {
            IsBusy = false;
            bool answer = await App.Current!.MainPage!.DisplayAlert(TripBliss.Resources.Language.AppResources.Question, TripBliss.Resources.Language.AppResources.Do_You_Want_Delete_Response, TripBliss.Resources.Language.AppResources.Yes, TripBliss.Resources.Language.AppResources.No);
            if (answer)
            {
                if (Constants.Permissions.CheckPermission(Constants.Permissions.Delete_ResponseWithDistributer))
                {
                    string DisId = Preferences.Default.Get(ApiConstants.distributorCompanyId, "");
                    if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                    {
                        string UserToken = await _service.UserToken();

                        var json = await Rep.PostAsync<ResponseWithDistributorDetailsResponse>(ApiConstants.ResponseDetailsDistApi + $"{DisId}/ResponseWithDistributor/{RequestId}/Delete", null, UserToken);

                        if (json == null)
                        {
                            var toast = Toast.Make(TripBliss.Resources.Language.AppResources.ResponseDeleteSuccess, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                            await toast.Show();

                            await App.Current!.MainPage!.Navigation.PushAsync(new HomeDistributorsPage(new Dis_HomeViewModel(Rep, _service), Rep, _service));
                        }
                        else
                        {
                            var toast = Toast.Make(TripBliss.Resources.Language.AppResources.ErrorTryAgain, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                            await toast.Show();
                        }
                    }
                }
                else
                {
                    var toast = Toast.Make(TripBliss.Resources.Language.AppResources.PermissionAlert, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
            }

            IsBusy = true;
        }

        void CheckShowReview()
        {
            if (Response?.TotalPriceAgentAccept > 0 && Response?.TotalPriceAgentAccept == Response?.TotalPayment)
            {
                if (string.IsNullOrEmpty(Response?.ReviewUserDistributorName))
                {
                    IsShowReviewBtn = true;
                }
                else
                {
                    IsShowReviewBtn = false;
                }
            }
            else
            {
                IsShowReviewBtn = false;
            }
        }
        #endregion
    }
}
