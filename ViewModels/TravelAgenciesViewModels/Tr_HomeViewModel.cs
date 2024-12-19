using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using Newtonsoft.Json;
using Syncfusion.Maui.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Constants;
using TripBliss.Controls;
using TripBliss.DataPaginated;
using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.Pages.Shared;
using TripBliss.Pages.TravelAgenciesPages.RequestDetails;

namespace TripBliss.ViewModels.TravelAgenciesViewModels
{
    public partial class Tr_HomeViewModel : BaseViewModel
    {
        #region Prop
        [ObservableProperty]
        public ObservableCollection<RequestTravelAgencyResponse> requests = new ObservableCollection<RequestTravelAgencyResponse>();
        [ObservableProperty]
        public ObservableCollection<RequestTravelAgencyResponse> requestsInPage = new ObservableCollection<RequestTravelAgencyResponse>();
        public int PageNumber { get; set; }
        public bool IsHasNext { get; set; }
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
            Init(); 
        } 
        #endregion


        public async void Init()
        {
            if (Constants.Permissions.LstPermissions.Count == 0)
            {       
                await LoadPermissions();
            }
            PageNumber = 1;
            IsHasNext = true;
            await GetRequestes();
        }

        async Task LoadPermissions()
        {
            string List = Preferences.Default.Get(ApiConstants.permissions, "");
            if (!string.IsNullOrEmpty(List))
            {
                Constants.Permissions.LstPermissions = JsonConvert.DeserializeObject<List<PermissionsValues>>(List)!;
            }
        }

        //async Task LoadPermissions(Services.Data.ServicesService service)
        //{
        //    string UserToken = await service.UserToken();

        //    if (!string.IsNullOrEmpty(UserToken))
        //    {
        //        Constants.Permissions.DecodeJwtToClass(UserToken);
        //    }
        //}

        #region Methods
        public async Task GetRequestes()
        {

            IsBusy = false;

            if (Constants.Permissions.CheckPermission(Constants.Permissions.Show_Home_Requests))
            {

                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    string id = Preferences.Default.Get(ApiConstants.travelAgencyCompanyId, "");
                    string UserToken = await _service.UserToken();
                    if (!string.IsNullOrEmpty(UserToken))
                    {
                        var json = await Rep.GetAsync<PagenationList<RequestTravelAgencyResponse>>(ApiConstants.AllRequestApi + $"{id}/RequestTravelAgency/pagenumber/{PageNumber}", UserToken);

                        if (json != null)
                        {
                            PagenationList<RequestTravelAgencyResponse> RequestsPage = json;

                            IsHasNext = RequestsPage.HasNextPage;

                            RequestsInPage = new ObservableCollection<RequestTravelAgencyResponse>(RequestsPage?.DataModel!);

                            if (Requests.Count == 0)
                            {
                                Requests = new ObservableCollection<RequestTravelAgencyResponse>(RequestsInPage.ToList());
                            }
                            else
                            {
                                if (Requests != RequestsInPage)
                                {
                                    RequestsInPage.ForEach(f => Requests.Add(f));
                                }
                            }
                            PageNumber += 1;      
                        }

                        //var toast = Toast.Make(Requests.Count().ToString(), CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                        //await toast.Show();
                    }

                }

            }
            else
            {
                //var toast = Toast.Make(Resources.Language.AppResources.PermissionAlert, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                //await toast.Show();
            }

            IsBusy = true;

            //IsBusy = true;

            //if (Constants.Permissions.CheckPermission(Constants.Permissions.Show_Home_Requests))
            //{

            //    if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            //    {
            //        string id = Preferences.Default.Get(ApiConstants.travelAgencyCompanyId, "");
            //        string UserToken = await _service.UserToken();
            //        if (!string.IsNullOrEmpty(UserToken))
            //        {
            //            UserDialogs.Instance.ShowLoading();
            //            var json = await Rep.GetAsync<ObservableCollection<RequestTravelAgencyResponse>>(ApiConstants.AllRequestApi + $"{id}/RequestTravelAgency", UserToken);
            //            UserDialogs.Instance.HideHud();
            //            if (json != null)
            //            {
            //                Requests!.Clear();
            //                Requests = json;
            //            }
            //        }

            //    }
            //}
            //else
            //{
            //    //var toast = Toast.Make(Resources.Language.AppResources.PermissionAlert, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
            //    //await toast.Show();
            //}

            //IsBusy = false;
        }


        #endregion

        #region RelayCommand
        [RelayCommand]
        async Task Selection(RequestTravelAgencyResponse model)
        {
            //UserDialogs.Instance.ShowLoading();
            await App.Current!.MainPage!.Navigation.PushAsync(new RequestDetailsPage(new RequestDetails.Tr_D_RequestDetailsViewModel(model.Id,Rep,_service)));
            //UserDialogs.Instance.HideHud();
        }


        [RelayCommand]
        async Task GetLoadMore()
        {
            if (IsHasNext)
            {
                await GetRequestes();
            }
        }
        #endregion

    }
}
