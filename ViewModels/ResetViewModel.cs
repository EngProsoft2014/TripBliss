using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripBliss.ViewModels
{
    partial class ResetViewModel : BaseViewModel
    {

        [ObservableProperty]
        string email;

        [RelayCommand]
        void OnApply()
        {
            App.Current.MainPage.Navigation.PopAsync();
        }
    }
}
