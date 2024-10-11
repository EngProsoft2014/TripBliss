
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
using TripBliss.Models.Account;
using TripBliss.Pages;
using TripBliss.Pages.MainPopups;
using TripBliss.Pages.Shared;

namespace TripBliss.ViewModels
{
    public partial class ProfileViewModel : BaseViewModel
    {
        [ObservableProperty]
        AccountResponse account;
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
            bool answer = await App.Current!.MainPage!.DisplayAlert("Question?", "Do you Want to Delete the Account?", "Yes", "No");
            if (answer) 
            {
                IsBusy = false;
                UserDialogs.Instance.ShowLoading();

                string UserToken = await _service.UserToken();

                string UserId = Preferences.Default.Get(ApiConstants.userid, "");
                var json = await Rep.PutAsync<string>(ApiConstants.PutUserAccountEnableOrDisable + UserId, null, UserToken);

                if (string.IsNullOrEmpty(json))
                {
                    var toast = Toast.Make("Successfully, for user deleted.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();

                    Preferences.Default.Clear();
                    await BlobCache.LocalMachine.InvalidateAll();
                    await BlobCache.LocalMachine.Vacuum();
                    Constants.Permissions.LstPermissions.Clear();
                    await Application.Current!.MainPage!.Navigation.PushAsync(new LoginPage(new LoginViewModel(Rep, _service)));
                }
                else
                {
                    var toast = Toast.Make("Error Please Try again.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }

                UserDialogs.Instance.ShowLoading();
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

                    var json = await Rep.GetAsync<AccountResponse>(ApiConstants.GetUserAccountApi, UserToken);

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
