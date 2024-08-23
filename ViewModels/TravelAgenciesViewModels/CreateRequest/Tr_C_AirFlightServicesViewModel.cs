﻿using CommunityToolkit.Mvvm.ComponentModel;
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
            GetAirFlights();
            GetClasses();
        }
        public Tr_C_AirFlightServicesViewModel(RequestTravelAgencyAirFlightResponse model, IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            AirFlightResponseModel = model;
            _service = service;
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
            App.Current.MainPage.Navigation.PopAsync();
        }

        [RelayCommand]
        void AddAdult()
        {
            Adult += 1;
        }
        [RelayCommand]
        void DeletAdult()
        {
            Adult -= 1;
        }

        [RelayCommand]
        void AddChild()
        {
            Chiled += 1;
        }
        [RelayCommand]
        void DeletChild()
        {
            Chiled -= 1;
        }
        [RelayCommand]
        void AddInfant()
        {
            Infant += 1;
        }
        [RelayCommand]
        void DeletInfant()
        {
            Infant -= 1;
        }
        [RelayCommand]
        void AplyClicked(RequestTravelAgencyAirFlightRequest Request)
        {

            AirFlightResponseModel.AirFlightId = AirFlightSelected.Id;
            AirFlightResponseModel.AirLine = AirFlightSelected.AirLine;
            AirFlightResponseModel.ClassAirFlightId = ClassSelected.Id;
            AirFlightResponseModel.ClassName = ClassSelected.ClassName;
            AirFlightResponseModel.AirportFrom = Request.AirportFrom;
            AirFlightResponseModel.AirportTo = Request.AirportTo;
            AirFlightResponseModel.Date = Request.Date;
            AirFlightResponseModel.TotalPerson = Request.TotalPerson;
            AirFlightResponseModel.RequestTravelAgencyId = Request.RequestTravelAgencyId;

            AirFlightClose.Invoke(Request, AirFlightResponseModel);

            App.Current!.MainPage!.Navigation.PopAsync();
        }

        #endregion
    }
}
