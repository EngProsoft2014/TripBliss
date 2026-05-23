using Akavache;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
using System.Reactive.Linq;
using TripBliss.Constants;
using TripBliss.Helpers;
using TripBliss.Pages;
using TripBliss.Pages.MainPopups;
using TripBliss.Pages.Shared;
using TripBliss.Pages.TravelAgenciesPages;
using TripBliss.Pages.Users;
using TripBliss.ViewModels.Shared.UsersViewModels;

namespace TripBliss.ViewModels.TravelAgenciesViewModels
{
    partial class Tr_MoreViewModel : BaseViewModel
    {
        #region Servises
        IGenericRepository Rep;
        readonly Services.Data.ServicesService _service;
        #endregion

        #region Cons
        public Tr_MoreViewModel(IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
        }
        #endregion

        #region RelayCommand

        [RelayCommand]
        async Task SelectLanguage()
        {
            await MopupService.Instance.PushAsync(new LanguagePopup(Rep, _service));
        }

        [RelayCommand]
        Task SelectExit()
        {
            Action action = async () =>
            {
                string LangValueToKeep = Preferences.Default.Get("Lan", "en");

                bool RememberMe = Preferences.Default.Get<bool>(ApiConstants.rememberMe, false);
                string RememberMeUserName = Preferences.Default.Get<string>(ApiConstants.rememberMeUserName, string.Empty);
                string RememberPassword = Preferences.Default.Get<string>(ApiConstants.rememberMePassword, string.Empty);

                Preferences.Default.Clear();
                await BlobCache.LocalMachine.InvalidateAll();
                await BlobCache.LocalMachine.Vacuum();
                Constants.Permissions.LstPermissions.Clear();

                Preferences.Default.Set("Lan", LangValueToKeep);
                Preferences.Default.Set(ApiConstants.rememberMe, RememberMe);
                Preferences.Default.Set(ApiConstants.rememberMeUserName, RememberMeUserName);
                Preferences.Default.Set(ApiConstants.rememberMePassword, RememberPassword);
                await Application.Current!.MainPage!.Navigation.PushAsync(new LoginPage(new LoginViewModel(Rep, _service)));
            };
            Controls.StaticMember.ShowSnackBar(TripBliss.Resources.Language.AppResources.Do_you_want_to_Logout, Controls.StaticMember.SnackBarColor, Controls.StaticMember.SnackBarTextColor, action);
            return Task.CompletedTask;
        }
        [RelayCommand]
        async Task DocumentClick()
        {
            if (Constants.Permissions.CheckPermission(Constants.Permissions.Show_Documents))
            {
                var vm = new Tr_DocumentsViewModel(Rep, _service);
                var page = new Tr_DocumentsPage(vm);
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
        async Task AccountClick()
        {
            string Result = "";
            if (TOD == "T")
            {
                Result = Constants.Permissions.ShowTravelAgencyCompanyAccount;
            }
            else
            {
                Result = Constants.Permissions.ShowDistributorCompanyAccount;
            }

            if (Constants.Permissions.CheckPermission(Result))
            {
                var vm = new Tr_AccountViewModel(Rep, _service);
                var page = new Tr_AccountPage();
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
        async Task UsersClick()
        {
            if (Constants.Permissions.CheckPermission(Constants.Permissions.Show_Users))
            {
                var vm = new UsersViewModel(Rep, _service);
                var page = new UsersPage(vm);
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
        async Task MyProfileClick()
        {
            var vm = new ProfileViewModel(Rep, _service);
            var page = new ProfilePage();
            page.BindingContext = vm;
            await App.Current!.MainPage!.Navigation.PushAsync(page);
        }
        #endregion


    }
}
