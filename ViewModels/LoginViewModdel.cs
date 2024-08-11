using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Pages.TravelAgenciesPages;
using TripBliss.Pages.DistributorsPages;


namespace TripBliss.ViewModels
{
    partial class LoginViewModdel : BaseViewModel
    {
        #region Property
        [ObservableProperty]
        string email;
        [ObservableProperty]
        string password;
        #endregion


        #region RelayCommand
        [RelayCommand]
        async void OnLogIn()
        {
            IsBusy = true;
            if (Email == "d")
            {
                await App.Current.MainPage.Navigation.PushAsync(new HomeDistributorsPage());
            }
            else
            {
                await App.Current.MainPage.Navigation.PushAsync(new HomeAgencyPage());
            }
            
            IsBusy = false;

        }

        #endregion
    }
}
