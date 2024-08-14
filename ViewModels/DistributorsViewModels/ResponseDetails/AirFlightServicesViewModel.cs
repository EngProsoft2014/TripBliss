using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Models;

namespace TripBliss.ViewModels.DistributorsViewModels.ResponseDetails
{
    partial class AirFlightServicesViewModel : BaseViewModel
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

        public AirFlightServicesViewModel()
        {
            AirFlights = new ObservableCollection<AirFlightModel>();
            Moddel = new AirFlightModel();
        }
        public AirFlightServicesViewModel(AirFlightModel model)
        {
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
