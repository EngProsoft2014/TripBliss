using Akavache;
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
using TripBliss.Pages.Shared;
using TripBliss.Pages.Users;
using TripBliss.ViewModels.Shared;

namespace TripBliss.ViewModels.Shared.UsersViewModels
{
    public partial class UsersViewModel : BaseViewModel
    {
        #region Prop
        [ObservableProperty]
        ObservableCollection<ApplicationUserResponse> lstUsers = new ObservableCollection<ApplicationUserResponse>(); 
        #endregion

        #region Services
        readonly Services.Data.ServicesService _service;
        IGenericRepository Rep;
        #endregion

        #region Cons
        public UsersViewModel(IGenericRepository generic, Services.Data.ServicesService service)
        {
            _service = service;
            Rep = generic;
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
        async Task AddUserClick()
        {
            if (Constants.Permissions.CheckPermission(Constants.Permissions.Add_User))
            {
                var vm = new AddUserViewModel(Rep, _service);
                vm.AddUserClose += async () =>
                {
                    await GetUsers();
                };
                var page = new AddUserPage(vm);
                page.BindingContext = vm;
                await App.Current!.MainPage!.Navigation.PushAsync(page);
            }
            else
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.PermissionAlert, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
        }
        [RelayCommand]
        async Task UserClick(ApplicationUserResponse model)
        {

            if (Constants.Permissions.CheckPermission(Constants.Permissions.Show_User_Details))
            {
                var vm = new UserPermissionViewModel(Rep, _service,model.Id);
                var page = new UserPermissionPage();
                page.BindingContext = vm;
                await App.Current!.MainPage!.Navigation.PushAsync(page);
            }
            else
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.PermissionAlert, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
        }

        [RelayCommand]
        async Task EnableOrDisableUser(ApplicationUserResponse model)
        {
            if (Constants.Permissions.CheckPermission(Constants.Permissions.Delete_User))
            {
                bool answer;
                if(model.IsDisabled == false)
                {
                    answer = await App.Current!.MainPage!.DisplayAlert(TripBliss.Resources.Language.AppResources.Question, TripBliss.Resources.Language.AppResources.Do_you_want_to_disable_user_account, TripBliss.Resources.Language.AppResources.Yes, TripBliss.Resources.Language.AppResources.No);
                }
                else
                {
                    answer = await App.Current!.MainPage!.DisplayAlert(TripBliss.Resources.Language.AppResources.Question, TripBliss.Resources.Language.AppResources.Do_you_want_to_enable_user_account, TripBliss.Resources.Language.AppResources.Yes, TripBliss.Resources.Language.AppResources.No);
                }

                if (answer)
                {
                    IsBusy = false;
                    UserDialogs.Instance.ShowLoading();
                    string UserToken = await _service.UserToken();
                    
                    var json = await Rep.PutAsync<string>(ApiConstants.PutUserAccountEnableOrDisable + model.Id, null, UserToken);

                    UserDialogs.Instance.HideHud();
                    IsBusy = true;

                    if (string.IsNullOrEmpty(json))
                    {
                        var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Successfully_action_completed, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                        await toast.Show();

                        await GetUsers();
                    }
                    else
                    {
                        var toast = Toast.Make(TripBliss.Resources.Language.AppResources.ErrorTryAgain, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                        await toast.Show();
                    }  
                }
            }
            else
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.PermissionAlert, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }

        }
        #endregion

        #region Methods
        async void Init()
        {
            await GetUsers();

        }

        async Task GetUsers()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();
                if (!string.IsNullOrEmpty(UserToken))
                {
                    string Id = Preferences.Default.Get(ApiConstants.travelAgencyCompanyId, "");
                    string uri = $"{ApiConstants.GetTravelUserApi}{Id}";
                    if (Id == "")
                    {
                        Id = Preferences.Default.Get(ApiConstants.distributorCompanyId, "");
                        uri = $"{ApiConstants.GetDistUserApi}{Id}";
                    }
                    UserDialogs.Instance.ShowLoading();
                    var json = await Rep.GetAsync<ObservableCollection<ApplicationUserResponse>>(uri, UserToken);
                    UserDialogs.Instance.HideHud();

                    if (json != null)
                    {
                        LstUsers = json;
                    }
                }
            }

        } 

        #endregion
    }

    
}
