using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
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
using TripBliss.Pages;
using TripBliss.Pages.DistributorsPages.ResponseDetailes;
using TripBliss.Pages.TravelAgenciesPages.RequestDetails;
using TripBliss.ViewModels.ActivateViewModels;
using TripBliss.ViewModels.DistributorsViewModels.ResponseDetails;


namespace TripBliss.ViewModels.DistributorsViewModels.CreateResponse
{
    partial class Dis_DistributorsViewModel : BaseViewModel
    {
        [ObservableProperty]
        ObservableCollection<TravelAgencyCompanyResponse> companyResponses = new ObservableCollection<TravelAgencyCompanyResponse>();

        #region Services
        readonly Services.Data.ServicesService _service;
        IGenericRepository Rep;
        #endregion

        #region Cons
        public Dis_DistributorsViewModel(IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
            Init();
        } 
        #endregion

        async void Init()
        {
            if (Constants.Permissions.CheckPermission(Constants.Permissions.DS_Show_Agencies))
            {
                await LoadAgency();
            }
            else
            {
                var toast = Toast.Make("Permission not allowed for this action.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
        }

        async Task LoadAgency()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();
                if (!string.IsNullOrEmpty(UserToken))
                {
                    UserDialogs.Instance.ShowLoading();
                    var json = await Rep.GetAsync<ObservableCollection<TravelAgencyCompanyResponse>>(ApiConstants.GetTravelCompanysApi, UserToken);
                    UserDialogs.Instance.HideHud();
                    if (json != null)
                    {
                        CompanyResponses = json;
                    }
                }
            }
        }

        #region RelayCommand
        [RelayCommand]
        async Task OnBackPressed()
        {
            await App.Current!.MainPage!.Navigation.PopAsync();
        } 
        #endregion


    }
}
