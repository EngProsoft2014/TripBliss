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

namespace TripBliss.ViewModels.DistributorsViewModels.ResponseDetails
{
    public partial class Dis_D_AirFlightServicesViewModel : BaseViewModel
    {
        #region prop
        [ObservableProperty]
        ObservableCollection<AirFlightModel> airFlights;
        [ObservableProperty]
        AirFlightModel moddel;
        [ObservableProperty]
        int adult;
        [ObservableProperty]
        int chiled;
        [ObservableProperty]
        int infant;

        #endregion

        IGenericRepository Rep;
        public Dis_D_AirFlightServicesViewModel(IGenericRepository generic)
        {
            Rep = generic;
            AirFlights = new ObservableCollection<AirFlightModel>();
            Moddel = new AirFlightModel();
        }
        public Dis_D_AirFlightServicesViewModel(AirFlightModel model, IGenericRepository generic)
        {
            Rep = generic;
            AirFlights = new ObservableCollection<AirFlightModel>();
            Lang = Preferences.Default.Get("Lan", "en");
            Moddel = new AirFlightModel();
            Moddel = model;
        }

        #region RelayCommand
        [RelayCommand]
        void OnBackPressed()
        {
            App.Current.MainPage.Navigation.PopAsync();
        }

        [RelayCommand]
        void AddAdult()
        {
            Adult += 1;
        }
        [RelayCommand]
        void DeletAdult()
        {
            Adult -= 1;
        }

        [RelayCommand]
        void AddChild()
        {
            Chiled += 1;
        }
        [RelayCommand]
        void DeletChild()
        {
            Chiled -= 1;
        }
        [RelayCommand]
        void AddInfant()
        {
            Infant += 1;
        }
        [RelayCommand]
        void DeletInfant()
        {
            Infant -= 1;
        }
        [RelayCommand]
        void AplyClicked()
        {
            AirFlights.Add(Moddel);
            App.Current.MainPage.Navigation.PopAsync();
        } 
        #endregion
    }
}
