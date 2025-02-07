﻿
using Akavache;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Constants;
using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.Pages;
using TripBliss.Pages.MainPopups;
using TripBliss.Pages.Shared;

namespace TripBliss.ViewModels
{
    public partial class ProfileViewModel : BaseViewModel
    {
        [ObservableProperty]
        ApplicationUserProfileResponse account;
        #region Service
        readonly IGenericRepository Rep;
        readonly Services.Data.ServicesService _service;
        #endregion

        #region Cons
        public ProfileViewModel(IGenericRepository GenericRep, Services.Data.ServicesService service)
        {
            Rep = GenericRep;
            _service = service;
            Init();
        }
        #endregion

        #region RelayCommand
        [RelayCommand]
        async Task BackClicked()
        {
            await App.Current!.MainPage!.Navigation.PopAsync();
        }
        [RelayCommand]
        async Task ChangePassClick()
        {
            var vm = new ChangePassViewModel(Rep,_service);
            var page = new ChangePassPage();
            page.BindingContext = vm;
            await App.Current!.MainPage!.Navigation.PushAsync(page);
        }
        [RelayCommand]
        async Task DisableAccount()
        {
            bool answer = await App.Current!.MainPage!.DisplayAlert(TripBliss.Resources.Language.AppResources.Question, TripBliss.Resources.Language.AppResources.Do_you_Want_to_Delete_the_Account, TripBliss.Resources.Language.AppResources.Yes, TripBliss.Resources.Language.AppResources.No);
            if (answer) 
            {
                IsBusy = false;
                
                string UserToken = await _service.UserToken();

                string UserId = Preferences.Default.Get(ApiConstants.userid, "");

                UserDialogs.Instance.ShowLoading();
                var json = await Rep.PutAsync<string>(ApiConstants.PutUserAccountEnableOrDisable + UserId, null, UserToken);
                UserDialogs.Instance.HideHud();
                

                if (string.IsNullOrEmpty(json))
                {
                    var toast = Toast.Make(TripBliss.Resources.Language.AppResources.UserDeleteSuccess, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();

                    string LangValueToKeep = Preferences.Default.Get("Lan", "en");
                    Preferences.Default.Clear();
                    await BlobCache.LocalMachine.InvalidateAll();
                    await BlobCache.LocalMachine.Vacuum();
                    Constants.Permissions.LstPermissions.Clear();
                    Preferences.Default.Set("Lan", LangValueToKeep);
                    await Application.Current!.MainPage!.Navigation.PushAsync(new LoginPage(new LoginViewModel(Rep, _service)));
                }
                else
                {
                    var toast = Toast.Make(TripBliss.Resources.Language.AppResources.ErrorTryAgain, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }

                IsBusy = true;
            }

           
        }
        #endregion

        #region Methods
        async void Init()
        {
            await GetData();
        }
        async Task GetData()
        {
            IsBusy = false;
            UserDialogs.Instance.ShowLoading();
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string id = Preferences.Default.Get(ApiConstants.travelAgencyCompanyId, "");
                string UserToken = await _service.UserToken();
                if (!string.IsNullOrEmpty(UserToken))
                {

                    var json = await Rep.GetAsync<ApplicationUserProfileResponse>(ApiConstants.GetUserAccountApi, UserToken);

                    if (json != null)
                    {
                        Account = json;
                    }
                }
                UserDialogs.Instance.HideHud();
            }


            IsBusy = true;
        } 
        #endregion
    }
}
