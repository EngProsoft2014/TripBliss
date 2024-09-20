﻿using CommunityToolkit.Maui.Alerts;
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
using TripBliss.Pages.ActivateDetailsPages;
using TripBliss.Pages.TravelAgenciesPages.RequestDetails;
using TripBliss.ViewModels.ActivateViewModels;

namespace TripBliss.ViewModels.TravelAgenciesViewModels.RequestDetails
{
     public partial class Tr_D_VisaServiceViewModel : BaseViewModel
    {
        #region Prop
        [ObservableProperty]
        ResponseWithDistributorVisaResponse moddel = new ResponseWithDistributorVisaResponse();
        [ObservableProperty]
        RequestTravelAgencyVisaRequest? visaRequestModel = new RequestTravelAgencyVisaRequest();


        [ObservableProperty]
        VisaResponse selectedVisa = new VisaResponse();
        [ObservableProperty]
        ObservableCollection<VisaResponse> visas = new ObservableCollection<VisaResponse>();
        [ObservableProperty]
        public int totalPayment = 0;
        #endregion

        #region Services
        IGenericRepository Rep;
        readonly Services.Data.ServicesService _service;
        #endregion

        #region Cons
        public Tr_D_VisaServiceViewModel(IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
        }
        public Tr_D_VisaServiceViewModel(int payment,ResponseWithDistributorVisaResponse model, IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            Moddel = model;
            _service = service;
            TotalPayment = payment;
            if (model.AcceptAgen)
            {
                Init();
            }
            else
            {
                Init(model);
            }
        } 
        #endregion

        #region Methods
        async void Init(ResponseWithDistributorVisaResponse model)
        {
            UserDialogs.Instance.ShowLoading();
            await Task.WhenAll(GetVisas());
            UserDialogs.Instance.HideHud();

            VisaRequestModel = new RequestTravelAgencyVisaRequest
            {
                PersonCount = model.RequestTravelAgencyVisa.PersonCount,
                Notes = model.Notes,
            };
            SelectedVisa = Visas.FirstOrDefault(x => x.Id == model.RequestTravelAgencyVisa.VisaId)!;
        }

         void Init()
        {
            VisaRequestModel = new RequestTravelAgencyVisaRequest
            {
                PersonCount = Moddel.RequestTravelAgencyVisa.PersonCount,
                Notes = Moddel.Notes,
            };
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

        [RelayCommand]
        async Task ActiveClicked()
        {
            if (TotalPayment == 0)
            {
                var toast = Toast.Make("Please make sure to pay part of the amount due.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else
            {
                var vm = new VisaActivateViewModel(Moddel, Rep, _service);
                var page = new VisaAttachmentsPage(vm);
                page.BindingContext = vm;
                await App.Current!.MainPage!.Navigation.PushAsync(page);
            }

        }
        #endregion
    }
}