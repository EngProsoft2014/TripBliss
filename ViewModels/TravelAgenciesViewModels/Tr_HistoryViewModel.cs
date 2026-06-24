using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using Plugin.FirebasePushNotifications;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Constants;
using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.Pages.Shared;
using TripBliss.Pages.TravelAgenciesPages.RequestDetails;

namespace TripBliss.ViewModels.TravelAgenciesViewModels
{
    partial class Tr_HistoryViewModel : BaseViewModel
    {
        #region Prop
        [ObservableProperty]
        public ObservableCollection<RequestTravelAgencyResponse> requests;
        #endregion

        #region Services
        readonly Services.Data.ServicesService _service;
        IGenericRepository Rep;
        readonly Services.Data.SignalRService _signalRService;
        readonly Services.Data.INotificationService _notificationService;
        readonly IFirebasePushNotification _firebasePushNotification;
        #endregion

        public Tr_HistoryViewModel(IGenericRepository generic, Services.Data.ServicesService service, Services.Data.SignalRService signalRService, Services.Data.INotificationService notificationService, IFirebasePushNotification firebasePushNotification)
        {
            Rep = generic;
            _service = service;
            _signalRService = signalRService;
            _notificationService = notificationService;
            _firebasePushNotification = firebasePushNotification;
            Requests = new ObservableCollection<RequestTravelAgencyResponse>();

            if (Controls.StaticMember.WayOfTab == 3)
            {
                Init();       
            }
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
                    string id = Preferences.Default.Get(ApiConstants.travelAgencyCompanyId, "");
                    string UserToken = await _service.UserToken();
                    if (!string.IsNullOrEmpty(UserToken))
                    {
                        UserDialogs.Instance.ShowLoading();
                        var json = await Rep.GetAsync<ObservableCollection<RequestTravelAgencyResponse>>($"TravelAgency/{id}/RequestTravelAgency/GetByTravelAgencyHistory", UserToken);
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

        #region RelayCommand
        [RelayCommand]
        async Task Selection(RequestTravelAgencyResponse model)
        {
            if (Constants.Permissions.CheckPermission(Constants.Permissions.Show_Request_Details_History))
            {
                await App.Current!.MainPage!.Navigation.PushAsync(new RequestDetailsPage(new RequestDetails.Tr_D_RequestDetailsViewModel(model.Id!, Rep, _service, _signalRService, _notificationService, _firebasePushNotification)));
            }
            else
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.PermissionAlert, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
        }
        #endregion

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
    }
}
