using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Helpers;
using TripBliss.Models;

namespace TripBliss.ViewModels.TravelAgenciesViewModels.CreateRequest
{
    public partial class Tr_C_VisaServiceViewModel : BaseViewModel
    {
        #region Prop
        [ObservableProperty]
        VisaServiceModel moddel;
        [ObservableProperty]
        ObservableCollection<VisaServiceModel> visaServices;
        #endregion

        IGenericRepository Rep;
        public Tr_C_VisaServiceViewModel(IGenericRepository generic)
        {
            Rep = generic;
            Moddel = new VisaServiceModel();
            VisaServices = new ObservableCollection<VisaServiceModel>();
        }
        public Tr_C_VisaServiceViewModel(VisaServiceModel model, IGenericRepository generic)
        {
            Rep = generic;
            Moddel = new VisaServiceModel();
            VisaServices = new ObservableCollection<VisaServiceModel>();
            Moddel = model;
            Lang = Preferences.Default.Get("Lan", "en");
        }

        #region RelayCommand
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
        #endregion
    }
}
