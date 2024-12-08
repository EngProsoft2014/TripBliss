﻿using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Http;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Constants;
using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.Pages.Shared;

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
            TransportRequestModel!.Date = DateOnly.FromDateTime(DateTime.Now);
            TransportRequestModel!.DateVM = DateTime.Now;
            Init();
        }
        public Tr_C_TransportaionServiceViewModel(RequestTravelAgencyTransportResponse model , IGenericRepository generic , Services.Data.ServicesService service)
        {
            Rep = generic;
            //TransportResponseModel = model;
            TransportRequestModel!.Date = DateOnly.FromDateTime(DateTime.Now);
            TransportRequestModel!.DateVM = DateTime.Now;
            _service = service;
            Init(model);
        }
        #endregion

        #region Methods
        async Task Init(RequestTravelAgencyTransportResponse model)
        {
            UserDialogs.Instance.ShowLoading();
            await Task.WhenAll(GetCarBrands(), GetCarModels(), GetCarTypes());
            UserDialogs.Instance.HideHud();

            TransportRequestModel = new RequestTravelAgencyTransportRequest
            {
                FromLocation = model.FromLocation,
                ToLocation = model.ToLocation,
                Date = model.Date,
                DateVM = model.Date.ToDateTime(new TimeOnly(0, 0)),
                Notes = model.Notes,
                Time = model.Time,
                TransportCount = model.TransportCount,
            };
            SelectrdBrand = CarBrands.FirstOrDefault(a=>a.Id == model.CarBrandId)!;
            SelectrdModel = CarModel.FirstOrDefault(a=>a.Id == model.CarModelId)!;
            SelectrdType = CarTypes.FirstOrDefault(a=>a.Id == model.CarTypeId)!;

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
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_SelectCarType, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (SelectrdBrand == null || SelectrdBrand?.Id == 0)
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_SelectCarBrand, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (SelectrdModel == null || SelectrdModel?.Id == 0)
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_SelectCarModel, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (request.TransportCount == 0)
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_TransportCount, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (request.DateVM < DateTime.Now)
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_Date, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (string.IsNullOrEmpty(request.FromLocation))
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_FromLocation, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (string.IsNullOrEmpty(request.ToLocation))
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_ToLocation, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else
            {
                IsBusy = false;
                UserDialogs.Instance.ShowLoading();

                request.Date = DateOnly.FromDateTime(request.DateVM);

                request.CarBrandId = SelectrdBrand!.Id;
                request.CarTypeId = SelectrdType!.Id;
                request.CarModelId = SelectrdModel!.Id;

                TransportResponseModel!.CarBrandId = SelectrdBrand!.Id;
                TransportResponseModel.CarTypeId = SelectrdType!.Id;
                TransportResponseModel.CarModelId = SelectrdModel!.Id;
                TransportResponseModel!.FromLocation = request.FromLocation;
                TransportResponseModel.ToLocation = request.ToLocation;
                TransportResponseModel.Date = request.Date;
                TransportResponseModel.TransportCount = request.TransportCount;
                TransportResponseModel.TypeName = SelectrdType.TypeName;
                TransportResponseModel.TypeNameAr = SelectrdType.TypeNameAr;
                TransportResponseModel.Notes = request.Notes;
                Controls.StaticMember.EndRequestStatic = (request.Date > DateOnly.FromDateTime(Controls.StaticMember.EndRequestStatic)) ? request.Date.ToDateTime(new TimeOnly(0,0)) : Controls.StaticMember.EndRequestStatic;

                TransportClose.Invoke(request, TransportResponseModel);
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
