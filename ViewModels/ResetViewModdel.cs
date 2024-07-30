using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripBliss.ViewModels
{
    partial class ResetViewModdel : BaseViewModel
    {

        [ObservableProperty]
        string email;

        [RelayCommand]
        void OnApply()
        {

        }
    }
}
