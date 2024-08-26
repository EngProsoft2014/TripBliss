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
using TripBliss.Models.RequestTravelAgency;
using CommunityToolkit.Maui.Alerts;
using TripBliss.Pages.TravelAgenciesPages;
using Controls.UserDialogs.Maui;
using Syncfusion.Maui.Data;



namespace TripBliss.ViewModels.TravelAgenciesViewModels.CreateRequest
{
    public partial class Tr_C_NewRequestViewModel : BaseViewModel
    {


        #region prop

        [ObservableProperty]
        RequestTravelAgencyRequest requestTravelAgency= new RequestTravelAgencyRequest();
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
        public Tr_C_NewRequestViewModel(IGenericRepository GenericRep , Services.Data.ServicesService service)
        {
            Rep = GenericRep;
            _service = service;
            Lang = Preferences.Default.Get("Lan", "en");
            //LoadData();
            //LoadTransportaionData();
            //LoadAirFlightData();
            //LoadVisaData();
        }
        public Tr_C_NewRequestViewModel(ObservableCollection<DistributorCompanyResponse> distributors , IGenericRepository GenericRep , Services.Data.ServicesService service)
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
            vm.HotelClose += (HoteltRequest, HotelResponse) =>
            {
                UserDialogs.Instance.ShowLoading();

                LstTravelAgencyHotelRequest.Add(HoteltRequest);
                LstTravelAgencyHotelResponse.Add(HotelResponse);

                UserDialogs.Instance.HideHud();
            };
            var page = new HotelServicePage(vm, Rep);
            
            App.Current!.MainPage!.Navigation.PushAsync(page);
        }
        [RelayCommand]
        void SelectHotel(RequestTravelAgencyHotelResponse model)
        {
            var vm = new Tr_C_HotelServiceViewModel(model,Rep,_service);
            vm.HotelClose += (HoteltRequest, HotelResponse) =>
            {
                UserDialogs.Instance.ShowLoading();

                LstTravelAgencyHotelRequest.Add(HoteltRequest);
                LstTravelAgencyHotelResponse.Add(HotelResponse);

                UserDialogs.Instance.HideHud();
            };
            var page = new HotelServicePage(vm,Rep);
            page.BindingContext = vm;
            App.Current!.MainPage!.Navigation.PushAsync(page);
        }

        [RelayCommand]
        void DeletHotel(RequestTravelAgencyHotelResponse model)
        {
            LstTravelAgencyHotelResponse.Remove(model);
        }
        #endregion

        #region Transportaiton RelayCommand
        [RelayCommand]
        void AddTransportaion()
        {
            var vm = new Tr_C_TransportaionServiceViewModel(Rep, _service);
            vm.TransportClose += (TransportRequest, TransportResponse) =>
            {
                UserDialogs.Instance.ShowLoading();

                LstTravelAgencyTransportRequest.Add(TransportRequest);
                LstTravelAgencyTransportResponse.Add(TransportResponse);

                UserDialogs.Instance.HideHud();
            };
            App.Current!.MainPage!.Navigation.PushAsync(new TransportaionServicePage(vm,Rep));
        }
        [RelayCommand]
        void SelectTransportaion(RequestTravelAgencyTransportResponse model)
        {
            var vm = new Tr_C_TransportaionServiceViewModel(model,Rep, _service);
            vm.TransportClose += (TransportRequest, TransportResponse) =>
            {
                UserDialogs.Instance.ShowLoading();

                LstTravelAgencyTransportRequest.Add(TransportRequest);
                LstTravelAgencyTransportResponse.Add(TransportResponse);

                UserDialogs.Instance.HideHud();
            };
            var page = new TransportaionServicePage(vm,Rep);
            App.Current!.MainPage!.Navigation.PushAsync(page);
        }
        [RelayCommand]
        void DeletTransPortation(RequestTravelAgencyTransportResponse model)
        {
            LstTravelAgencyTransportResponse.Remove(model);
        }

        #endregion

        #region Air Flight RelayCommand
        [RelayCommand]
        void AddAirFlight()
        {
            var pageView = new Tr_C_AirFlightServicesViewModel(Rep, _service);
            pageView.AirFlightClose += (AirFlightRequest, AirFlightResponse) =>
            {
                UserDialogs.Instance.ShowLoading();

                LstTravelAgencyAirFlightResponse.Add(AirFlightResponse);
                LstTravelAgencyAirFlightRequest.Add(AirFlightRequest);
                
                UserDialogs.Instance.HideHud();
            };

            App.Current!.MainPage!.Navigation.PushAsync(new AirFlightServicePage(pageView));
        }
        [RelayCommand]
        void SelectAirFlight(RequestTravelAgencyAirFlightResponse Response)
        {
            var vm = new Tr_C_AirFlightServicesViewModel(Response, Rep, _service);
            vm.AirFlightClose += (AirFlightRequest, AirFlightResponse) =>
            {
                UserDialogs.Instance.ShowLoading();

                //AirFlightRequest.Id = AirFlightResponse.Id;
                LstTravelAgencyAirFlightResponse.Add(AirFlightResponse);
                LstTravelAgencyAirFlightRequest.Add(AirFlightRequest);

                UserDialogs.Instance.HideHud();
            };
            var page = new AirFlightServicePage(vm);
            App.Current!.MainPage!.Navigation.PushAsync(page);
        }
        [RelayCommand]
        void DeletAirFlight(RequestTravelAgencyAirFlightResponse model)
        {
            LstTravelAgencyAirFlightResponse.Remove(model);

            //LstTravelAgencyAirFlightRequest.Remove(LstTravelAgencyAirFlightRequest.Where(s => s.Id == model.Id).FirstOrDefault()!);
        }
        #endregion

        #region Visa RelayCommand
        [RelayCommand]
        void AddVisa()
        {
            var vm = new Tr_C_VisaServiceViewModel( Rep, _service);
            vm.VisaClose += (VisaRequest, VisaResponse) =>
            {
                UserDialogs.Instance.ShowLoading();

                LstTravelAgencyVisaRequest.Add(VisaRequest);
                LstTravelAgencyVisaResponse.Add(VisaResponse);

                UserDialogs.Instance.HideHud();
            };

            App.Current!.MainPage!.Navigation.PushAsync(new VisaServicePage(vm, Rep));
        }
        [RelayCommand]
        void SelectVisa(RequestTravelAgencyVisaResponse model)
        {
            var vm = new Tr_C_VisaServiceViewModel(model,Rep, _service);
            vm.VisaClose += (VisaRequest, VisaResponse) =>
            {
                UserDialogs.Instance.ShowLoading();

                LstTravelAgencyVisaRequest.Add(VisaRequest);
                LstTravelAgencyVisaResponse.Add(VisaResponse);

                UserDialogs.Instance.HideHud();
            };
            var page = new VisaServicePage(vm,Rep);
            App.Current!.MainPage!.Navigation.PushAsync(page);
        }
        [RelayCommand]
        void DeletVisa(RequestTravelAgencyVisaResponse model)
        {
            LstTravelAgencyVisaResponse.Remove(model);
        }
        #endregion

        #region AddRequest RelayCommand
        [RelayCommand]
        async Task AddToRequest()
        {
            if (LstTravelAgencyHotelRequest.Count ==0 & LstTravelAgencyTransportRequest.Count == 0 & LstTravelAgencyAirFlightRequest.Count == 0 & LstTravelAgencyVisaRequest.Count == 0)
            {
                var toast = Toast.Make("Please add at least one service.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else
            {
                RequestTravelAgency = new RequestTravelAgencyRequest();

                RequestTravelAgency.RequestTravelAgencyHotel = LstTravelAgencyHotelRequest.ToList();
                RequestTravelAgency.RequestTravelAgencyTransport = LstTravelAgencyTransportRequest.ToList();
                RequestTravelAgency.RequestTravelAgencyAirFlight = LstTravelAgencyAirFlightRequest.ToList();
                RequestTravelAgency.RequestTravelAgencyVisa = LstTravelAgencyVisaRequest.ToList();

                DistributorCompanies.ForEach(s => DistributorRequests.Add(new ResponseWithDistributorRequest { DistributorCompanyId = s.Id }));
                RequestTravelAgency.ResponseWithDistributor = DistributorRequests.ToList();

                IsBusy = false;

                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {

                    string UserToken = await _service.UserToken();

                    string id = Preferences.Default.Get(ApiConstants.travelAgencyCompanyId, "");
                    var json = await Rep.PostTRAsync<RequestTravelAgencyRequest, RequestTravelAgencyResponse>(ApiConstants.AddRequestApi + $"{id}/RequestTravelAgency", RequestTravelAgency, UserToken);

                    if (json.Item1 != null)
                    {
                        var toast = Toast.Make("Successfully for Add Request", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                        await toast.Show();

                        await App.Current!.MainPage!.Navigation.PushAsync(new HomeAgencyPage(new Tr_HomeViewModel(Rep, _service), Rep, _service));
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


        #endregion



        #region Methodes static
        //void LoadData()
        //{
        //    var checkinDate = DateOnly.FromDateTime(DateTime.Now.Date);

        //    // Hotel Data
        //    Hotels.Add(new HotelServiceModel()
        //    {
        //        Checkin = checkinDate,
        //        Checkout = checkinDate.AddDays(30),
        //        HotelName = "AL RHAIA HOTEL",
        //        Meals = "3",
        //        Notes = "Good Clean",
        //        RoomNo = 5,
        //        RoomType = "Vip",
        //        RoomView = "Nile"
        //    });

        //    Hotels.Add(new HotelServiceModel()
        //    {
        //        Checkin = checkinDate.AddDays(1),
        //        Checkout = checkinDate.AddDays(31),
        //        HotelName = "LUXURY SUITES",
        //        Meals = "2",
        //        Notes = "Excellent Service",
        //        RoomNo = 10,
        //        RoomType = "Suite",
        //        RoomView = "City"
        //    });

        //    Hotels.Add(new HotelServiceModel()
        //    {
        //        Checkin = checkinDate.AddDays(2),
        //        Checkout = checkinDate.AddDays(32),
        //        HotelName = "BEACH RESORT",
        //        Meals = "3",
        //        Notes = "Great Location",
        //        RoomNo = 15,
        //        RoomType = "Deluxe",
        //        RoomView = "Sea"
        //    });

        //    Hotels.Add(new HotelServiceModel()
        //    {
        //        Checkin = checkinDate.AddDays(3),
        //        Checkout = checkinDate.AddDays(33),
        //        HotelName = "MOUNTAIN INN",
        //        Meals = "1",
        //        Notes = "Cozy Atmosphere",
        //        RoomNo = 20,
        //        RoomType = "Standard",
        //        RoomView = "Mountain"
        //    });

        //    Hotels.Add(new HotelServiceModel()
        //    {
        //        Checkin = checkinDate.AddDays(4),
        //        Checkout = checkinDate.AddDays(34),
        //        HotelName = "CITY CENTRAL HOTEL",
        //        Meals = "2",
        //        Notes = "Convenient Location",
        //        RoomNo = 25,
        //        RoomType = "Standard",
        //        RoomView = "City"
        //    });

        //    Hotels.Add(new HotelServiceModel()
        //    {
        //        Checkin = checkinDate.AddDays(5),
        //        Checkout = checkinDate.AddDays(35),
        //        HotelName = "FOREST RETREAT",
        //        Meals = "3",
        //        Notes = "Peaceful Surroundings",
        //        RoomNo = 30,
        //        RoomType = "Deluxe",
        //        RoomView = "Forest"
        //    });

        //    Hotels.Add(new HotelServiceModel()
        //    {
        //        Checkin = checkinDate.AddDays(6),
        //        Checkout = checkinDate.AddDays(36),
        //        HotelName = "DESERT OASIS",
        //        Meals = "2",
        //        Notes = "Unique Experience",
        //        RoomNo = 35,
        //        RoomType = "Vip",
        //        RoomView = "Desert"
        //    });

        //}
        //void LoadTransportaionData()
        //{
        //    var todayDate = DateOnly.FromDateTime(DateTime.Now.Date);
        //    var currentTime = TimeOnly.FromDateTime(DateTime.Now);

        //    transportaionServices.Add(new TransportaionServiceModel()
        //    {
        //        Brand = "Bmw",
        //        CarType = "Suv",
        //        Date = todayDate,
        //        From = "Cairo",
        //        Model = "2024",
        //        Nos = 4,
        //        time = currentTime,
        //        To = "Asuut",
        //        Notes = "Black Color"
        //    });

        //    transportaionServices.Add(new TransportaionServiceModel()
        //    {
        //        Brand = "Mercedes",
        //        CarType = "Sedan",
        //        Date = todayDate.AddDays(1),
        //        From = "Alexandria",
        //        Model = "2023",
        //        Nos = 3,
        //        time = currentTime.AddHours(1),
        //        To = "Cairo",
        //        Notes = "White Color"
        //    });

        //    transportaionServices.Add(new TransportaionServiceModel()
        //    {
        //        Brand = "Audi",
        //        CarType = "Coupe",
        //        Date = todayDate.AddDays(2),
        //        From = "Giza",
        //        Model = "2022",
        //        Nos = 2,
        //        time = currentTime.AddHours(2),
        //        To = "Luxor",
        //        Notes = "Red Color"
        //    });

        //    transportaionServices.Add(new TransportaionServiceModel()
        //    {
        //        Brand = "Tesla",
        //        CarType = "Electric",
        //        Date = todayDate.AddDays(3),
        //        From = "Hurghada",
        //        Model = "2024",
        //        Nos = 4,
        //        time = currentTime.AddHours(3),
        //        To = "Sharm El-Sheikh",
        //        Notes = "Blue Color"
        //    });

        //    transportaionServices.Add(new TransportaionServiceModel()
        //    {
        //        Brand = "Toyota",
        //        CarType = "Van",
        //        Date = todayDate.AddDays(4),
        //        From = "Aswan",
        //        Model = "2021",
        //        Nos = 8,
        //        time = currentTime.AddHours(4),
        //        To = "Cairo",
        //        Notes = "Green Color"
        //    });

        //    transportaionServices.Add(new TransportaionServiceModel()
        //    {
        //        Brand = "Honda",
        //        CarType = "Hatchback",
        //        Date = todayDate.AddDays(5),
        //        From = "Suez",
        //        Model = "2020",
        //        Nos = 4,
        //        time = currentTime.AddHours(5),
        //        To = "Port Said",
        //        Notes = "Yellow Color"
        //    });

        //    transportaionServices.Add(new TransportaionServiceModel()
        //    {
        //        Brand = "Ford",
        //        CarType = "Truck",
        //        Date = todayDate.AddDays(6),
        //        From = "Ismailia",
        //        Model = "2023",
        //        Nos = 6,
        //        time = currentTime.AddHours(6),
        //        To = "Cairo",
        //        Notes = "Silver Color"
        //    });

        //}
        //void LoadAirFlightData()
        //{
        //    var flightDate = DateOnly.FromDateTime(DateTime.Now);

        //    airFlights.Add(new AirFlightModel()
        //    {
        //        Class = "B",
        //        Infant = 1,
        //        Adult = 3,
        //        Date = flightDate.AddDays(1),
        //        Carrier = "Delta",
        //        Child = 2,
        //        ETA = "12:00 PM",
        //        ETD = "03:00 PM",
        //        From = "Canada",
        //        To = "Egypt",
        //        Notes = "Comfortable Seats"
        //    });

        //    airFlights.Add(new AirFlightModel()
        //    {
        //        Class = "C",
        //        Infant = 0,
        //        Adult = 1,
        //        Date = flightDate.AddDays(2),
        //        Carrier = "Emirates",
        //        Child = 1,
        //        ETA = "10:00 AM",
        //        ETD = "01:00 PM",
        //        From = "UK",
        //        To = "Egypt",
        //        Notes = "Excellent In-Flight Entertainment"
        //    });

        //    airFlights.Add(new AirFlightModel()
        //    {
        //        Class = "D",
        //        Infant = 1,
        //        Adult = 4,
        //        Date = flightDate.AddDays(3),
        //        Carrier = "Qatar Airways",
        //        Child = 2,
        //        ETA = "08:00 AM",
        //        ETD = "11:00 AM",
        //        From = "Australia",
        //        To = "Egypt",
        //        Notes = "Friendly Staff"
        //    });

        //    airFlights.Add(new AirFlightModel()
        //    {
        //        Class = "E",
        //        Infant = 2,
        //        Adult = 2,
        //        Date = flightDate.AddDays(4),
        //        Carrier = "Singapore Airlines",
        //        Child = 1,
        //        ETA = "09:00 AM",
        //        ETD = "12:00 PM",
        //        From = "Germany",
        //        To = "Egypt",
        //        Notes = "Spacious Legroom"
        //    });

        //    airFlights.Add(new AirFlightModel()
        //    {
        //        Class = "F",
        //        Infant = 3,
        //        Adult = 1,
        //        Date = flightDate.AddDays(5),
        //        Carrier = "Turkish Airlines",
        //        Child = 0,
        //        ETA = "11:00 AM",
        //        ETD = "02:00 PM",
        //        From = "France",
        //        To = "Egypt",
        //        Notes = "Great Food"
        //    });

        //    airFlights.Add(new AirFlightModel()
        //    {
        //        Class = "G",
        //        Infant = 1,
        //        Adult = 3,
        //        Date = flightDate.AddDays(6),
        //        Carrier = "British Airways",
        //        Child = 2,
        //        ETA = "07:00 AM",
        //        ETD = "10:00 AM",
        //        From = "Italy",
        //        To = "Egypt",
        //        Notes = "Smooth Flight"
        //    });

        //}
        //void LoadVisaData()
        //{

        //    visaServices.Add(new VisaServiceModel()
        //    {
        //        VisaType = "Tourist",
        //        VisaNo = "048",
        //        Notes = "Single Entry"
        //    });

        //    visaServices.Add(new VisaServiceModel()
        //    {
        //        VisaType = "Student",
        //        VisaNo = "052",
        //        Notes = "University Enrollment"
        //    });

        //    visaServices.Add(new VisaServiceModel()
        //    {
        //        VisaType = "Business",
        //        VisaNo = "061",
        //        Notes = "Multiple Entry"
        //    });

        //    visaServices.Add(new VisaServiceModel()
        //    {
        //        VisaType = "Transit",
        //        VisaNo = "072",
        //        Notes = "24-hour Validity"
        //    });

        //    visaServices.Add(new VisaServiceModel()
        //    {
        //        VisaType = "Medical",
        //        VisaNo = "083",
        //        Notes = "Hospital Treatment"
        //    });

        //    visaServices.Add(new VisaServiceModel()
        //    {
        //        VisaType = "Diplomatic",
        //        VisaNo = "091",
        //        Notes = "Government Official"
        //    });

        //}
        #endregion
    }
}
