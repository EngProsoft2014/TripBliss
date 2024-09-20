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
using TripBliss.Controls;
using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.Pages.TravelAgenciesPages.RequestDetails;

namespace TripBliss.ViewModels.TravelAgenciesViewModels
{
    public partial class Tr_HomeViewModel : BaseViewModel
    {
        #region Prop
        [ObservableProperty]
        public ObservableCollection<RequestTravelAgencyResponse> requests = new ObservableCollection<RequestTravelAgencyResponse>();
        #endregion

        #region Services
        readonly Services.Data.ServicesService _service;
        IGenericRepository Rep;
        #endregion

        #region Cons
        public Tr_HomeViewModel(IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
            //Init();
            
        } 
        #endregion


        async void Init()
        {           
            await GetRequestes();
        }

        #region Methods
        async Task GetRequestes()
        {
            IsBusy = true;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string id = Preferences.Default.Get(ApiConstants.travelAgencyCompanyId, "");
                string UserToken = await _service.UserToken();
                if (!string.IsNullOrEmpty(UserToken))
                {
                    UserDialogs.Instance.ShowLoading();
                    var json = await Rep.GetAsync<ObservableCollection<RequestTravelAgencyResponse>>(ApiConstants.AllRequestApi + $"{id}/RequestTravelAgency", UserToken);
                    UserDialogs.Instance.HideHud();
                    if (json != null)
                    {
                        Requests!.Clear();
                        Requests = json;
                    } 
                }
                
            }

            IsBusy = false;
        }


        #endregion

        #region RelayCommand
        [RelayCommand]
        async Task Selection(RequestTravelAgencyResponse model)
        {
            await App.Current!.MainPage!.Navigation.PushAsync(new RequestDetailsPage(new RequestDetails.Tr_D_RequestDetailsViewModel(model.Id,Rep,_service)));
        } 
        #endregion
    }
}
