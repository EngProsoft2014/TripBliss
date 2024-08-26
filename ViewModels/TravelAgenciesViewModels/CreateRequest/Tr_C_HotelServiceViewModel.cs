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
using CommunityToolkit.Maui.Alerts;
using Controls.UserDialogs.Maui;


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
            HotelRequestModel!.CheckIn = DateTime.Now;
            HotelRequestModel!.CheckOut = DateTime.Now.AddDays(7);
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
            HotelRequestModel!.CheckIn = DateTime.Now;
            HotelRequestModel!.CheckOut = DateTime.Now.AddDays(7);
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

        async void GetHotels()
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

        async void GetMeals()
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

        async void GetRoomTypes()
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

        async void GetRoomViews()
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
            else if (request.RoomCount == 0 )
            {
                var toast = Toast.Make("Please Complete This Field Required : Room Count.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (request.CheckIn > request.CheckOut)
            {
                var toast = Toast.Make("Arrival date must be less than departure date.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else
            {
                
                IsBusy = false;
                UserDialogs.Instance.ShowLoading();

                request.HotelId = SelectedHotel!.Id;
                request.RoomViewId = SelectedRoomView!.Id;
                request.MealId = SelectedMeal!.Id;
                request.LocationId = SelectedLocation!.Id;
                request.RoomTypeId = SelectedRoomType!.Id;

                HotelResponseModel!.HotelName = SelectedHotel!.HotelName;
                HotelResponseModel!.CheckIn = request.CheckIn;
                HotelResponseModel!.CheckOut = request.CheckOut;
                HotelResponseModel!.RoomViewName = SelectedRoomView!.RoomViewName;
                HotelResponseModel!.LocationName = SelectedLocation!.LocationName;

                HotelClose.Invoke(request, HotelResponseModel);
                await App.Current!.MainPage!.Navigation.PopAsync();

                UserDialogs.Instance.HideHud();
                IsBusy = true;
            }

        }
        #endregion
    }
}
