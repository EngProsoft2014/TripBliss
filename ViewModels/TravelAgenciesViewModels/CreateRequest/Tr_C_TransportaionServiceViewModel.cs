using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Constants;
using TripBliss.Helpers;
using TripBliss.Models;

namespace TripBliss.ViewModels.TravelAgenciesViewModels.CreateRequest
{
    public partial class Tr_C_TransportaionServiceViewModel : BaseViewModel
    {
        #region Prop
        [ObservableProperty]
        RequestTravelAgencyTransportRequest? transportRequestModel = new RequestTravelAgencyTransportRequest();
        [ObservableProperty]
        RequestTravelAgencyTransportResponse? transportResponseModel = new RequestTravelAgencyTransportResponse();

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
        public delegate void TransportDelegte(RequestTravelAgencyTransportRequest TransportRequest, RequestTravelAgencyTransportResponse TransportResponse);
        public event TransportDelegte TransportClose;

        #region Services
        IGenericRepository Rep;
        readonly Services.Data.ServicesService _service;
        #endregion

        #region Const
        public Tr_C_TransportaionServiceViewModel(IGenericRepository generic , Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
            TransportRequestModel!.Date = DateTime.Now;
            GetCarBrands();
            GetCarModels();
            GetCarTypes();
        }
        public Tr_C_TransportaionServiceViewModel(RequestTravelAgencyTransportResponse model , IGenericRepository generic , Services.Data.ServicesService service)
        {
            Rep = generic;
            TransportResponseModel = model;
            TransportRequestModel!.Date = DateTime.Now;
            _service = service;
            GetCarBrands();
            GetCarModels();
            GetCarTypes();
        }
        #endregion

        #region Methods
        async void GetCarBrands()
        {
            IsBusy = true;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();

                var json = await Rep.GetAsync<ObservableCollection<CarBrandResponse>>(ApiConstants.GetAllCarBrandsApi, UserToken);

                if (json != null)
                {
                    CarBrands = json;
                }
            }

            IsBusy = false;
        }

        async void GetCarModels()
        {
            IsBusy = true;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();

                var json = await Rep.GetAsync<ObservableCollection<CarModelResponse>>(ApiConstants.GetAllCarModelsApi, UserToken);

                if (json != null)
                {
                    CarModel = json;
                }
            }

            IsBusy = false;
        }

        async void GetCarTypes()
        {
            IsBusy = true;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();

                var json = await Rep.GetAsync<ObservableCollection<CarTypeResponse>>(ApiConstants.GetAllCarTypesApi, UserToken);

                if (json != null)
                {
                    CarTypes = json;
                }
            }

            IsBusy = false;
        }
        #endregion

        #region RelayCommand
        [RelayCommand]
        void OnApply(RequestTravelAgencyTransportRequest request)
        {
            request.CarBrandId = SelectrdBrand.Id;
            request.CarTypeId = SelectrdType.Id;
            request.CarModelId = SelectrdModel.Id;
            TransportResponseModel!.FromLocation = request.FromLocation;
            TransportResponseModel.ToLocation = request.ToLocation;
            TransportResponseModel.Date = request.Date;
            TransportResponseModel.TransportCount = request.TransportCount;
            TransportResponseModel.TypeName = SelectrdType.TypeName;

            TransportClose.Invoke(request, TransportResponseModel);
            App.Current!.MainPage!.Navigation.PopAsync();
        }

        [RelayCommand]
        void OnBackButtonClicked()
        {
            App.Current.MainPage.Navigation.PopAsync();
        } 
        #endregion
    }
}
