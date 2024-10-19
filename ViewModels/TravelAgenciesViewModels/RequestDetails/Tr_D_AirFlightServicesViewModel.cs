using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using TripBliss.Constants;
using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.Pages.ActivateDetailsPages;
using TripBliss.Pages.TravelAgenciesPages.ActivateDetailsPages;
using TripBliss.Pages.TravelAgenciesPages.RequestDetails;
using TripBliss.ViewModels.ActivateViewModels;

namespace TripBliss.ViewModels.TravelAgenciesViewModels.RequestDetails
{
    public partial class Tr_D_AirFlightServicesViewModel : BaseViewModel
    {
        //Test
        public class AirLines
        {
            public string? iata_code { get; set; }
            public string? name { get; set; }
            public string? icao_code { get; set; }
            public string? nameVM { get { return name + $" ({iata_code})"; } }
        }
        #region prop

        [ObservableProperty]
        ResponseWithDistributorAirFlightResponse moddel = new ResponseWithDistributorAirFlightResponse();
        [ObservableProperty]
        RequestTravelAgencyAirFlightRequest airFlightRequestModel = new RequestTravelAgencyAirFlightRequest();
        [ObservableProperty]
        ObservableCollection<AirFlightResponse> airFlights = new ObservableCollection<AirFlightResponse>();
        [ObservableProperty]
        ObservableCollection<ClassAirFlightResponse> classes = new ObservableCollection<ClassAirFlightResponse>();
        [ObservableProperty]
        AirFlightResponse airFlightSelected;
        [ObservableProperty]
        ClassAirFlightResponse classSelected;
        [ObservableProperty]
        public int totalPayment = 0;
        [ObservableProperty]
        bool isRequestHistory;
        //Test
        [ObservableProperty]
        ObservableCollection<AirLines> lstAirLines = new ObservableCollection<AirLines>();

        #endregion

        #region Services
        IGenericRepository Rep;
        readonly Services.Data.ServicesService _service;
        #endregion

        #region Cons
        public Tr_D_AirFlightServicesViewModel(IGenericRepository generic)
        {
            Rep = generic;

        }
        public Tr_D_AirFlightServicesViewModel(bool _IsRequestHistory, int payment,ResponseWithDistributorAirFlightResponse model, IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            Moddel = model;
            _service = service;
            IsRequestHistory = _IsRequestHistory;
            TotalPayment = payment;
            if (model.AcceptAgen == false)
            {
                Init(model);
            }
            else
            {
                Init();
            }
            
        }
        #endregion

        #region Methods
        async void Init(ResponseWithDistributorAirFlightResponse model)
        {
            UserDialogs.Instance.ShowLoading();
            //Test
            await GetAirLinesInfo();
            await Task.WhenAll(GetAirFlights(), GetClasses());
            UserDialogs.Instance.HideHud();
            AirFlightRequestModel = new RequestTravelAgencyAirFlightRequest
            {
                Date = model.RequestTravelAgencyAirFlight.Date,
                AirportFrom = model!.RequestTravelAgencyAirFlight!.AirportFrom!,
                AirportTo = model!.RequestTravelAgencyAirFlight!.AirportTo!,
                ETA = model.RequestTravelAgencyAirFlight.ETA,
                ETD = model.RequestTravelAgencyAirFlight.ETD,
                InfoAdultCount = model!.RequestTravelAgencyAirFlight!.InfoAdultCount!,
                InfoChildCount = model.RequestTravelAgencyAirFlight.InfoChildCount,
                InfoInfantCount = model.RequestTravelAgencyAirFlight.InfoInfantCount,
                Notes = model.Notes

            };
            AirFlightSelected = AirFlights.FirstOrDefault(a => a.Id == model.RequestTravelAgencyAirFlight.AirFlightId)!;
            ClassSelected = Classes.FirstOrDefault(a => a.Id == model.RequestTravelAgencyAirFlight.ClassAirFlightId)!;
        }

        void Init()
        {
            AirFlightRequestModel = new RequestTravelAgencyAirFlightRequest
            {
                Date = Moddel.RequestTravelAgencyAirFlight.Date,
                AirportFrom = Moddel!.RequestTravelAgencyAirFlight!.AirportFrom!,
                AirportTo = Moddel!.RequestTravelAgencyAirFlight!.AirportTo!,
                ETA = Moddel.RequestTravelAgencyAirFlight.ETA,
                ETD = Moddel.RequestTravelAgencyAirFlight.ETD,
                InfoAdultCount = Moddel!.RequestTravelAgencyAirFlight!.InfoAdultCount!,
                InfoChildCount = Moddel.RequestTravelAgencyAirFlight.InfoChildCount,
                InfoInfantCount = Moddel.RequestTravelAgencyAirFlight.InfoInfantCount,
                Notes = Moddel.Notes

            };
        }

        //Test
        public async Task GetAirLinesInfo()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://iata-and-icao-codes.p.rapidapi.com/airlines"),
                Headers =
                        {
                            { "x-rapidapi-key", "6fccfd7c71msh7934183b73f2229p11ce70jsn0061fc86c83f" },
                            { "x-rapidapi-host", "iata-and-icao-codes.p.rapidapi.com" },
                        },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                var body = JsonConvert.DeserializeObject<ObservableCollection<AirLines>>(json);
                LstAirLines = body!;
            }
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
            if (AirFlightRequestModel.InfoAdultCount >= 0)
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

            //Test
            AirFlightSelected = AirFlights.FirstOrDefault()!;

            if (AirFlightSelected == null || AirFlightSelected?.Id == 0)
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_SelectCarrier, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (Request.Date.Date < DateTime.Now.Date)
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.The_selected_date_is_less_than_today, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (ClassSelected == null || ClassSelected?.Id == 0)
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_SelectClass, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (string.IsNullOrEmpty(Request.AirportFrom))
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_FromLocation, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (string.IsNullOrEmpty(Request.AirportTo))
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_ToLocation, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (Request.ETD > Request.ETA)
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Arrival_date_must_be_less_than_departure_date, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (Request.InfoAdultCount == 0 & Request.InfoChildCount == 0 && Request.InfoInfantCount == 0)
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_PassengersCount, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else
            {
                IsBusy = false;
                UserDialogs.Instance.ShowLoading();

                await App.Current!.MainPage!.Navigation.PopAsync();

                UserDialogs.Instance.HideHud();
                IsBusy = true;
            }


        }

        [RelayCommand]
        async Task ActiveClicked()
        {
            if (Constants.Permissions.CheckPermission(Constants.Permissions.Show_Attachments))
            {
                if (TotalPayment == 0)
                {
                    var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Agency_must_pay_part_of_the_amount_due, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
                else
                {
                    var vm = new AirFlightActivateViewModel(IsRequestHistory, false, Moddel, Rep, _service);
                    var page = new AirFlightAttachmentsPage(vm);
                    page.BindingContext = vm;
                    await App.Current!.MainPage!.Navigation.PushAsync(page);
                }
            }
            else
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.PermissionAlert, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }

        }

        #endregion
    }
}
