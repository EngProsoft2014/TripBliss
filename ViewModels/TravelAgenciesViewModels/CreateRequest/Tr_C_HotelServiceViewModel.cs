using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TripBliss.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using TripBliss.Helpers;
using TripBliss.Constants;


namespace TripBliss.ViewModels.TravelAgenciesViewModels.CreateRequest
{
    public partial class Tr_C_HotelServiceViewModel : BaseViewModel
    {
        #region prop
        [ObservableProperty]
        RequestTravelAgencyHotelRequest? hotelRequestModel = new RequestTravelAgencyHotelRequest();
        [ObservableProperty]
        RequestTravelAgencyHotelResponse? hotelResponseModel = new RequestTravelAgencyHotelResponse();

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
        LocationResponse ? selectedLocation;
        [ObservableProperty]
        HotelResponse? selectedHotel;
        [ObservableProperty]
        MealResponse? selectedMeal;
        [ObservableProperty]
        RoomTypeResponse? selectedRoomType;
        [ObservableProperty]
        RoomViewResponse? selectedRoomView;

        #endregion

        public delegate void HotelDelegte(RequestTravelAgencyHotelRequest HotelRequest, RequestTravelAgencyHotelResponse HotelResponse);
        public event HotelDelegte HotelClose;

        #region Services
        readonly Services.Data.ServicesService _service;
        IGenericRepository Rep; 
        #endregion

        #region Cons
        public Tr_C_HotelServiceViewModel(IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
            GetLocation();
            GetHotels();
            GetMeals();
            GetRoomTypes();
            GetRoomViews();
        }
        public Tr_C_HotelServiceViewModel(RequestTravelAgencyHotelResponse model, IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
            HotelResponseModel = model;

            GetLocation();
            GetHotels();
            GetMeals();
            GetRoomTypes();
            GetRoomViews();
        }
        #endregion

        #region Methods
        async void GetLocation()
        {
            IsBusy = true;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();

                var json = await Rep.GetAsync<ObservableCollection<LocationResponse>>(ApiConstants.GetAllLocationsApi, UserToken);

                if (json != null)
                {
                    Locations = json;
                }
            }

            IsBusy = false;
        }

        async void GetHotels()
        {
            IsBusy = true;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();

                var json = await Rep.GetAsync<ObservableCollection<HotelResponse>>(ApiConstants.GetAllHotelsApi, UserToken);

                if (json != null)
                {
                    Hoteles = json;
                }
            }

            IsBusy = false;
        }

        async void GetMeals()
        {
            IsBusy = true;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();

                var json = await Rep.GetAsync<ObservableCollection<MealResponse>>(ApiConstants.GetAllMealsApi, UserToken);

                if (json != null)
                {
                    Meals = json;
                }
            }

            IsBusy = false;
        }

        async void GetRoomTypes()
        {
            IsBusy = true;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();

                var json = await Rep.GetAsync<ObservableCollection<RoomTypeResponse>>(ApiConstants.GetAllRoomTypesApi, UserToken);

                if (json != null)
                {
                    RoomTypes = json;
                }
            }

            IsBusy = false;
        }

        async void GetRoomViews()
        {
            IsBusy = true;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();

                var json = await Rep.GetAsync<ObservableCollection<RoomViewResponse>>(ApiConstants.GetAllRoomViewsApi, UserToken);

                if (json != null)
                {
                    RoomViews = json;
                }
            }

            IsBusy = false;
        } 
        #endregion

        #region RelayCommand
        [RelayCommand]
        void AddRoom()
        {
            if (HotelRequestModel!.RoomCount >=0)
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
            await App.Current.MainPage.Navigation.PopAsync();
        }

        [RelayCommand]
        async Task ApplyHotelClicked(RequestTravelAgencyHotelRequest request)
        {
            request.HotelId = SelectedHotel!.Id;
            request.RoomViewId = SelectedRoomView!.Id;
            request.MealId = SelectedMeal!.Id;
            request.LocationId = SelectedLocation!.Id;
            request.RoomTypeId = SelectedRoomType!.Id;

            HotelResponseModel!.HotelName = SelectedHotel!.HotelName;
            HotelResponseModel!.CheckIn = request.CheckIn;
            HotelResponseModel!.CheckOut = request.CheckOut;
            HotelResponseModel!.RoomViewName = SelectedRoomView!.RoomViewName;

            HotelClose.Invoke(request, HotelResponseModel);
            await App.Current!.MainPage!.Navigation.PopAsync();
        }
        #endregion
    }
}
