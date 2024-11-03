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
using TripBliss.Pages.Shared;


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
            VisaRequestModel!.DateVisa = DateOnly.FromDateTime(DateTime.Now);
            VisaRequestModel!.DateVisaVM = DateTime.Now;
            Task.WhenAll(GetVisas());
        }
        public Tr_C_VisaServiceViewModel(RequestTravelAgencyVisaResponse model, IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
            VisaRequestModel!.DateVisa = DateOnly.FromDateTime(DateTime.Now);
            VisaRequestModel!.DateVisaVM = DateTime.Now;
            Init(model);


        }
        #endregion

        #region Methods
        async Task Init(RequestTravelAgencyVisaResponse model)
        {
            UserDialogs.Instance.ShowLoading();
            await Task.WhenAll(GetVisas());
            UserDialogs.Instance.HideHud();

            VisaRequestModel = new RequestTravelAgencyVisaRequest
            {
                PersonCount = model.PersonCount,
                DateVisa = model.DateVisa,
                DateVisaVM = model.DateVisa.ToDateTime(new TimeOnly(0, 0)),
                Notes = model.Notes,
            };
            SelectedVisa = Visas.FirstOrDefault(x => x.Id == model.VisaId)!;
        }

        async Task Init()
        {
            UserDialogs.Instance.ShowLoading();
            await Task.WhenAll(GetVisas());
            UserDialogs.Instance.HideHud();
        }

        async Task GetVisas()
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
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_SelectTypeofVisa, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (request.PersonCount == 0)
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_PassengersCount, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (request.DateVisaVM < DateTime.Now)
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_Visa_Date, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else
            {
                IsBusy = false;
                UserDialogs.Instance.ShowLoading();

                request.DateVisa = DateOnly.FromDateTime(request.DateVisaVM);

                VisaResponseModel!.VisaId = request.VisaId = SelectedVisa!.Id;
                VisaResponseModel!.VisaName = SelectedVisa.VisaName;
                VisaResponseModel!.VisaNameAr = SelectedVisa.VisaNameAr;
                VisaResponseModel!.PersonCount = request.PersonCount;
                VisaResponseModel!.DateVisa = request.DateVisa;
                VisaResponseModel!.Notes = request.Notes;
                Controls.StaticMember.EndRequestStatic = (request.DateVisa > DateOnly.FromDateTime(Controls.StaticMember.EndRequestStatic)) ? request.DateVisa.ToDateTime(new TimeOnly(0, 0)) : Controls.StaticMember.EndRequestStatic;

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
