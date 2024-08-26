using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Constants;
using TripBliss.Helpers;
using TripBliss.Models;


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
            GetAirFlights();
            GetClasses();
        }
        public Tr_C_AirFlightServicesViewModel(RequestTravelAgencyAirFlightResponse model, IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            AirFlightResponseModel = model;
            _service = service;
            AirFlightRequestModel!.Date = DateTime.Now;
            GetAirFlights();
            GetClasses();
        }
        #endregion

        #region Methods
        async void GetAirFlights()
        {
            IsBusy = true;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();

                var json = await Rep.GetAsync<ObservableCollection<AirFlightResponse>>(ApiConstants.GetAllAirFlightApi, UserToken);

                if (json != null)
                {
                    AirFlights = json;
                }
            }

            IsBusy = false;
        }

        async void GetClasses()
        {
            IsBusy = true;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();

                var json = await Rep.GetAsync<ObservableCollection<ClassAirFlightResponse>>(ApiConstants.GetAllClassesAirFlightApi, UserToken);

                if (json != null)
                {
                    Classes = json;
                }
            }

            IsBusy = false;
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
        void AplyClicked(RequestTravelAgencyAirFlightRequest Request)
        {

            Request.AirFlightId = AirFlightSelected.Id;
            AirFlightResponseModel.AirLine = AirFlightSelected.AirLine;
            Request.ClassAirFlightId = ClassSelected.Id;
            AirFlightResponseModel.ClassName = ClassSelected.ClassName;
            AirFlightResponseModel.AirportFrom = Request.AirportFrom;
            AirFlightResponseModel.AirportTo = Request.AirportTo;
            AirFlightResponseModel.Date = Request.Date;
            AirFlightResponseModel.TotalPerson = Request.TotalPerson;
            AirFlightResponseModel.TotalPerson = Request.TotalPerson = Request.InfoChildCount + Request.InfoAdultCount + Request.InfoInfantCount;

            AirFlightClose.Invoke(Request, AirFlightResponseModel);

            App.Current!.MainPage!.Navigation.PopAsync();
        }

        #endregion
    }
}
