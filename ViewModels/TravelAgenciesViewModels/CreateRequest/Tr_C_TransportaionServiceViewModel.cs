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
        TransportaionServiceModel serviceModdel = new TransportaionServiceModel();
        [ObservableProperty]
        ObservableCollection<CarBrandResponse> carBrands = new ObservableCollection<CarBrandResponse>();
        [ObservableProperty]
        ObservableCollection<CarModelResponse> carModel = new ObservableCollection<CarModelResponse>();
        [ObservableProperty]
        ObservableCollection<CarTypeResponse> carTypes = new ObservableCollection<CarTypeResponse>();

        #endregion

        #region Services
        IGenericRepository Rep;
        readonly Services.Data.ServicesService _service;
        #endregion

        #region Const
        public Tr_C_TransportaionServiceViewModel(IGenericRepository generic , Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
            GetCarBrands();
            GetCarModels();
            GetCarTypes();
        }
        public Tr_C_TransportaionServiceViewModel(TransportaionServiceModel model , IGenericRepository generic , Services.Data.ServicesService service)
        {
            Rep = generic;
            ServiceModdel = model;
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
        void OnApply()
        {
            if (ServiceModdel != null)
            {
                App.Current.MainPage.Navigation.PopAsync();
            }
        }

        [RelayCommand]
        void OnBackButtonClicked()
        {
            App.Current.MainPage.Navigation.PopAsync();
        } 
        #endregion
    }
}
