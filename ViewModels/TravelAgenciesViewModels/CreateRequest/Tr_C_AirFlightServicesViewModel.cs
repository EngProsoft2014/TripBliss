using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using System.Collections.ObjectModel;
using TripBliss.Constants;
using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.Exceptions;


namespace TripBliss.ViewModels.TravelAgenciesViewModels.CreateRequest
{
    public partial class Tr_C_AirFlightServicesViewModel : BaseViewModel
    {
        #region prop
        [ObservableProperty]
        RequestTravelAgencyAirFlightRequest airFlightRequestModel = new RequestTravelAgencyAirFlightRequest();
        [ObservableProperty]
        RequestTravelAgencyAirFlightResponse airFlightResponseModel = new RequestTravelAgencyAirFlightResponse();
        [ObservableProperty]
        ObservableCollection<AirFlightResponse> airFlights = new ObservableCollection<AirFlightResponse>();
        [ObservableProperty]
        ObservableCollection<ClassAirFlightResponse> classes = new ObservableCollection<ClassAirFlightResponse>();
        [ObservableProperty]
        int adult;
        [ObservableProperty]
        int chiled;
        [ObservableProperty]
        int infant;
        [ObservableProperty]
        AirFlightResponse airFlightSelected;
        [ObservableProperty]
        ClassAirFlightResponse classSelected;

        #endregion

        public delegate void AirFlightDelegte(RequestTravelAgencyAirFlightRequest AirFlightRequest, RequestTravelAgencyAirFlightResponse AirFlightResponse);
        public event AirFlightDelegte AirFlightClose;

        #region Services
        IGenericRepository Rep;
        readonly Services.Data.ServicesService _service;
        #endregion

        #region Cons
        public Tr_C_AirFlightServicesViewModel(IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
            AirFlightRequestModel!.Date = DateTime.Now;
            Inti();
        }
        public Tr_C_AirFlightServicesViewModel(RequestTravelAgencyAirFlightResponse model, IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            AirFlightResponseModel = model;
            _service = service;
            AirFlightRequestModel!.Date = DateTime.Now;
            Inti();


        }
        #endregion

        #region Methods
        async Task Inti()
        {
            UserDialogs.Instance.ShowLoading();
            await Task.WhenAll(GetAirFlights(),GetClasses());
            UserDialogs.Instance.HideHud();
        }
        async Task GetAirFlights()
        {

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();

                var json = await Rep.GetAsync<ObservableCollection<AirFlightResponse>>(ApiConstants.GetAllAirFlightApi, UserToken);

                if (json != null)
                {
                    AirFlights = json;
                }
            }
        }

        async Task GetClasses()
        {

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();

                var json = await Rep.GetAsync<ObservableCollection<ClassAirFlightResponse>>(ApiConstants.GetAllClassesAirFlightApi, UserToken);

                if (json != null)
                {
                    Classes = json;
                }
            }
        }
        #endregion

        #region RelayCommand
        [RelayCommand]
        void OnBackPressed()
        {
            App.Current!.MainPage!.Navigation.PopAsync();
        }

        [RelayCommand]
        void AddAdult()
        {
            if (AirFlightRequestModel.InfoAdultCount >=0)
            {
                AirFlightRequestModel.InfoAdultCount += 1;
            }
        }
        [RelayCommand]
        void DeletAdult()
        {
            if (AirFlightRequestModel.InfoAdultCount > 0)
            {
                AirFlightRequestModel.InfoAdultCount -= 1;
            }
        }

        [RelayCommand]
        void AddChild()
        {
            if (AirFlightRequestModel.InfoChildCount >= 0)
            {
                AirFlightRequestModel.InfoChildCount += 1;
            }
        }
        [RelayCommand]
        void DeletChild()
        {
            if (AirFlightRequestModel.InfoChildCount > 0)
            {
                AirFlightRequestModel.InfoChildCount -= 1;
            }
        }
        [RelayCommand]
        void AddInfant()
        {
            if (AirFlightRequestModel.InfoInfantCount >= 0)
            {
                AirFlightRequestModel.InfoInfantCount += 1;
            }
        }
        [RelayCommand]
        void DeletInfant()
        {
            if (AirFlightRequestModel.InfoInfantCount > 0)
            {
                AirFlightRequestModel.InfoInfantCount -= 1;
            }
        }
        [RelayCommand]
        async Task ApplyClicked(RequestTravelAgencyAirFlightRequest Request)
        {
            if (AirFlightSelected == null || AirFlightSelected?.Id == 0)
            {
                var toast = Toast.Make("Please Complete This Field Required : Select Carrier.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (Request.Date.Date < DateTime.Now.Date)
            {
                var toast = Toast.Make("The selected date is less than today's date.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (ClassSelected == null || ClassSelected?.Id == 0)
            {
                var toast = Toast.Make("Please Complete This Field Required : Select Class.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (string.IsNullOrEmpty(Request.AirportFrom))
            {
                var toast = Toast.Make("Please Complete This Field Required : From Location.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (string.IsNullOrEmpty(Request.AirportTo))
            {
                var toast = Toast.Make("Please Complete This Field Required : To Location.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (Request.ETD > Request.ETA)
            {
                var toast = Toast.Make("The expected time of departure must be less than the expected time of arrival.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (Request.InfoAdultCount == 0 & Request.InfoChildCount == 0 && Request.InfoInfantCount == 0 )
            {
                var toast = Toast.Make("Please Complete This Field Required : Passengers Count.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else
            {
                IsBusy = false;
                UserDialogs.Instance.ShowLoading();

                Request.AirFlightId = AirFlightSelected!.Id;
                AirFlightResponseModel.AirLine = AirFlightSelected.AirLine;
                Request.ClassAirFlightId = ClassSelected!.Id;
                AirFlightResponseModel.ClassName = ClassSelected.ClassName;
                AirFlightResponseModel.AirportFrom = Request.AirportFrom;
                AirFlightResponseModel.AirportTo = Request.AirportTo;
                AirFlightResponseModel.Date = Request.Date;
                AirFlightResponseModel.TotalPerson = Request.TotalPerson;
                AirFlightResponseModel.TotalPerson = Request.TotalPerson = Request.InfoChildCount + Request.InfoAdultCount + Request.InfoInfantCount;

                AirFlightClose.Invoke(Request, AirFlightResponseModel);

                await App.Current!.MainPage!.Navigation.PopAsync();

                UserDialogs.Instance.HideHud();
                IsBusy = true;
            }

            
        }

        #endregion
    }
}
