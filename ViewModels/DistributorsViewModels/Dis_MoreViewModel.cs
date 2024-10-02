using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Helpers;
using TripBliss.Pages;
using TripBliss.Pages.DistributorsPages;
using TripBliss.Pages.MainPopups;
using TripBliss.Pages.Users;
using TripBliss.ViewModels.Users;

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
            var vm = new DocumentsViewModel(Rep, _service);
            var page = new DocumentsPage(vm);
            page.BindingContext = vm;
            await App.Current!.MainPage!.Navigation.PushAsync(page);
        }
        [RelayCommand]
        async Task UsersClick()
        {
            var vm = new UsersViewModel(Rep, _service);
            var page = new UsersPage(vm);
            page.BindingContext = vm;
            await App.Current!.MainPage!.Navigation.PushAsync(page);
        }

        [RelayCommand]
        async Task AccountClick()
        {
            var vm = new Dis_AccountViewModel(Rep, _service);
            var page = new Dis_AccountPage();
            page.BindingContext = vm;
            await App.Current!.MainPage!.Navigation.PushAsync(page);
        }

        [RelayCommand]
        [Obsolete]
        Task SelectExit()
        {
            Action action = async () =>
            {
                Preferences.Default.Clear();
                await Application.Current!.MainPage!.Navigation.PushAsync(new LoginPage(new LoginViewModel(Rep, _service)));
            };
            Controls.StaticMember.ShowSnackBar("Do you want to Logout?", Controls.StaticMember.SnackBarColor, Controls.StaticMember.SnackBarTextColor, action);
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
