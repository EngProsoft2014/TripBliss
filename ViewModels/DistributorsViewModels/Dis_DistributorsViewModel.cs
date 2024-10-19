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
using TripBliss.Pages.DistributorsPages;
using TripBliss.Pages.DistributorsPages.ResponseDetailes;
using TripBliss.Pages.Shared;
using TripBliss.Pages.TravelAgenciesPages.RequestDetails;
using TripBliss.ViewModels.ActivateViewModels;
using TripBliss.ViewModels.DistributorsViewModels.ResponseDetails;
using TripBliss.ViewModels.Shared;


namespace TripBliss.ViewModels.DistributorsViewModels.CreateResponse
{
    partial class Dis_DistributorsViewModel : BaseViewModel
    {
        #region Prop
        [ObservableProperty]
        ObservableCollection<TravelAgencyCompanyResponse> companyResponses = new ObservableCollection<TravelAgencyCompanyResponse>(); 
        #endregion

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

        #region Methods
        async void Init()
        {
            if (Constants.Permissions.CheckPermission(Constants.Permissions.DS_Show_Agencies))
            {
                await LoadAgency();
            }
            else
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.PermissionAlert, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
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
        #endregion

        #region RelayCommand
        [RelayCommand]
        async Task OnBackPressed()
        {
            await App.Current!.MainPage!.Navigation.PopAsync();
        }
        [RelayCommand]
        async Task Selection(TravelAgencyCompanyResponse model)
        {
            var vm = new Dis_ProviderDetailsViewModel(model!.Id!,Rep,_service);
            var page = new Dis_ProviderDetailsPage();
            page.BindingContext = vm;
            await App.Current!.MainPage!.Navigation.PushAsync(page);
        }
        #endregion


    }
}
