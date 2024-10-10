using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using Controls.UserDialogs.Maui;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Helpers;
using TripBliss.Models;

namespace TripBliss.ViewModels.TravelAgenciesViewModels
{
    partial class Tr_HistoryViewModel : BaseViewModel
    {
        #region Prop
        [ObservableProperty]
        public ObservableCollection<RequestClassModel> requests;
        #endregion

        IGenericRepository Rep;
        public Tr_HistoryViewModel(IGenericRepository generic)
        {
            Rep = generic;
            Requests = new ObservableCollection<RequestClassModel>();
            Requests = Controls.StaticMember.LstHistories;

            if (Controls.StaticMember.WayOfTab == 3)
            {
                if (Constants.Permissions.CheckPermission(Constants.Permissions.Show_History))
                {
                    UserDialogs.Instance.ShowLoading();
                    LoadData();
                    UserDialogs.Instance.HideHud();
                }
                else
                {
                    var toast = Toast.Make("Permission not allowed for this action.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    toast.Show();
                }   
            }

        }



        #region Methods
        void LoadData()
        {
            
            Requests.Add(new RequestClassModel()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                RugestName = "Group 1",
                DistName = "Tark",
                Statues = "Active",
                Services = "Hotel - Tickting - Transportion"

            });
            Requests.Add(new RequestClassModel()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                RugestName = "Group 2",
                DistName = "Ali",
                Statues = "Not Active",
                Services = "Hotel - Tickting - Transportion"

            });
            Requests.Add(new RequestClassModel()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                RugestName = "Group 3",
                DistName = "Mohammed",
                Statues = "Not Active",
                Services = "Hotel - Tickting - Transportion"

            });
            Requests.Add(new RequestClassModel()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                RugestName = "Group 4",
                DistName = "Abdullah",
                Statues = "Active",
                Services = "Hotel - Tickting - Transportion"

            });
            Requests.Add(new RequestClassModel()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                RugestName = "Group 5",
                DistName = "Hassn",
                Statues = "Active",
                Services = "Hotel - Tickting - Transportion"

            });
            Requests.Add(new RequestClassModel()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                RugestName = "Group 6",
                DistName = "Omar",
                Statues = "Not Active",
                Services = "Hotel - Tickting - Transportion"

            });
            Requests.Add(new RequestClassModel()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                RugestName = "Group 7",
                DistName = "Tark",
                Statues = "Active",
                Services = "Hotel - Tickting - Transportion"

            });
            Requests.Add(new RequestClassModel()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                RugestName = "Group 1",
                DistName = "Tark",
                Statues = "Not Active",
                Services = "Hotel - Tickting - Transportion"

            });
            
        }
        #endregion
    }
}
