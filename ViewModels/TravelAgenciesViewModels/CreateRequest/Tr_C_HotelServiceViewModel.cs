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
using Microsoft.AspNet.SignalR.Client.Http;
using TripBliss.Pages.Shared;


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
            Init();
        }
        public Tr_C_HotelServiceViewModel(RequestTravelAgencyHotelResponse model, IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
            //HotelResponseModel = model;
            HotelRequestModel!.CheckIn = DateTime.Now;
            HotelRequestModel!.CheckOut = DateTime.Now.AddDays(7);
            Init(model);
        }
        #endregion

        #region Methods

        async void Init(RequestTravelAgencyHotelResponse model)
        {
            await RunManyMethods();
            HotelRequestModel = new RequestTravelAgencyHotelRequest
            {
                CheckIn = model.CheckIn,
                CheckOut = model.CheckOut,
                Notes = model.Notes,
                RoomCount = model.RoomCount,
                
            };
            SelectedHotel = Hoteles.FirstOrDefault(a=>a.Id == model.HotelId)!;
            SelectedLocation = Locations.FirstOrDefault(a=>a.Id == model.LocationId)!;
            SelectedRoomType = RoomTypes.FirstOrDefault(a=>a.Id == model.RoomTypeId)!;
            SelectedRoomView = RoomViews.FirstOrDefault(a=>a.Id == model.RoomViewId)!;
            SelectedMeal = Meals.FirstOrDefault(a=>a.Id == model.MealId)!;
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
                    Hoteles = new ObservableCollection<HotelResponse>(json.OrderBy(d => d.HotelNameLang).ToList());
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
            if (HotelRequestModel!.RoomCount >=0)
            {
                HotelRequestModel!.RoomCount += 1;
            }
        }

        [RelayCommand]
        void DeletRoom()
        {
            if (HotelRequestModel!.RoomCount > 0)
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
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_SelectLocation, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (SelectedHotel == null || SelectedHotel?.Id == 0)
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_SelectHotel, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (SelectedRoomView == null || SelectedRoomView?.Id == 0)
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_SelectRoomView, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (SelectedRoomType == null || SelectedRoomType?.Id == 0)
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_SelectRoomType, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (SelectedMeal == null || SelectedMeal?.Id == 0)
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_SelectMeal, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (request.RoomCount == 0 )
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_RoomCount, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (request.CheckIn < DateTime.Now)
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.checkin_date_today_or_after, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (request.CheckIn.Date > request.CheckOut.Date)
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Arrival_date_must_be_less_than_departure_date, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else
            {
                
                IsBusy = false;
                UserDialogs.Instance.ShowLoading();

                request.HotelId = HotelResponseModel!.HotelId = SelectedHotel!.Id;
                request.RoomViewId = HotelResponseModel!.RoomViewId = SelectedRoomView!.Id;
                request.MealId = HotelResponseModel!.MealId = SelectedMeal!.Id;
                request.LocationId = HotelResponseModel!.LocationId = SelectedLocation!.Id;
                request.RoomTypeId = HotelResponseModel!.RoomTypeId = SelectedRoomType!.Id;

                HotelResponseModel!.HotelName = SelectedHotel!.HotelName;
                HotelResponseModel!.HotelNameAr = SelectedHotel!.HotelNameAr;
                HotelResponseModel!.CheckIn = request.CheckIn;
                HotelResponseModel!.CheckOut = request.CheckOut;
                HotelResponseModel!.Notes = request.Notes;
                HotelResponseModel!.RoomViewName = SelectedRoomView!.RoomViewName;
                HotelResponseModel!.RoomViewNameAr = SelectedRoomView!.RoomViewNameAr;
                HotelResponseModel!.LocationName = SelectedLocation!.LocationName;
                HotelResponseModel!.LocationNameAr = SelectedLocation!.LocationNameAr;
                HotelResponseModel!.RoomCount = request.RoomCount;
                Controls.StaticMember.EndRequestStatic = (request.CheckOut > Controls.StaticMember.EndRequestStatic) ? request.CheckOut : Controls.StaticMember.EndRequestStatic;
                HotelClose.Invoke(request, HotelResponseModel);
                await App.Current!.MainPage!.Navigation.PopAsync();

                UserDialogs.Instance.HideHud();
                IsBusy = true;
            }

        }
        #endregion
    }
}
