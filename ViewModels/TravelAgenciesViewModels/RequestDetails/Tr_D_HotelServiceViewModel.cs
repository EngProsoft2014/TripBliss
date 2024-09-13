using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using System.Collections.ObjectModel;
using TripBliss.Constants;
using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.Pages.ActivateDetailsPages;
using TripBliss.ViewModels.ActivateViewModels;


namespace TripBliss.ViewModels.TravelAgenciesViewModels.RequestDetails
{
    public partial class Tr_D_HotelServiceViewModel : BaseViewModel
    {
        #region prop
        [ObservableProperty]
        RequestTravelAgencyHotelRequest? hotelRequestModel = new RequestTravelAgencyHotelRequest();
        [ObservableProperty]
        ResponseWithDistributorHotelResponse? hotelService = new ResponseWithDistributorHotelResponse();

        [ObservableProperty]
        ObservableCollection<LocationResponse> locations = new ObservableCollection<LocationResponse>();
        [ObservableProperty]
        ObservableCollection<HotelResponse> hoteles = new ObservableCollection<HotelResponse>();
        [ObservableProperty]
        ObservableCollection<MealResponse> meals = new ObservableCollection<MealResponse>();
        [ObservableProperty]
        ObservableCollection<RoomTypeResponse> roomTypes = new ObservableCollection<RoomTypeResponse>();
        [ObservableProperty]
        ObservableCollection<RoomViewResponse> roomViews = new ObservableCollection<RoomViewResponse>();

        [ObservableProperty]
        LocationResponse? selectedLocation;
        [ObservableProperty]
        HotelResponse? selectedHotel;
        [ObservableProperty]
        MealResponse? selectedMeal;
        [ObservableProperty]
        RoomTypeResponse? selectedRoomType;
        [ObservableProperty]
        RoomViewResponse? selectedRoomView;
        [ObservableProperty]
        public int totalPayment = 0;

        public delegate void HotelDelegte(ResponseWithDistributorHotelResponse HotelResponse);
        public event HotelDelegte HotelClose;
        #endregion

        #region Services
        readonly Services.Data.ServicesService _service;
        IGenericRepository Rep;
        #endregion
        public Tr_D_HotelServiceViewModel(IGenericRepository generic)
        {
            Rep = generic;
            Init();
        }
        public Tr_D_HotelServiceViewModel(int payment,ResponseWithDistributorHotelResponse model, IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            Lang = Preferences.Default.Get("Lan", "en");
            HotelService = model;
            _service = service;
            TotalPayment = payment;
            Init(model);
        }

        #region Methods
        async void Init(ResponseWithDistributorHotelResponse model)
        {
            await RunManyMethods();
            HotelRequestModel = new RequestTravelAgencyHotelRequest
            {
                CheckIn = model.RequestTravelAgencyHotel.CheckIn,
                CheckOut = model.RequestTravelAgencyHotel.CheckOut,
                Notes = model.Notes,
                RoomCount = model.RequestTravelAgencyHotel.RoomCount,

            };
            SelectedHotel = Hoteles.FirstOrDefault(a => a.Id == model.RequestTravelAgencyHotel.HotelId)!;
            SelectedLocation = Locations.FirstOrDefault(a => a.Id == model.RequestTravelAgencyHotel.LocationId)!;
            SelectedRoomType = RoomTypes.FirstOrDefault(a => a.Id == model.RequestTravelAgencyHotel.RoomTypeId)!;
            SelectedRoomView = RoomViews.FirstOrDefault(a => a.Id == model.RequestTravelAgencyHotel.RoomViewId)!;
            SelectedMeal = Meals.FirstOrDefault(a => a.Id == model.RequestTravelAgencyHotel.MealId)!;
        }

        async void Init()
        {
            await RunManyMethods();
        }
        async Task RunManyMethods()
        {
            UserDialogs.Instance.ShowLoading();
            await Task.WhenAll(
                GetLocation(),
                GetHotels(),
                GetMeals(),
                GetRoomViews(),
                GetRoomTypes()
                );
            UserDialogs.Instance.HideHud();
        }

        async Task GetLocation()
        {


            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();

                var json = await Rep.GetAsync<ObservableCollection<LocationResponse>>(ApiConstants.GetAllLocationsApi, UserToken);

                if (json != null)
                {
                    Locations = json;
                }
            }


        }

        async Task GetHotels()
        {


            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();

                var json = await Rep.GetAsync<ObservableCollection<HotelResponse>>(ApiConstants.GetAllHotelsApi, UserToken);

                if (json != null)
                {
                    Hoteles = json;
                }
            }


        }

        async Task GetMeals()
        {

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();

                var json = await Rep.GetAsync<ObservableCollection<MealResponse>>(ApiConstants.GetAllMealsApi, UserToken);

                if (json != null)
                {
                    Meals = json;
                }
            }


        }

        async Task GetRoomTypes()
        {


            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();

                var json = await Rep.GetAsync<ObservableCollection<RoomTypeResponse>>(ApiConstants.GetAllRoomTypesApi, UserToken);

                if (json != null)
                {
                    RoomTypes = json;
                }
            }


        }

        async Task GetRoomViews()
        {


            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();

                var json = await Rep.GetAsync<ObservableCollection<RoomViewResponse>>(ApiConstants.GetAllRoomViewsApi, UserToken);

                if (json != null)
                {
                    RoomViews = json;
                }
            }


        }
        #endregion

        #region RelayCommand
        [RelayCommand]
        void AddRoom()
        {
            if (HotelRequestModel!.RoomCount >= 0)
            {
                HotelRequestModel!.RoomCount += 1;
            }
        }

        [RelayCommand]
        void DeletRoom()
        {
            if (HotelRequestModel!.RoomCount >= 0)
            {
                HotelRequestModel!.RoomCount -= 1;
            }
        }
        [RelayCommand]
        async Task BackClicked()
        {
            await App.Current!.MainPage!.Navigation.PopAsync();
        }

        [RelayCommand]
        async Task ApplyHotelClicked(RequestTravelAgencyHotelRequest request)
        {
            if (SelectedLocation == null || SelectedLocation?.Id == 0)
            {
                var toast = Toast.Make("Please Complete This Field Required : Select Location.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (SelectedHotel == null || SelectedHotel?.Id == 0)
            {
                var toast = Toast.Make("Please Complete This Field Required : Select Hotel.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (SelectedRoomView == null || SelectedRoomView?.Id == 0)
            {
                var toast = Toast.Make("Please Complete This Field Required : Select Room View.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (SelectedRoomType == null || SelectedRoomType?.Id == 0)
            {
                var toast = Toast.Make("Please Complete This Field Required : Select Room Type.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (SelectedMeal == null || SelectedMeal?.Id == 0)
            {
                var toast = Toast.Make("Please Complete This Field Required : Select Meal.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (request.RoomCount == 0)
            {
                var toast = Toast.Make("Please Complete This Field Required : Room Count.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (request.CheckIn.Date > request.CheckOut.Date)
            {
                var toast = Toast.Make("Arrival date must be less than departure date.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
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
            if(TotalPayment == 0)
            {
                var toast = Toast.Make("Please make sure to pay part of the amount due.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else
            {
                var vm = new MainActivateViewModel(HotelService!, Rep, _service);
                var page = new MainActivatePage(vm);
                page.BindingContext = vm;
                await App.Current!.MainPage!.Navigation.PushAsync(page);
            }
            
        }
        #endregion
    }
}
