using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TripBliss.Constants;
using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.Pages.DistributorsPages.ResponseDetailes;
using TripBliss.ViewModels.DistributorsViewModels.ResponseDetails;

namespace TripBliss.ViewModels.DistributorsViewModels
{
    public partial class Dis_HomeViewModel : BaseViewModel
    {
        #region Prop
        [ObservableProperty]
        public ObservableCollection<ResponseWithDistributorResponse> requests;
        #endregion

        #region Services
        readonly Services.Data.ServicesService _service;
        IGenericRepository Rep;
        #endregion

        public Dis_HomeViewModel(IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
            Requests = new ObservableCollection<ResponseWithDistributorResponse>();
            Init();
        }


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
                string id = Preferences.Default.Get(ApiConstants.distributorCompanyId, "");
                string UserToken = await _service.UserToken();
                if (!string.IsNullOrEmpty(UserToken))
                {
                    UserDialogs.Instance.ShowLoading();
                    var json = await Rep.GetAsync<ObservableCollection<ResponseWithDistributorResponse>>(ApiConstants.AllResponseDistApi + $"{id}/ResponseWithDistributor", UserToken);
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
        async Task Selection(RequestClassModel model)
        {
            await App.Current!.MainPage!.Navigation.PushAsync(new RequestDetailsPage(new Dis_D_RequestDetailsViewModel(Rep)));
        }
        #endregion
    }
}
