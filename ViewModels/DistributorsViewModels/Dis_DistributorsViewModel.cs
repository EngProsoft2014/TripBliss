using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using Microsoft.AspNet.SignalR.Client.Http;
using Syncfusion.Maui.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Constants;
using TripBliss.DataPaginated;
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
        [ObservableProperty]
        public ObservableCollection<TravelAgencyCompanyResponse> agenciesInPage = new ObservableCollection<TravelAgencyCompanyResponse>();
        public int PageNumber { get; set; }
        public bool IsHasNext { get; set; }
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
            PageNumber = 1;
            IsHasNext =true;
            await LoadAgency();
        }

        public async Task LoadAgency()
        {
            IsBusy = false;

            if (Constants.Permissions.CheckPermission(Constants.Permissions.DS_Show_Agencies))
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    string UserToken = await _service.UserToken();
                    if (!string.IsNullOrEmpty(UserToken))
                    {
                        UserDialogs.Instance.ShowLoading();
                        var json = await Rep.GetAsync<PagenationList<TravelAgencyCompanyResponse>>(ApiConstants.GetTravelCompanysApi + $"/{PageNumber}", UserToken);
                        UserDialogs.Instance.HideHud();
                        if (json != null)
                        {
                            PagenationList<TravelAgencyCompanyResponse> AgenciesPage = json;
                            
                            IsHasNext = AgenciesPage.HasNextPage;

                            AgenciesInPage = new ObservableCollection<TravelAgencyCompanyResponse>(AgenciesPage?.DataModel!);

                            if (CompanyResponses.Count == 0)
                            {
                                CompanyResponses = new ObservableCollection<TravelAgencyCompanyResponse>(AgenciesInPage.OrderBy(x => x.CompanyName).ToList());
                            }
                            else
                            {
                                if (CompanyResponses != AgenciesInPage)
                                {
                                    AgenciesInPage.ForEach(f => CompanyResponses.Add(f));
                                }
                            }

                            PageNumber += 1;
                        }

                        //var toast = Toast.Make(CompanyResponses.Count().ToString(), CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                        //await toast.Show();
                    }
                }

            }
            else
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.PermissionAlert, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            
            IsBusy = true;


            //if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            //{
            //    string UserToken = await _service.UserToken();
            //    if (!string.IsNullOrEmpty(UserToken))
            //    {
            //        UserDialogs.Instance.ShowLoading();
            //        var json = await Rep.GetAsync<ObservableCollection<TravelAgencyCompanyResponse>>(ApiConstants.GetTravelCompanysApi, UserToken);
            //        UserDialogs.Instance.HideHud();
            //        if (json != null)
            //        {
            //            CompanyResponses = json;
            //        }
            //    }
            //}
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

        [RelayCommand]
        async Task GetLoadMore()
        {
            if (IsHasNext)
            {
                await LoadAgency();
            }
        }
        #endregion


    }
}
