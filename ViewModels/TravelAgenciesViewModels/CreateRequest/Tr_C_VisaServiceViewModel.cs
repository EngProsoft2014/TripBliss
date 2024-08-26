using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Constants;
using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.Models.Visa;

namespace TripBliss.ViewModels.TravelAgenciesViewModels.CreateRequest
{
    public partial class Tr_C_VisaServiceViewModel : BaseViewModel
    {
        #region Prop

        [ObservableProperty]
        RequestTravelAgencyVisaRequest? visaRequestModel = new RequestTravelAgencyVisaRequest();
        [ObservableProperty]
        RequestTravelAgencyVisaResponse? visaResponseModel = new RequestTravelAgencyVisaResponse();


        [ObservableProperty]
        VisaResponse selectedVisa = new VisaResponse();
        [ObservableProperty]
        ObservableCollection<VisaResponse> visas = new ObservableCollection<VisaResponse>();
        #endregion

        public delegate void VisaDelegte(RequestTravelAgencyVisaRequest VisaRequest, RequestTravelAgencyVisaResponse VisaResponse);
        public event VisaDelegte VisaClose;

        #region Services
        IGenericRepository Rep;
        readonly Services.Data.ServicesService _service; 
        #endregion

        #region Cons
        public Tr_C_VisaServiceViewModel(IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
            GetVisas();
        }
        public Tr_C_VisaServiceViewModel(RequestTravelAgencyVisaResponse model, IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
            visaResponseModel = model;
            GetVisas();
        }
        #endregion

        #region Methods
        async void GetVisas()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();

                var json = await Rep.GetAsync<ObservableCollection<VisaResponse>>(ApiConstants.GetVisasApi, UserToken);

                if (json != null)
                {
                    Visas = json;
                }
            }
        } 
        #endregion

        #region RelayCommand
        [RelayCommand]
        async Task Apply(RequestTravelAgencyVisaRequest request)
        {
            if (SelectedVisa == null || SelectedVisa?.Id == 0)
            {
                var toast = Toast.Make("Please Complete This Field Required : Select Type of Visa.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (request.PersonCount == 0)
            {
                var toast = Toast.Make("Please Complete This Field Required : passengers Count.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else
            {
                IsBusy = false;
                UserDialogs.Instance.ShowLoading();

                VisaRequestModel!.VisaId = SelectedVisa!.Id;
                VisaResponseModel!.VisaName = SelectedVisa.VisaName;
                VisaResponseModel!.PersonCount = request.PersonCount;
                VisaClose.Invoke(request, VisaResponseModel);
                await App.Current!.MainPage!.Navigation.PopAsync();

                UserDialogs.Instance.HideHud();
                IsBusy = true;
            }
            
        }
        [RelayCommand]
        async Task BackCLicked()
        {
            await App.Current!.MainPage!.Navigation.PopAsync();
        } 
        #endregion
    }
}
