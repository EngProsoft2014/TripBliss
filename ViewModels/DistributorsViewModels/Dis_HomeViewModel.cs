using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using Microsoft.AspNet.SignalR.Client.Http;
using Newtonsoft.Json;
using Syncfusion.Maui.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TripBliss.Constants;
using TripBliss.DataPaginated;
using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.Pages.DistributorsPages.ResponseDetailes;
using TripBliss.Pages.Shared;
using TripBliss.ViewModels.DistributorsViewModels.ResponseDetails;

namespace TripBliss.ViewModels.DistributorsViewModels
{
    public partial class Dis_HomeViewModel : BaseViewModel
    {
        #region Prop
        [ObservableProperty]
        public ObservableCollection<ResponseWithDistributorResponse> responses = new ObservableCollection<ResponseWithDistributorResponse>();
        [ObservableProperty]
        public ObservableCollection<ResponseWithDistributorResponse> responsesInPage = new ObservableCollection<ResponseWithDistributorResponse>();
        public int PageNumber { get; set; }
        public bool IsHasNext { get; set; }
        #endregion

        #region Services
        readonly Services.Data.ServicesService _service;
        IGenericRepository Rep;
        #endregion

        #region Cons
        public Dis_HomeViewModel(IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
            Init();
        } 
        #endregion

        #region Methods
        async void Init()
        {
            if (Constants.Permissions.LstPermissions.Count == 0)
            {
                await LoadPermissions();
            }

            PageNumber = 1;
            IsHasNext = true;
            await GetResponses();     
        }

        async Task LoadPermissions()
        {
            string List = Preferences.Default.Get(ApiConstants.permissions, "");
            if (!string.IsNullOrEmpty(List))
            {
                Constants.Permissions.LstPermissions = JsonConvert.DeserializeObject<List<PermissionsValues>>(List)!;
            }
        }

        public async Task GetResponses()
        {
            IsBusy = false;

            if (Constants.Permissions.CheckPermission(Constants.Permissions.Show_Home_Requests))
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    string id = Preferences.Default.Get(ApiConstants.distributorCompanyId, "");
                    string UserToken = await _service.UserToken();
                    if (!string.IsNullOrEmpty(UserToken))
                    {
                        var json = await Rep.GetAsync<PagenationList<ResponseWithDistributorResponse>>(ApiConstants.AllResponseDistApi + $"{id}/ResponseWithDistributor/PageNumber/{PageNumber}", UserToken);

                        if (json != null)
                        {
                            PagenationList<ResponseWithDistributorResponse> ResponesPage = json;
                            
                            IsHasNext = ResponesPage.HasNextPage;

                            ResponsesInPage = new ObservableCollection<ResponseWithDistributorResponse>(ResponesPage?.DataModel!);

                            if (Responses.Count == 0)
                            {
                                Responses = new ObservableCollection<ResponseWithDistributorResponse>(ResponsesInPage.ToList());
                            }
                            else
                            {
                                if (Responses != ResponsesInPage)
                                {
                                    ResponsesInPage.ForEach(f => Responses.Add(f));
                                }
                            }

                            PageNumber += 1;
                        }
                    }

                    //var toast = Toast.Make(Responses.Count().ToString(), CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    //await toast.Show();
                }

            }
            else
            {
                //var toast = Toast.Make(TripBliss.Resources.Language.AppResources.PermissionAlert, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                //await toast.Show();
            }

            IsBusy = true;

            //IsBusy = true;

            //if (Constants.Permissions.CheckPermission(Constants.Permissions.Show_Home_Requests))
            //{
            //    if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            //    {
            //        string id = Preferences.Default.Get(ApiConstants.distributorCompanyId, "");
            //        string UserToken = await _service.UserToken();
            //        if (!string.IsNullOrEmpty(UserToken))
            //        {
            //            UserDialogs.Instance.ShowLoading();
            //            var json = await Rep.GetAsync<ObservableCollection<ResponseWithDistributorResponse>>(ApiConstants.AllResponseDistApi + $"{id}/ResponseWithDistributor", UserToken);
            //            UserDialogs.Instance.HideHud();
            //            if (json != null)
            //            {
            //                Responses!.Clear();
            //                Responses = json;
            //            }
            //        }

            //    }
            //}
            //else
            //{
            //    //var toast = Toast.Make(TripBliss.Resources.Language.AppResources.PermissionAlert, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
            //    //await toast.Show();
            //}

            //IsBusy = false;
        }


        #endregion

        #region RelayCommand
        [RelayCommand]
        async Task Selection(ResponseWithDistributorResponse model)
        {
            if (Constants.Permissions.CheckPermission(Constants.Permissions.Show_Response))
            {
                await App.Current!.MainPage!.Navigation.PushAsync(new RequestDetailsPage(new Dis_D_RequestDetailsViewModel(model.Id, Rep, _service), new Dis_D_PaymentViewModel(model, Rep, _service), Rep, _service));
            }
            else
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.PermissionAlert, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
        }

        [RelayCommand]
        async Task GetLoadMore()
        {
            if (IsHasNext)
            {
                await GetResponses();
            }
        }
        #endregion
    }
}
