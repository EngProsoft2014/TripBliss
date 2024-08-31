using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Models;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using Microsoft.VisualBasic;
using TripBliss.Pages.TravelAgenciesPages.RequestDetails;
using TripBliss.Helpers;
using TripBliss.ViewModels.TravelAgenciesViewModels.CreateRequest;
using Controls.UserDialogs.Maui;
using TripBliss.Constants;
using CommunityToolkit.Maui.Alerts;
using TripBliss.Pages.TravelAgenciesPages;

namespace TripBliss.ViewModels.TravelAgenciesViewModels.RequestDetails
{
    public partial class Tr_D_NewRequestViewModel : BaseViewModel
    {


        #region prop
        [ObservableProperty]
        ResponseWithDistributorDetailsResponse response = new ResponseWithDistributorDetailsResponse();
        
        #endregion

        #region Services
        IGenericRepository Rep;
        readonly Services.Data.ServicesService _service;
        #endregion

        #region Cons
        public Tr_D_NewRequestViewModel(ResponseWithDistributorResponse distributorResponse, IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
            _ = Init(distributorResponse.DistributorCompanyId, distributorResponse.Id);
        } 
        #endregion

        #region Generl RelayCommand
        [RelayCommand]
        void BackButtonClicked()
        {
            App.Current.MainPage.Navigation.PopAsync();
        }
        [RelayCommand]
        async Task Apply()
        {
            bool answer = await App.Current!.MainPage!.DisplayAlert("Question?", "Are You Accept This Finall Price?", "Yes", "No");

            IsBusy = false;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet && answer)
            {

                string UserToken = await _service.UserToken();

                
                var json = await Rep.PostTRAsync<ResponseWithDistributorDetailsResponse, ResponseWithDistributorResponse>(ApiConstants.ResponseDetailsDistApi + $"{Response.DistributorCompanyId}/ResponseWithDistributor/{Response.Id}", Response, UserToken);

                if (json.Item1 != null)
                {
                    var toast = Toast.Make("Successfully for Add Response", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();

                    Controls.StaticMember.WayOfTab = 0;
                    await App.Current!.MainPage!.Navigation.PushAsync(new HomeAgencyPage(new Tr_HomeViewModel(Rep, _service), Rep, _service));
                }
                else
                {
                    var toast = Toast.Make($"Warning, {json.Item2}", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
            }

            IsBusy = true;
        }
        #endregion

        #region Hotel RelayCommand
        [RelayCommand]
        void SelectHotel(ResponseWithDistributorHotelResponse model)
        {
            var vm = new Tr_D_HotelServiceViewModel(model,Rep,_service);
            var page = new HotelServicePage(vm,Rep);
            page.BindingContext = vm;
            App.Current!.MainPage!.Navigation.PushAsync(page);
        } 
        #endregion

        #region Transportaiton RelayCommand
        [RelayCommand]
        async Task SelectTransportaion(ResponseWithDistributorTransportResponse model)
        {
            var vm = new Tr_D_TransportaionServiceViewModel(model,Rep, _service);
            var page = new TransportaionServicePage(vm,Rep);
            page.BindingContext = vm;
            await App.Current!.MainPage!.Navigation.PushAsync(page);
        } 
        #endregion

        #region Air Flight RelayCommand
        [RelayCommand]
        void SelectAirFlight(ResponseWithDistributorAirFlightResponse model)
        {
            var vm = new Tr_D_AirFlightServicesViewModel(model,Rep,_service);
            var page = new AirFlightServicePage(vm,Rep);
            page.BindingContext = vm;
            App.Current!.MainPage!.Navigation.PushAsync(page);
        } 
        #endregion

        #region Visa RelayCommand
        [RelayCommand]
        async Task SelectVisa(ResponseWithDistributorVisaResponse model)
        {
            var vm = new Tr_D_VisaServiceViewModel(model,Rep, _service);
            var page = new VisaServicePage(vm,Rep);
            page.BindingContext = vm;
            await App.Current!.MainPage!.Navigation.PushAsync(page);
        }
        #endregion

        #region Methodes
        async Task Init(string distId,int ReqId)
        {
            UserDialogs.Instance.ShowLoading();
            await GetRequestDetailes(distId,ReqId);
            UserDialogs.Instance.HideHud();
        }
        async Task GetRequestDetailes(string distId, int ReqId)
        {

            
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();

                var json = await Rep.GetAsync<ResponseWithDistributorDetailsResponse>(ApiConstants.ResponseDetailsDistApi + $"{distId}/ResponseWithDistributor/{ReqId}", UserToken);

                if (json != null)
                {
                    Response = json;
                }
            }
        }
        
        #endregion


    }
}
