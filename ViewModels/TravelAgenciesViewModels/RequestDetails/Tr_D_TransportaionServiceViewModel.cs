using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using System.Collections.ObjectModel;
using TripBliss.Constants;
using TripBliss.Helpers;
using TripBliss.Models;

namespace TripBliss.ViewModels.TravelAgenciesViewModels.RequestDetails
{
    public partial class Tr_D_TransportaionServiceViewModel : BaseViewModel
    {
        #region Prop
        [ObservableProperty]
        ResponseWithDistributorTransportResponse serviceModdel = new ResponseWithDistributorTransportResponse();
        [ObservableProperty]
        RequestTravelAgencyTransportRequest? transportRequestModel = new RequestTravelAgencyTransportRequest();

        [ObservableProperty]
        ObservableCollection<CarBrandResponse> carBrands = new ObservableCollection<CarBrandResponse>();
        [ObservableProperty]
        ObservableCollection<CarModelResponse> carModel = new ObservableCollection<CarModelResponse>();
        [ObservableProperty]
        ObservableCollection<CarTypeResponse> carTypes = new ObservableCollection<CarTypeResponse>();

        [ObservableProperty]
        CarBrandResponse selectrdBrand = new CarBrandResponse();
        [ObservableProperty]
        CarModelResponse selectrdModel = new CarModelResponse();
        [ObservableProperty]
        CarTypeResponse selectrdType = new CarTypeResponse();
        #endregion

        #region Services
        IGenericRepository Rep;
        readonly Services.Data.ServicesService _service;
        #endregion
        #region Const
        public Tr_D_TransportaionServiceViewModel(IGenericRepository generic)
        {
            Rep = generic;
        }
        public Tr_D_TransportaionServiceViewModel(ResponseWithDistributorTransportResponse model, IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            ServiceModdel = model;
            Lang = Preferences.Default.Get("Lan", "en");
            _service = service;
            Init(model);
        }
        #endregion

        #region Methods
        async Task Init(ResponseWithDistributorTransportResponse model)
        {
            UserDialogs.Instance.ShowLoading();
            await Task.WhenAll(GetCarBrands(), GetCarModels(), GetCarTypes());
            UserDialogs.Instance.HideHud();

            TransportRequestModel = new RequestTravelAgencyTransportRequest
            {
                FromLocation = model.RequestTravelAgencyTransport.FromLocation,
                ToLocation = model.RequestTravelAgencyTransport.ToLocation,
                Date = model.RequestTravelAgencyTransport.Date,
                Notes = model.Notes,
                Time = model.RequestTravelAgencyTransport.Time,
                TransportCount = model.RequestTravelAgencyTransport.TransportCount,
            };
            SelectrdBrand = CarBrands.FirstOrDefault(a => a.Id == model.RequestTravelAgencyTransport.CarBrandId)!;
            SelectrdModel = CarModel.FirstOrDefault(a => a.Id == model.RequestTravelAgencyTransport.CarModelId)!;
            SelectrdType = CarTypes.FirstOrDefault(a => a.Id == model.RequestTravelAgencyTransport.CarTypeId)!;

        }
        async Task Init()
        {
            UserDialogs.Instance.ShowLoading();
            await Task.WhenAll(GetCarBrands(), GetCarModels(), GetCarTypes());
            UserDialogs.Instance.HideHud();
        }
        async Task GetCarBrands()
        {

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();

                var json = await Rep.GetAsync<ObservableCollection<CarBrandResponse>>(ApiConstants.GetAllCarBrandsApi, UserToken);

                if (json != null)
                {
                    CarBrands = json;
                }
            }
        }

        async Task GetCarModels()
        {

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();

                var json = await Rep.GetAsync<ObservableCollection<CarModelResponse>>(ApiConstants.GetAllCarModelsApi, UserToken);

                if (json != null)
                {
                    CarModel = json;
                }
            }

        }

        async Task GetCarTypes()
        {

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();

                var json = await Rep.GetAsync<ObservableCollection<CarTypeResponse>>(ApiConstants.GetAllCarTypesApi, UserToken);

                if (json != null)
                {
                    CarTypes = json;
                }
            }
        }
        #endregion

        #region RelayCommand
        [RelayCommand]
        async Task OnApply(RequestTravelAgencyTransportRequest request)
        {
            if (SelectrdType == null || SelectrdType?.Id == 0)
            {
                var toast = Toast.Make("Please Complete This Field Required : Select Car Type.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (SelectrdBrand == null || SelectrdBrand?.Id == 0)
            {
                var toast = Toast.Make("Please Complete This Field Required : Select Car Brand.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (SelectrdModel == null || SelectrdModel?.Id == 0)
            {
                var toast = Toast.Make("Please Complete This Field Required : Select Car Model.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (request.TransportCount == 0)
            {
                var toast = Toast.Make("Please Complete This Field Required : Transport Count.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (string.IsNullOrEmpty(request.FromLocation))
            {
                var toast = Toast.Make("Please Complete This Field Required : From Location.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (string.IsNullOrEmpty(request.ToLocation))
            {
                var toast = Toast.Make("Please Complete This Field Required : To Location.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
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
        async Task OnBackButtonClicked()
        {
            await App.Current!.MainPage!.Navigation.PopAsync();
        }
        #endregion
    }
}
