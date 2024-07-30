using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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


        #region Methodes
        [RelayCommand]
        void OnLogIn()
        {
            IsBusy = true;
            Shell.Current.DisplayAlert("click", Email, "cancle");
            IsBusy = false;

        }

        #endregion
    }
}
