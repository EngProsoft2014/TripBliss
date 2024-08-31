using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Models;
using TripBliss.ViewModels.TravelAgenciesViewModels.CreateRequest;
using TripBliss.Pages.TravelAgenciesPages.RequestDetails;
using TripBliss.Helpers;
using TripBliss.Constants;
using Controls.UserDialogs.Maui;

namespace TripBliss.ViewModels.TravelAgenciesViewModels.RequestDetails
{
    public partial class Tr_D_RequestDetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        public RequestTravelAgencyDetailsResponse requestDetailes= new RequestTravelAgencyDetailsResponse();
        


        #region Services
        IGenericRepository Rep;
        readonly Services.Data.ServicesService _service;
        #endregion

        #region Cons
        public Tr_D_RequestDetailsViewModel(int ReqId,IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
            Init(ReqId);
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

            string DisId = Preferences.Default.Get(ApiConstants.travelAgencyCompanyId , "");
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();

                var json = await Rep.GetAsync<RequestTravelAgencyDetailsResponse>(ApiConstants.RequestDetailesApi + $"{DisId}/RequestTravelAgency/{ReqId}", UserToken);

                if (json != null)
                {
                    RequestDetailes = json;
                }
            }
        }

        //void LoadTransportaionData()
        //{
        //    var todayDate = DateOnly.FromDateTime(DateTime.Now.Date);
        //    var currentTime = TimeOnly.FromDateTime(DateTime.Now);

        //    transportaionServices.Add(new TransportaitionRequestDetailesModel()
        //    {
        //        Vehicle = "Suv",
        //        Date = todayDate,
        //        From = "Cairo",
        //        No = 4,
        //        To = "Asuut",
        //    });

        //    transportaionServices.Add(new TransportaitionRequestDetailesModel()
        //    {
        //        Vehicle = "Sedan",
        //        Date = todayDate.AddDays(1),
        //        From = "Alexandria",
        //        No = 3,
        //        To = "Cairo",
        //    });

        //    transportaionServices.Add(new TransportaitionRequestDetailesModel()
        //    {
        //        Vehicle = "Sedan",
        //        Date = todayDate.AddDays(1),
        //        From = "Alexandria",
        //        No = 3,
        //        To = "Cairo",
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
        //        Notes = "Single Entry",
        //        Price = "100"
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
        //void LoadData()
        //{
        //    distributors.Add(new DistributorsModel
        //    {
        //        Address = "Egypt",
        //        Name = "Akl Group",
        //        Phone = "+20155154110",
        //        Rate = "4.5",
        //        Services = "Hotel - Ticketing - Transportation"
        //    });

        //    distributors.Add(new DistributorsModel
        //    {
        //        Address = "Saudi Arabia",
        //        Name = "Al Faisal Company",
        //        Phone = "+966123456789",
        //        Rate = "4.2",
        //        Services = "Hotel - Transportation"
        //    });

        //    distributors.Add(new DistributorsModel
        //    {
        //        Address = "United Arab Emirates",
        //        Name = "Dubai Services",
        //        Phone = "+971987654321",
        //        Rate = "4.7",
        //        Services = "Hotel - Ticketing - Tours"
        //    });

        //    distributors.Add(new DistributorsModel
        //    {
        //        Address = "Qatar",
        //        Name = "Qatar Hospitality",
        //        Phone = "+974654321987",
        //        Rate = "4.3",
        //        Services = "Hotel - Transportation - Ticketing"
        //    });

        //    distributors.Add(new DistributorsModel
        //    {
        //        Address = "Kuwait",
        //        Name = "Kuwait Travels",
        //        Phone = "+965321654987",
        //        Rate = "4.6",
        //        Services = "Hotel - Ticketing - Transportation"
        //    });

        //    distributors.Add(new DistributorsModel
        //    {
        //        Address = "Bahrain",
        //        Name = "Bahrain Tour Services",
        //        Phone = "+973789456123",
        //        Rate = "4.4",
        //        Services = "Hotel - Tours - Transportation"
        //    });

        //    distributors.Add(new DistributorsModel
        //    {
        //        Address = "Oman",
        //        Name = "Oman Travel Agency",
        //        Phone = "+968456123789",
        //        Rate = "4.8",
        //        Services = "Hotel - Ticketing - Transportation"
        //    });

        //} 
        #endregion

        #region RelayCommand
        [RelayCommand]
        async Task Selection(ResponseWithDistributorResponse model)
        {
            await App.Current!.MainPage!.Navigation.PushAsync(new NewRequestPage(new Tr_D_NewRequestViewModel(model,Rep,_service),Rep));
        }
        [RelayCommand]
        async Task BackButton()
        {
            await App.Current!.MainPage!.Navigation.PopAsync();
        } 
        #endregion
    }
}
