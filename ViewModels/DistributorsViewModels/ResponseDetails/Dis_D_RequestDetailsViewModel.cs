using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var vm = new Dis_D_HotelServiceViewModel(model, Rep);
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
            var vm = new Dis_D_TransportaionServiceViewModel(model, Rep);
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
            var vm = new Dis_D_AirFlightServicesViewModel(model, Rep);
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
            var vm = new Dis_D_VisaServiceViewModel(model, Rep);
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
        //        RoomView = "Nile",
        //        price = "750"
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
        //        RoomView = "City",
        //        price = "650"
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
        //        RoomView = "Sea",
        //        price = "550"
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
        //        RoomView = "Mountain",
        //        price = "450"
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
        //        RoomView = "City",
        //        price = "650"
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
        //        RoomView = "Forest",
        //        price = "750"
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
        //        RoomView = "Desert",
        //        price = "950"
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
        //        Notes = "Black Color",
        //        price = "650"
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
        //        Notes = "White Color",
        //        price = "670"
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
        //        Notes = "Red Color",
        //        price = "690"
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
        //        Notes = "Blue Color",
        //        price = "250"
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
        //        Notes = "Green Color",
        //        price = "680"
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
        //        Notes = "Yellow Color",
        //        price = "650"
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
        //        Notes = "Silver Color",
        //        price = "670"
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
        //        Notes = "Comfortable Seats",
        //        price = "670"
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
        //        Notes = "Excellent In-Flight Entertainment",
        //        price = "650"
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
        //        Notes = "Friendly Staff",
        //        price = "350"
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
        //        Notes = "Spacious Legroom",
        //        price = "950"
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
        //        Notes = "Great Food",
        //        price = "610"
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
        //        Notes = "Smooth Flight",
        //        price = "690"
        //    });

        //}
        //void LoadVisaData()
        //{

        //    visaServices.Add(new VisaServiceModel()
        //    {
        //        VisaType = "Tourist",
        //        VisaNo = "048",
        //        Notes = "Single Entry",
        //        Price = "250"
        //    });

        //    visaServices.Add(new VisaServiceModel()
        //    {
        //        VisaType = "Student",
        //        VisaNo = "052",
        //        Notes = "University Enrollment",
        //        Price = "290"
        //    });

        //    visaServices.Add(new VisaServiceModel()
        //    {
        //        VisaType = "Business",
        //        VisaNo = "061",
        //        Notes = "Multiple Entry",
        //        Price = "150"
        //    });

        //    visaServices.Add(new VisaServiceModel()
        //    {
        //        VisaType = "Transit",
        //        VisaNo = "072",
        //        Notes = "24-hour Validity",
        //        Price = "250"
        //    });

        //    visaServices.Add(new VisaServiceModel()
        //    {
        //        VisaType = "Medical",
        //        VisaNo = "083",
        //        Notes = "Hospital Treatment",
        //        Price = "250"
        //    });

        //    visaServices.Add(new VisaServiceModel()
        //    {
        //        VisaType = "Diplomatic",
        //        VisaNo = "091",
        //        Notes = "Government Official",
        //        Price = "350"
        //    });

        //}
        #endregion

        #region RelayCommand
        [RelayCommand]
        [Obsolete]
        async Task AddToRequest()
        {
            bool answer = await App.Current!.MainPage!.DisplayAlert("Question?", "Are You Accept This Price?", "Yes", "No");
            Response.TotalPriceDisAccept = answer ? 1 : 0;
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
