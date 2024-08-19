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
    public partial class Tr_C_AirFlightServicesViewModel : BaseViewModel
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
        public Tr_C_AirFlightServicesViewModel(IGenericRepository generic)
        {
            Rep = generic;
            AirFlights = new ObservableCollection<AirFlightModel>();
            Moddel = new AirFlightModel();
        }
        public Tr_C_AirFlightServicesViewModel( AirFlightModel model,IGenericRepository generic)
        {
            Rep = generic;
            AirFlights = new ObservableCollection<AirFlightModel>();
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


        [RelayCommand]
        async Task GetAllAirFlights()
        {
            if (Connectivity.NetworkAccess == Microsoft.Maui.Networking.NetworkAccess.Internet)
            {
                var json = await Rep.GetAsync<ObservableCollection<AirFlightModel>>(Constants.ApiConstants.GetAllAirFlightApi);

                if (json != null)
                {
                    AirFlights = new ObservableCollection<AirFlightModel>(json);
                }
            }
        }
        #endregion
    }
}
