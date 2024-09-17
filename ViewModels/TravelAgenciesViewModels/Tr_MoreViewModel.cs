using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Helpers;
using TripBliss.Pages;
using TripBliss.Pages.MainPopups;
using TripBliss.Pages.TravelAgenciesPages;
using TripBliss.Pages.TravelAgenciesPages.Guests;
using TripBliss.ViewModels.TravelAgenciesViewModels.Guests;

namespace TripBliss.ViewModels.TravelAgenciesViewModels
{
    partial class Tr_MoreViewModel : BaseViewModel
    {
        IGenericRepository Rep;
        readonly Services.Data.ServicesService _service;
        public Tr_MoreViewModel(IGenericRepository generic, Services.Data.ServicesService service)
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
        [Obsolete]
        Task SelectExit()
        {
            Action action = async() =>
            {
                Preferences.Default.Clear();
                await Application.Current!.MainPage!.Navigation.PushAsync(new LoginPage(new LoginViewModel(Rep, _service)));
            };
            Controls.StaticMember.ShowSnackBar("Do you want to Logout?", Controls.StaticMember.SnackBarColor, Controls.StaticMember.SnackBarTextColor, action);
            return Task.CompletedTask;
        }


        //[RelayCommand]
        //async Task GuestClick()
        //{
        //    var vm = new GuestsViewModel(Rep,_service);
            
        //    var page = new Tr_G_GuestsPage();
        //    page.BindingContext = vm;
        //    await App.Current!.MainPage!.Navigation.PushAsync(page);
        //}
    }
}
