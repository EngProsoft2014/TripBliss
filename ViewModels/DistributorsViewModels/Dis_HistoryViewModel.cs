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
using TripBliss.Pages.DistributorsPages.ResponseDetailes;
using TripBliss.Pages.Shared;
using TripBliss.ViewModels.DistributorsViewModels.ResponseDetails;

namespace TripBliss.ViewModels.DistributorsViewModels
{
    partial class Dis_HistoryViewModel : BaseViewModel
    {
        #region Prop
        [ObservableProperty]
        public ObservableCollection<ResponseWithDistributorResponse> requests;
        #endregion

        #region Services
        readonly Services.Data.ServicesService _service;
        IGenericRepository Rep;
        #endregion

        public Dis_HistoryViewModel(IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
            Requests = new ObservableCollection<ResponseWithDistributorResponse>();
            Init();
        }


        #region Methods

        async void Init()
        {
            await GetRequestsHistory();
        }

        async Task GetRequestsHistory()
        {
            if (Constants.Permissions.CheckPermission(Constants.Permissions.Show_History))
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    string id = Preferences.Default.Get(ApiConstants.distributorCompanyId, "");
                    string UserToken = await _service.UserToken();
                    if (!string.IsNullOrEmpty(UserToken))
                    {
                        UserDialogs.Instance.ShowLoading();
                        var json = await Rep.GetAsync<ObservableCollection<ResponseWithDistributorResponse>>($"Distributor/{id}/ResponseWithDistributor/GetByDistributorHistory", UserToken);
                        UserDialogs.Instance.HideHud();
                        if (json != null)
                        {
                            Requests!.Clear();
                            Requests = json;
                        }
                    }
                }

            }
            else
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.PermissionAlert, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
        }

        //void LoadData()
        //{
        //    Requests.Add(new RequestClassModel()
        //    {
        //        Date = DateOnly.FromDateTime(DateTime.Now),
        //        RugestName = "Group 1",
        //        DistName = "Tark",
        //        Statues = "Active",
        //        Services = "Hotel - Tickting - Transportion"

        //    });
        //    Requests.Add(new RequestClassModel()
        //    {
        //        Date = DateOnly.FromDateTime(DateTime.Now),
        //        RugestName = "Group 2",
        //        DistName = "Ali",
        //        Statues = "Not Active",
        //        Services = "Hotel - Tickting - Transportion"

        //    });
        //    Requests.Add(new RequestClassModel()
        //    {
        //        Date = DateOnly.FromDateTime(DateTime.Now),
        //        RugestName = "Group 3",
        //        DistName = "Mohammed",
        //        Statues = "Not Active",
        //        Services = "Hotel - Tickting - Transportion"

        //    });
        //    Requests.Add(new RequestClassModel()
        //    {
        //        Date = DateOnly.FromDateTime(DateTime.Now),
        //        RugestName = "Group 4",
        //        DistName = "Abdullah",
        //        Statues = "Active",
        //        Services = "Hotel - Tickting - Transportion"

        //    });
        //    Requests.Add(new RequestClassModel()
        //    {
        //        Date = DateOnly.FromDateTime(DateTime.Now),
        //        RugestName = "Group 5",
        //        DistName = "Hassn",
        //        Statues = "Active",
        //        Services = "Hotel - Tickting - Transportion"

        //    });
        //    Requests.Add(new RequestClassModel()
        //    {
        //        Date = DateOnly.FromDateTime(DateTime.Now),
        //        RugestName = "Group 6",
        //        DistName = "Omar",
        //        Statues = "Not Active",
        //        Services = "Hotel - Tickting - Transportion"

        //    });
        //    Requests.Add(new RequestClassModel()
        //    {
        //        Date = DateOnly.FromDateTime(DateTime.Now),
        //        RugestName = "Group 7",
        //        DistName = "Tark",
        //        Statues = "Active",
        //        Services = "Hotel - Tickting - Transportion"

        //    });
        //    Requests.Add(new RequestClassModel()
        //    {
        //        Date = DateOnly.FromDateTime(DateTime.Now),
        //        RugestName = "Group 1",
        //        DistName = "Tark",
        //        Statues = "Not Active",
        //        Services = "Hotel - Tickting - Transportion"

        //    });

        //}
        #endregion


        #region RelayCommand
        [RelayCommand]
        async Task Selection(ResponseWithDistributorResponse model)
        {
            if (Constants.Permissions.CheckPermission(Constants.Permissions.Show_Response_Details_History))
            {
                await App.Current!.MainPage!.Navigation.PushAsync(new RequestDetailsPage(new Dis_D_RequestDetailsViewModel(model.Id!, Rep, _service)));
            }
            else
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.PermissionAlert, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
        } 
        #endregion
    }
}
