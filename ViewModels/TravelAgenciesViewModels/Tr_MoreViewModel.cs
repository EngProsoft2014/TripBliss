using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
using TripBliss.Helpers;
using TripBliss.Pages;
using TripBliss.Pages.MainPopups;
using TripBliss.Pages.TravelAgenciesPages.Users;
using TripBliss.ViewModels.TravelAgenciesViewModels.Users;

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
        async Task DocumentClick()
        {
            var vm = new DocumentsViewModel(Rep,_service);
            var page = new DocumentsPage(vm);
            page.BindingContext = vm;
            await App.Current!.MainPage!.Navigation.PushAsync(page);
        }

        [RelayCommand]
        async Task UsersClick()
        {
            var vm = new Tr_UsersViewModel(Rep, _service);
            var page = new Tr_UsersPage(vm);
            page.BindingContext = vm;
            await App.Current!.MainPage!.Navigation.PushAsync(page);
        }
        #endregion


    }
}
