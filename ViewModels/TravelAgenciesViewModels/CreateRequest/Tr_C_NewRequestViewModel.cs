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
using TripBliss.Pages.TravelAgenciesPages.CreateRequest;
using TripBliss.Helpers;
using TripBliss.Constants;

namespace TripBliss.ViewModels.TravelAgenciesViewModels.CreateRequest
{
    public partial class Tr_C_NewRequestViewModel : BaseViewModel
    {


        #region prop
        public ObservableCollection<HotelServiceModel> Hotels { get; set; }
        public ObservableCollection<TransportaionServiceModel> transportaionServices { get; set; }
        public ObservableCollection<AirFlightModel> airFlights { get; set; }
        public ObservableCollection<VisaServiceModel> visaServices { get; set; }

        [ObservableProperty]
        HotelServiceModel selectedHotel;
        [ObservableProperty]
        TransportaionServiceModel selectedTransportaition;
        [ObservableProperty]
        AirFlightModel selectedAirFlight;
        [ObservableProperty]
        VisaServiceModel selectedVisa;

        #endregion

        readonly Services.Data.ServicesService _service;
        readonly IGenericRepository Rep;
        public Tr_C_NewRequestViewModel(IGenericRepository GenericRep , Services.Data.ServicesService service)
        {
            Rep = GenericRep;
            _service = service;
            Hotels = new ObservableCollection<HotelServiceModel>();
            transportaionServices = new ObservableCollection<TransportaionServiceModel>();
            airFlights = new ObservableCollection<AirFlightModel>();
            visaServices = new ObservableCollection<VisaServiceModel>();
            Lang = Preferences.Default.Get("Lan", "en");
            LoadData();
            LoadTransportaionData();
            LoadAirFlightData();
            LoadVisaData();
        }
        public Tr_C_NewRequestViewModel(DistributorsModel model , IGenericRepository GenericRep)
        {
            Rep = GenericRep;
            Hotels = new ObservableCollection<HotelServiceModel>();
            transportaionServices = new ObservableCollection<TransportaionServiceModel>();
            airFlights = new ObservableCollection<AirFlightModel>();
            visaServices = new ObservableCollection<VisaServiceModel>();
            Lang = Preferences.Default.Get("Lan", "en");
            LoadData();
            LoadTransportaionData();
            LoadAirFlightData();
            LoadVisaData();
        }

        #region Generl RelayCommand
        [RelayCommand]
        void BackButtonClicked()
        {
            App.Current.MainPage.Navigation.PopAsync();
        }
        #endregion

        #region Hotel RelayCommand
        [RelayCommand]
        void AddHotel()
        {
            App.Current.MainPage.Navigation.PushAsync(new HotelServicePage(new Tr_C_HotelServiceViewModel(Rep,_service) , Rep));
        }
        [RelayCommand]
        void SelectHotel(HotelServiceModel model)
        {
            var vm = new Tr_C_HotelServiceViewModel(model,Rep,_service);
            var page = new HotelServicePage(vm,Rep);
            page.BindingContext = vm;
            App.Current.MainPage.Navigation.PushAsync(page);
        }
        #endregion

        #region Transportaiton RelayCommand
        [RelayCommand]
        void AddTransportaion()
        {
            App.Current.MainPage.Navigation.PushAsync(new TransportaionServicePage(new Tr_C_TransportaionServiceViewModel(Rep, _service),Rep));
        }
        [RelayCommand]
        void SelectTransportaion(TransportaionServiceModel model)
        {
            var vm = new Tr_C_TransportaionServiceViewModel(model,Rep, _service);
            var page = new TransportaionServicePage(vm,Rep);
            page.BindingContext = vm;
            App.Current.MainPage.Navigation.PushAsync(page);
        }
        #endregion

        #region Air Flight RelayCommand
        [RelayCommand]
        void AddAirFlight()
        {
            App.Current.MainPage.Navigation.PushAsync(new AirFlightServicePage(new Tr_C_AirFlightServicesViewModel(Rep, _service)));
        }
        [RelayCommand]
        void SelectAirFlight(AirFlightModel model)
        {
            var vm = new Tr_C_AirFlightServicesViewModel(model, Rep, _service);
            var page = new AirFlightServicePage(new Tr_C_AirFlightServicesViewModel(model, Rep, _service));
            page.BindingContext = vm;
            App.Current!.MainPage!.Navigation.PushAsync(page);
        }
        #endregion

        #region Visa RelayCommand
        [RelayCommand]
        void AddVisa()
        {
            App.Current.MainPage.Navigation.PushAsync(new VisaServicePage(new Tr_C_VisaServiceViewModel(Rep, _service),Rep));
        }
        [RelayCommand]
        void SelectVisa(VisaServiceModel model)
        {
            var vm = new Tr_C_VisaServiceViewModel(model,Rep, _service);
            var page = new VisaServicePage(vm,Rep);
            page.BindingContext = vm;
            App.Current.MainPage.Navigation.PushAsync(page);
        }
        #endregion

        #region Methodes
        async Task AddToFavouiter(string DistributorId)
        {
            IsBusy = true;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string id = Preferences.Default.Get(ApiConstants.userid, "");
                string UserToken = await _service.UserToken();

                var json = await Rep.PostTRAsync<string, DistributorCompanyResponse>(ApiConstants.AddfavouritesApi + $"{id}/TravelAgencywithDistributors", DistributorId, UserToken);

                if (json.Item1 != null)
                {
                    
                }
            }

            IsBusy = false;
        }
        #endregion


    }
}
