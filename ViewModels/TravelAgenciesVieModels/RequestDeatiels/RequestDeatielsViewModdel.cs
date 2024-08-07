using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Models;
using TripBliss.ViewModels.TravelAgenciesVieModels.CreateRequest;
using TripBliss.Pages.TravelAgenciesPages.RequestDeatiels;

namespace TripBliss.ViewModels.TravelAgenciesVieModels.RequestDeatiels
{
    partial class RequestDeatielsViewModdel : BaseViewModel
    {
        public ObservableCollection<TransportaitionRequestDetailesModdel> transportaionServices { get; set; }
        public ObservableCollection<AirFlightModdel> airFlights { get; set; }
        public ObservableCollection<VisaServiceModdel> visaServices { get; set; }
        public ObservableCollection<DistributorsModdel>? distributors { get; set; }

        public RequestDeatielsViewModdel()
        {
            transportaionServices = new ObservableCollection<TransportaitionRequestDetailesModdel>();
            airFlights = new ObservableCollection<AirFlightModdel>();
            visaServices = new ObservableCollection<VisaServiceModdel>();
            distributors = new ObservableCollection<DistributorsModdel>();
            LoadTransportaionData();
            LoadAirFlightData();
            LoadVisaData();
            LoadData();
        }

        void LoadTransportaionData()
        {
            var todayDate = DateOnly.FromDateTime(DateTime.Now.Date);
            var currentTime = TimeOnly.FromDateTime(DateTime.Now);

            transportaionServices.Add(new TransportaitionRequestDetailesModdel()
            {
                Vechil = "Suv",
                Date = todayDate,
                From = "Cairo",
                No = 4,
                To = "Asuut",
            });

            transportaionServices.Add(new TransportaitionRequestDetailesModdel()
            {
                Vechil = "Sedan",
                Date = todayDate.AddDays(1),
                From = "Alexandria",
                No = 3,
                To = "Cairo",
            });

            transportaionServices.Add(new TransportaitionRequestDetailesModdel()
            {
                Vechil = "Coupe",
                Date = todayDate.AddDays(2),
                From = "Giza",
                No = 2,
                To = "Luxor",
            });

            transportaionServices.Add(new TransportaitionRequestDetailesModdel()
            {
                Vechil = "Electric",
                Date = todayDate.AddDays(3),
                From = "Hurghada",
                No = 4,
                To = "Sharm El-Sheikh",
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
        void LoadData()
        {
            distributors.Add(new DistributorsModdel
            {
                Address = "Egypt",
                Name = "Akl Group",
                Phone = "+20155154110",
                Rate = "4.5",
                Services = "Hotel - Ticketing - Transportation"
            });

            distributors.Add(new DistributorsModdel
            {
                Address = "Saudi Arabia",
                Name = "Al Faisal Company",
                Phone = "+966123456789",
                Rate = "4.2",
                Services = "Hotel - Transportation"
            });

            distributors.Add(new DistributorsModdel
            {
                Address = "United Arab Emirates",
                Name = "Dubai Services",
                Phone = "+971987654321",
                Rate = "4.7",
                Services = "Hotel - Ticketing - Tours"
            });

            distributors.Add(new DistributorsModdel
            {
                Address = "Qatar",
                Name = "Qatar Hospitality",
                Phone = "+974654321987",
                Rate = "4.3",
                Services = "Hotel - Transportation - Ticketing"
            });

            distributors.Add(new DistributorsModdel
            {
                Address = "Kuwait",
                Name = "Kuwait Travels",
                Phone = "+965321654987",
                Rate = "4.6",
                Services = "Hotel - Ticketing - Transportation"
            });

            distributors.Add(new DistributorsModdel
            {
                Address = "Bahrain",
                Name = "Bahrain Tour Services",
                Phone = "+973789456123",
                Rate = "4.4",
                Services = "Hotel - Tours - Transportation"
            });

            distributors.Add(new DistributorsModdel
            {
                Address = "Oman",
                Name = "Oman Travel Agency",
                Phone = "+968456123789",
                Rate = "4.8",
                Services = "Hotel - Ticketing - Transportation"
            });

        }

        [RelayCommand]
        void Selection()
        {
            App.Current.MainPage.Navigation.PushAsync(new NewRequestPage());
        }
        [RelayCommand]
        void BackButton()
        {
            App.Current.MainPage.Navigation.PopAsync();
        }
    }
}
