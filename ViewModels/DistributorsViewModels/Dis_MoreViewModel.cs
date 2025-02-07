﻿using Akavache;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Helpers;
using TripBliss.Pages;
using TripBliss.Pages.DistributorsPages;
using TripBliss.Pages.MainPopups;
using TripBliss.Pages.Shared;
using TripBliss.Pages.Users;
using TripBliss.ViewModels.Shared.UsersViewModels;

namespace TripBliss.ViewModels.DistributorsViewModels
{
    partial class Dis_MoreViewModel : BaseViewModel
    {
        IGenericRepository Rep;
        readonly Services.Data.ServicesService _service;
        public Dis_MoreViewModel(IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
        }

        [RelayCommand]
        async Task SelectLanguage()
        {
            await MopupService.Instance.PushAsync(new LanguagePopup(Rep, _service));
        }
        [RelayCommand]
        async Task DocumentClick()
        {
            if (Constants.Permissions.CheckPermission(Constants.Permissions.Show_Documents))
            {
                var vm = new Dis_DocumentsViewModel(Rep, _service);
                var page = new Dis_DocumentsPage(vm);
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
        async Task AccountClick()
        {
            if (Constants.Permissions.CheckPermission(Constants.Permissions.ShowDistributorCompanyAccount))
            {
                var vm = new Dis_AccountViewModel(Rep, _service);
                var page = new Dis_AccountPage();
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
        Task SelectExit()
        {
            Action action = async () =>
            {
                string LangValueToKeep = Preferences.Default.Get("Lan", "en");
                Preferences.Default.Clear();
                await BlobCache.LocalMachine.InvalidateAll();
                await BlobCache.LocalMachine.Vacuum();
                Constants.Permissions.LstPermissions.Clear();

                Preferences.Default.Set("Lan", LangValueToKeep);
                await Application.Current!.MainPage!.Navigation.PushAsync(new LoginPage(new LoginViewModel(Rep, _service)));
            };
            Controls.StaticMember.ShowSnackBar(TripBliss.Resources.Language.AppResources.Do_you_want_to_Logout, Controls.StaticMember.SnackBarColor, Controls.StaticMember.SnackBarTextColor, action);
            return Task.CompletedTask;
        }

        [RelayCommand]
        async Task MyProfileClick()
        {
            var vm = new ProfileViewModel(Rep, _service);
            var page = new ProfilePage();
            page.BindingContext = vm;
            await App.Current!.MainPage!.Navigation.PushAsync(page);
        }
    }
}
