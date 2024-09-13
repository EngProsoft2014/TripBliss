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

        #region Cons
        public Dis_HomeViewModel(IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
            Requests = new ObservableCollection<ResponseWithDistributorResponse>();
            Init();
        } 
        #endregion

        #region Methods
        async void Init()
        {
            await GetRequestes();
        }

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


        #endregion

        #region RelayCommand
        [RelayCommand]
        async Task Selection(ResponseWithDistributorResponse model)
        {
            await App.Current!.MainPage!.Navigation.PushAsync(new RequestDetailsPage(new Dis_D_RequestDetailsViewModel(model.Id, Rep, _service)));
        }
        #endregion
    }
}
