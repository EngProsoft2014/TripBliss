using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using Microsoft.AspNet.SignalR.Client.Http;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Constants;
using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.Models.Premission;



namespace TripBliss.ViewModels.Shared
{

    public partial class UserPermissionViewModel : BaseViewModel
    {
        [ObservableProperty]
        ObservableCollection<PremissionResponse> userPermissions = new ObservableCollection<PremissionResponse>();

        [ObservableProperty]
        ObservableCollection<PremissionResponse> detailsRequest = new ObservableCollection<PremissionResponse>();
        [ObservableProperty]
        ObservableCollection<PremissionResponse> requestTravelAgency = new ObservableCollection<PremissionResponse>();
        [ObservableProperty]
        ObservableCollection<PremissionResponse> offer = new ObservableCollection<PremissionResponse>();
        [ObservableProperty]
        ObservableCollection<PremissionResponse> users = new ObservableCollection<PremissionResponse>();
        [ObservableProperty]
        ObservableCollection<PremissionResponse> documents = new ObservableCollection<PremissionResponse>();
        [ObservableProperty]
        ObservableCollection<PremissionResponse> history = new ObservableCollection<PremissionResponse>();
        [ObservableProperty]
        ObservableCollection<PremissionResponse> companyAccount = new ObservableCollection<PremissionResponse>();

        string UserId;
        #region Services
        readonly Services.Data.ServicesService _service;
        IGenericRepository Rep;
        #endregion

        #region Cons
        public UserPermissionViewModel(IGenericRepository generic, Services.Data.ServicesService service, string userId)
        {
            _service = service;
            Rep = generic;
            UserId = userId;
            Init();
        }
        #endregion

        #region RelayCommand
        [RelayCommand]
        async Task BackPressed()
        {
            await App.Current!.MainPage!.Navigation.PopAsync();
        }

        [RelayCommand]
        async Task ApplyClick()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {

                string UserToken = await _service.UserToken();
                UserDialogs.Instance.ShowLoading();
                var json = await Rep.PostTRAsync<ObservableCollection<PremissionResponse>, ObservableCollection<PremissionResponse>>(ApiConstants.UpdatePremissionListApi, UserPermissions, UserToken);
                UserDialogs.Instance.HideHud();

                if (json.Item1 != null)
                {
                    OrganizeLists(json.Item1);
                    var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Successfully_permissions_updated, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
                else
                {
                    var toast = Toast.Make($"{json.Item2!.errors!.FirstOrDefault().Value}", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
            }
        }
        #endregion

        #region Methods
        async void Init()
        {
            await GetPremissions();

        }

        async Task GetPremissions()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                UserDialogs.Instance.ShowLoading();
                string UserToken = await _service.UserToken();
                if (!string.IsNullOrEmpty(UserToken))
                {
                    string Id = Preferences.Default.Get(ApiConstants.travelAgencyCompanyId, "");
                    string uri = $"{ApiConstants.GetPremissionListApi}{UserId}";

                    var json = await Rep.GetAsync<ObservableCollection<PremissionResponse>>(uri, UserToken);

                    if (json != null)
                    {
                        OrganizeLists(json);
                    }
                }
                UserDialogs.Instance.HideHud();
            }
        }

        void OrganizeLists(ObservableCollection<PremissionResponse> Lst)
        {
            ClearLists();
            UserPermissions = Lst;
            foreach (var premm in Lst)
            {
                if (premm.categoryPermissions == "Details Request")
                {
                    DetailsRequest.Add(premm);
                }
                else if (premm.categoryPermissions == "Request Travel Agency")
                {
                    RequestTravelAgency.Add(premm);
                }
                else if (premm.categoryPermissions == "Offer")
                {
                    Offer.Add(premm);
                }
                else if (premm.categoryPermissions == "Users")
                {
                    Users.Add(premm);
                }
                else if (premm.categoryPermissions == "Documents")
                {
                    Documents.Add(premm);
                }
                else if (premm.categoryPermissions == "History")
                {
                    History.Add(premm);
                }
                else if (premm.categoryPermissions == "TravelAgencyCompany" || premm.categoryPermissions == "DistributorCompany")
                {
                    CompanyAccount.Add(premm);
                }
            }
        }

        void ClearLists()
        {
            UserPermissions.Clear();
            DetailsRequest.Clear();
            RequestTravelAgency.Clear();
            Offer.Clear();
            Users.Clear();
            Documents.Clear();
            History.Clear();
            CompanyAccount.Clear();
        }
        #endregion
    }
}
