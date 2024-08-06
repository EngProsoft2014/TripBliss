using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Models;

namespace TripBliss.ViewModels.TravelAgenciesVieModels.CreateRequest
{
    partial class VisaServiceViewModdel : BaseViewModel
    {
        #region Prop
        [ObservableProperty]
        VisaServiceModdel moddel;
        [ObservableProperty]
        ObservableCollection<VisaServiceModdel> visaServices;
        #endregion

        public VisaServiceViewModdel()
        {
            Moddel = new VisaServiceModdel();
            VisaServices = new ObservableCollection<VisaServiceModdel>();
        }

        [RelayCommand]
        void Apply()
        {
            VisaServices.Add(Moddel);
            App.Current.MainPage.Navigation.PopAsync();
        }
        [RelayCommand]
        void BackCLicked()
        {
            App.Current.MainPage.Navigation.PopAsync();
        }
    }
}
