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
using TripBliss.Pages.TravelAgenciesPages.RequestDeatiels;
using TripBliss.ViewModels.DistributorsViewModel.CreateResponse;

namespace TripBliss.ViewModels.TravelAgenciesVieModels.RequestDeatiels
{
    partial class NewRequestViewModdel : BaseViewModel
    {


        #region prop
        public ObservableCollection<HotelService> Hotels { get; set; }
        public ObservableCollection<TransportaionServiceModdel> transportaionServices { get; set; }
        public ObservableCollection<AirFlightModdel> airFlights { get; set; }
        public ObservableCollection<VisaServiceModdel> visaServices { get; set; }

        [ObservableProperty]
        HotelService selectedHotel;
        [ObservableProperty]
        TransportaionServiceModdel selectedTransportaition;
        [ObservableProperty]
        AirFlightModdel selectedAirFlight;
        [ObservableProperty]
        VisaServiceModdel selectedVisa;

        #endregion

        public NewRequestViewModdel()
        {
            Hotels = new ObservableCollection<HotelService>();
            transportaionServices = new ObservableCollection<TransportaionServiceModdel>();
            airFlights = new ObservableCollection<AirFlightModdel>();
            visaServices = new ObservableCollection<VisaServiceModdel>();
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
        void SelectHotel()
        {
            var vm = new HotelServiceViewModdel(SelectedHotel);
            var page = new HotelServicesPage();
            page.BindingContext = vm;
            App.Current.MainPage.Navigation.PushAsync(page);
        } 
        #endregion

        #region Transportaiton RelayCommand
        [RelayCommand]
        void SelectTransportaion()
        {
            var vm = new TransportaionServiceViewModdel(SelectedTransportaition);
            var page = new TransportaionServicePage();
            page.BindingContext = vm;
            App.Current.MainPage.Navigation.PushAsync(page);
        } 
        #endregion

        #region Air Flight RelayCommand
        [RelayCommand]
        void SelectAirFlight()
        {
            var vm = new AirFlightServicesViewModdel(SelectedAirFlight);
            var page = new AirFlightServicePage();
            page.BindingContext = vm;
            App.Current.MainPage.Navigation.PushAsync(page);
        } 
        #endregion

        #region Visa RelayCommand
        [RelayCommand]
        void SelectVisa()
        {
            var vm = new VisaServiceViewModdel(SelectedVisa);
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
            Hotels.Add(new HotelService()
            {
                Checkin = checkinDate,
                Checkout = checkinDate.AddDays(30),
                HotelName = "AL RHAIA HOTEL",
                Meals = "3",
                Notes = "Good Clean",
                RoomNo = 5,
                RoomType = "Vip",
                RoomView = "Nile"
            });

            Hotels.Add(new HotelService()
            {
                Checkin = checkinDate.AddDays(1),
                Checkout = checkinDate.AddDays(31),
                HotelName = "LUXURY SUITES",
                Meals = "2",
                Notes = "Excellent Service",
                RoomNo = 10,
                RoomType = "Suite",
                RoomView = "City"
            });

            Hotels.Add(new HotelService()
            {
                Checkin = checkinDate.AddDays(2),
                Checkout = checkinDate.AddDays(32),
                HotelName = "BEACH RESORT",
                Meals = "3",
                Notes = "Great Location",
                RoomNo = 15,
                RoomType = "Deluxe",
                RoomView = "Sea"
            });

            Hotels.Add(new HotelService()
            {
                Checkin = checkinDate.AddDays(3),
                Checkout = checkinDate.AddDays(33),
                HotelName = "MOUNTAIN INN",
                Meals = "1",
                Notes = "Cozy Atmosphere",
                RoomNo = 20,
                RoomType = "Standard",
                RoomView = "Mountain"
            });

            Hotels.Add(new HotelService()
            {
                Checkin = checkinDate.AddDays(4),
                Checkout = checkinDate.AddDays(34),
                HotelName = "CITY CENTRAL HOTEL",
                Meals = "2",
                Notes = "Convenient Location",
                RoomNo = 25,
                RoomType = "Standard",
                RoomView = "City"
            });

            Hotels.Add(new HotelService()
            {
                Checkin = checkinDate.AddDays(5),
                Checkout = checkinDate.AddDays(35),
                HotelName = "FOREST RETREAT",
                Meals = "3",
                Notes = "Peaceful Surroundings",
                RoomNo = 30,
                RoomType = "Deluxe",
                RoomView = "Forest"
            });

            Hotels.Add(new HotelService()
            {
                Checkin = checkinDate.AddDays(6),
                Checkout = checkinDate.AddDays(36),
                HotelName = "DESERT OASIS",
                Meals = "2",
                Notes = "Unique Experience",
                RoomNo = 35,
                RoomType = "Vip",
                RoomView = "Desert"
            });

        }
        void LoadTransportaionData()
        {
            var todayDate = DateOnly.FromDateTime(DateTime.Now.Date);
            var currentTime = TimeOnly.FromDateTime(DateTime.Now);

            transportaionServices.Add(new TransportaionServiceModdel()
            {
                Brand = "Bmw",
                CarType = "Suv",
                Date = todayDate,
                From = "Cairo",
                Model = "2024",
                Nos = 4,
                time = currentTime,
                To = "Asuut",
                Notes = "Black Color"
            });

            transportaionServices.Add(new TransportaionServiceModdel()
            {
                Brand = "Mercedes",
                CarType = "Sedan",
                Date = todayDate.AddDays(1),
                From = "Alexandria",
                Model = "2023",
                Nos = 3,
                time = currentTime.AddHours(1),
                To = "Cairo",
                Notes = "White Color"
            });

            transportaionServices.Add(new TransportaionServiceModdel()
            {
                Brand = "Audi",
                CarType = "Coupe",
                Date = todayDate.AddDays(2),
                From = "Giza",
                Model = "2022",
                Nos = 2,
                time = currentTime.AddHours(2),
                To = "Luxor",
                Notes = "Red Color"
            });

            transportaionServices.Add(new TransportaionServiceModdel()
            {
                Brand = "Tesla",
                CarType = "Electric",
                Date = todayDate.AddDays(3),
                From = "Hurghada",
                Model = "2024",
                Nos = 4,
                time = currentTime.AddHours(3),
                To = "Sharm El-Sheikh",
                Notes = "Blue Color"
            });

            transportaionServices.Add(new TransportaionServiceModdel()
            {
                Brand = "Toyota",
                CarType = "Van",
                Date = todayDate.AddDays(4),
                From = "Aswan",
                Model = "2021",
                Nos = 8,
                time = currentTime.AddHours(4),
                To = "Cairo",
                Notes = "Green Color"
            });

            transportaionServices.Add(new TransportaionServiceModdel()
            {
                Brand = "Honda",
                CarType = "Hatchback",
                Date = todayDate.AddDays(5),
                From = "Suez",
                Model = "2020",
                Nos = 4,
                time = currentTime.AddHours(5),
                To = "Port Said",
                Notes = "Yellow Color"
            });

            transportaionServices.Add(new TransportaionServiceModdel()
            {
                Brand = "Ford",
                CarType = "Truck",
                Date = todayDate.AddDays(6),
                From = "Ismailia",
                Model = "2023",
                Nos = 6,
                time = currentTime.AddHours(6),
                To = "Cairo",
                Notes = "Silver Color"
            });

        }
        void LoadAirFlightData()
        {
            var flightDate = DateOnly.FromDateTime(DateTime.Now);

            airFlights.Add(new AirFlightModdel()
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
                Notes = "Comfortable Seats"
            });

            airFlights.Add(new AirFlightModdel()
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
                Notes = "Excellent In-Flight Entertainment"
            });

            airFlights.Add(new AirFlightModdel()
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
                Notes = "Friendly Staff"
            });

            airFlights.Add(new AirFlightModdel()
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
                Notes = "Spacious Legroom"
            });

            airFlights.Add(new AirFlightModdel()
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
                Notes = "Great Food"
            });

            airFlights.Add(new AirFlightModdel()
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
                Notes = "Smooth Flight"
            });

        }
        void LoadVisaData()
        {

            visaServices.Add(new VisaServiceModdel()
            {
                VisaType = "Tourist",
                VisaNo = "048",
                Notes = "Single Entry"
            });

            visaServices.Add(new VisaServiceModdel()
            {
                VisaType = "Student",
                VisaNo = "052",
                Notes = "University Enrollment"
            });

            visaServices.Add(new VisaServiceModdel()
            {
                VisaType = "Business",
                VisaNo = "061",
                Notes = "Multiple Entry"
            });

            visaServices.Add(new VisaServiceModdel()
            {
                VisaType = "Transit",
                VisaNo = "072",
                Notes = "24-hour Validity"
            });

            visaServices.Add(new VisaServiceModdel()
            {
                VisaType = "Medical",
                VisaNo = "083",
                Notes = "Hospital Treatment"
            });

            visaServices.Add(new VisaServiceModdel()
            {
                VisaType = "Diplomatic",
                VisaNo = "091",
                Notes = "Government Official"
            });

        } 
        #endregion


    }
}
