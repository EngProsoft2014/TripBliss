using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Models;
using TripBliss.Pages.DistributorsPages.ResponseDetailes;

namespace TripBliss.ViewModels.DistributorsViewModels.ResponseDetails
{
    partial class RequestDetailsViewModel : BaseViewModel
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

        public RequestDetailsViewModel()
        {
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
            App.Current.MainPage.Navigation.PushAsync(new HotelServicePage());
        }
        [RelayCommand]
        void SelectHotel(HotelServiceModel model)
        {
            var vm = new HotelServiceViewModel(model);
            var page = new HotelServicePage();
            page.BindingContext = vm;
            App.Current.MainPage.Navigation.PushAsync(page);
        }
        #endregion

        #region Transportaiton RelayCommand
        [RelayCommand]
        void AddTransportaion()
        {
            App.Current.MainPage.Navigation.PushAsync(new TransportaionServicePage());
        }
        [RelayCommand]
        void SelectTransportaion(TransportaionServiceModel model)
        {
            var vm = new TransportaionServiceViewModel(model);
            var page = new TransportaionServicePage();
            page.BindingContext = vm;
            App.Current.MainPage.Navigation.PushAsync(page);
        }
        #endregion

        #region Air Flight RelayCommand
        [RelayCommand]
        void AddAirFlight()
        {
            App.Current.MainPage.Navigation.PushAsync(new AirFlightServicePage());
        }
        [RelayCommand]
        void SelectAirFlight(AirFlightModel model)
        {
            var vm = new AirFlightServicesViewModel(model);
            var page = new AirFlightServicePage();
            page.BindingContext = vm;
            App.Current.MainPage.Navigation.PushAsync(page);
        }
        #endregion

        #region Visa RelayCommand
        [RelayCommand]
        void AddVisa()
        {
            App.Current.MainPage.Navigation.PushAsync(new VisaServicePage());
        }
        [RelayCommand]
        void SelectVisa(VisaServiceModel model)
        {
            var vm = new VisaServiceViewModel(model);
            var page = new VisaServicePage();
            page.BindingContext = vm;
            App.Current.MainPage.Navigation.PushAsync(page);
        }
        #endregion

        #region Methodes
        void LoadData()
        {
            var checkinDate = DateOnly.FromDateTime(DateTime.Now.Date);

            // Hotel Data
            Hotels.Add(new HotelServiceModel()
            {
                Checkin = checkinDate,
                Checkout = checkinDate.AddDays(30),
                HotelName = "AL RHAIA HOTEL",
                Meals = "3",
                Notes = "Good Clean",
                RoomNo = 5,
                RoomType = "Vip",
                RoomView = "Nile",
                price = "750"
            });

            Hotels.Add(new HotelServiceModel()
            {
                Checkin = checkinDate.AddDays(1),
                Checkout = checkinDate.AddDays(31),
                HotelName = "LUXURY SUITES",
                Meals = "2",
                Notes = "Excellent Service",
                RoomNo = 10,
                RoomType = "Suite",
                RoomView = "City",
                price = "650"
            });

            Hotels.Add(new HotelServiceModel()
            {
                Checkin = checkinDate.AddDays(2),
                Checkout = checkinDate.AddDays(32),
                HotelName = "BEACH RESORT",
                Meals = "3",
                Notes = "Great Location",
                RoomNo = 15,
                RoomType = "Deluxe",
                RoomView = "Sea",
                price = "550"
            });

            Hotels.Add(new HotelServiceModel()
            {
                Checkin = checkinDate.AddDays(3),
                Checkout = checkinDate.AddDays(33),
                HotelName = "MOUNTAIN INN",
                Meals = "1",
                Notes = "Cozy Atmosphere",
                RoomNo = 20,
                RoomType = "Standard",
                RoomView = "Mountain",
                price = "450"
            });

            Hotels.Add(new HotelServiceModel()
            {
                Checkin = checkinDate.AddDays(4),
                Checkout = checkinDate.AddDays(34),
                HotelName = "CITY CENTRAL HOTEL",
                Meals = "2",
                Notes = "Convenient Location",
                RoomNo = 25,
                RoomType = "Standard",
                RoomView = "City",
                price = "650"
            });

            Hotels.Add(new HotelServiceModel()
            {
                Checkin = checkinDate.AddDays(5),
                Checkout = checkinDate.AddDays(35),
                HotelName = "FOREST RETREAT",
                Meals = "3",
                Notes = "Peaceful Surroundings",
                RoomNo = 30,
                RoomType = "Deluxe",
                RoomView = "Forest",
                price = "750"
            });

            Hotels.Add(new HotelServiceModel()
            {
                Checkin = checkinDate.AddDays(6),
                Checkout = checkinDate.AddDays(36),
                HotelName = "DESERT OASIS",
                Meals = "2",
                Notes = "Unique Experience",
                RoomNo = 35,
                RoomType = "Vip",
                RoomView = "Desert",
                price = "950"
            });

        }
        void LoadTransportaionData()
        {
            var todayDate = DateOnly.FromDateTime(DateTime.Now.Date);
            var currentTime = TimeOnly.FromDateTime(DateTime.Now);

            transportaionServices.Add(new TransportaionServiceModel()
            {
                Brand = "Bmw",
                CarType = "Suv",
                Date = todayDate,
                From = "Cairo",
                Model = "2024",
                Nos = 4,
                time = currentTime,
                To = "Asuut",
                Notes = "Black Color",
                price = "650"
            });

            transportaionServices.Add(new TransportaionServiceModel()
            {
                Brand = "Mercedes",
                CarType = "Sedan",
                Date = todayDate.AddDays(1),
                From = "Alexandria",
                Model = "2023",
                Nos = 3,
                time = currentTime.AddHours(1),
                To = "Cairo",
                Notes = "White Color",
                price = "670"
            });

            transportaionServices.Add(new TransportaionServiceModel()
            {
                Brand = "Audi",
                CarType = "Coupe",
                Date = todayDate.AddDays(2),
                From = "Giza",
                Model = "2022",
                Nos = 2,
                time = currentTime.AddHours(2),
                To = "Luxor",
                Notes = "Red Color",
                price = "690"
            });

            transportaionServices.Add(new TransportaionServiceModel()
            {
                Brand = "Tesla",
                CarType = "Electric",
                Date = todayDate.AddDays(3),
                From = "Hurghada",
                Model = "2024",
                Nos = 4,
                time = currentTime.AddHours(3),
                To = "Sharm El-Sheikh",
                Notes = "Blue Color",
                price = "250"
            });

            transportaionServices.Add(new TransportaionServiceModel()
            {
                Brand = "Toyota",
                CarType = "Van",
                Date = todayDate.AddDays(4),
                From = "Aswan",
                Model = "2021",
                Nos = 8,
                time = currentTime.AddHours(4),
                To = "Cairo",
                Notes = "Green Color",
                price = "680"
            });

            transportaionServices.Add(new TransportaionServiceModel()
            {
                Brand = "Honda",
                CarType = "Hatchback",
                Date = todayDate.AddDays(5),
                From = "Suez",
                Model = "2020",
                Nos = 4,
                time = currentTime.AddHours(5),
                To = "Port Said",
                Notes = "Yellow Color",
                price = "650"
            });

            transportaionServices.Add(new TransportaionServiceModel()
            {
                Brand = "Ford",
                CarType = "Truck",
                Date = todayDate.AddDays(6),
                From = "Ismailia",
                Model = "2023",
                Nos = 6,
                time = currentTime.AddHours(6),
                To = "Cairo",
                Notes = "Silver Color",
                price = "670"
            });

        }
        void LoadAirFlightData()
        {
            var flightDate = DateOnly.FromDateTime(DateTime.Now);

            airFlights.Add(new AirFlightModel()
            {
                Class = "B",
                Infant = 1,
                Adult = 3,
                Date = flightDate.AddDays(1),
                Carrier = "Delta",
                Child = 2,
                ETA = "12:00 PM",
                ETD = "03:00 PM",
                From = "Canada",
                To = "Egypt",
                Notes = "Comfortable Seats",
                price = "670"
            });

            airFlights.Add(new AirFlightModel()
            {
                Class = "C",
                Infant = 0,
                Adult = 1,
                Date = flightDate.AddDays(2),
                Carrier = "Emirates",
                Child = 1,
                ETA = "10:00 AM",
                ETD = "01:00 PM",
                From = "UK",
                To = "Egypt",
                Notes = "Excellent In-Flight Entertainment",
                price = "650"
            });

            airFlights.Add(new AirFlightModel()
            {
                Class = "D",
                Infant = 1,
                Adult = 4,
                Date = flightDate.AddDays(3),
                Carrier = "Qatar Airways",
                Child = 2,
                ETA = "08:00 AM",
                ETD = "11:00 AM",
                From = "Australia",
                To = "Egypt",
                Notes = "Friendly Staff",
                price = "350"
            });

            airFlights.Add(new AirFlightModel()
            {
                Class = "E",
                Infant = 2,
                Adult = 2,
                Date = flightDate.AddDays(4),
                Carrier = "Singapore Airlines",
                Child = 1,
                ETA = "09:00 AM",
                ETD = "12:00 PM",
                From = "Germany",
                To = "Egypt",
                Notes = "Spacious Legroom",
                price = "950"
            });

            airFlights.Add(new AirFlightModel()
            {
                Class = "F",
                Infant = 3,
                Adult = 1,
                Date = flightDate.AddDays(5),
                Carrier = "Turkish Airlines",
                Child = 0,
                ETA = "11:00 AM",
                ETD = "02:00 PM",
                From = "France",
                To = "Egypt",
                Notes = "Great Food",
                price = "610"
            });

            airFlights.Add(new AirFlightModel()
            {
                Class = "G",
                Infant = 1,
                Adult = 3,
                Date = flightDate.AddDays(6),
                Carrier = "British Airways",
                Child = 2,
                ETA = "07:00 AM",
                ETD = "10:00 AM",
                From = "Italy",
                To = "Egypt",
                Notes = "Smooth Flight",
                price = "690"
            });

        }
        void LoadVisaData()
        {

            visaServices.Add(new VisaServiceModel()
            {
                VisaType = "Tourist",
                VisaNo = "048",
                Notes = "Single Entry",
                Price = "250"
            });

            visaServices.Add(new VisaServiceModel()
            {
                VisaType = "Student",
                VisaNo = "052",
                Notes = "University Enrollment",
                Price = "290"
            });

            visaServices.Add(new VisaServiceModel()
            {
                VisaType = "Business",
                VisaNo = "061",
                Notes = "Multiple Entry",
                Price = "150"
            });

            visaServices.Add(new VisaServiceModel()
            {
                VisaType = "Transit",
                VisaNo = "072",
                Notes = "24-hour Validity",
                Price = "250"
            });

            visaServices.Add(new VisaServiceModel()
            {
                VisaType = "Medical",
                VisaNo = "083",
                Notes = "Hospital Treatment",
                Price = "250"
            });

            visaServices.Add(new VisaServiceModel()
            {
                VisaType = "Diplomatic",
                VisaNo = "091",
                Notes = "Government Official",
                Price = "350"
            });

        }
        #endregion
    }
}
