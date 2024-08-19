using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Helpers;
using TripBliss.Models;

namespace TripBliss.ViewModels.TravelAgenciesViewModels.CreateRequest
{
    public partial class AirFlightServicesViewModel : BaseViewModel
    {
        readonly IGenericRepository Rep;
        public AirFlightServicesViewModel(IGenericRepository GenericRep)
        {
            Rep = GenericRep;
            
        }
        public AirFlightServicesViewModel(AirFlightModel model, IGenericRepository GenericRep)
        {
            Rep = GenericRep;
            Moddel = model;
        }

        #region prop
        [ObservableProperty]
        ObservableCollection<AirFlightModel> airFlights = new ObservableCollection<AirFlightModel>();
        [ObservableProperty]
        AirFlightModel moddel = new AirFlightModel();
        [ObservableProperty]
        int adult;
        [ObservableProperty]
        int chiled;
        [ObservableProperty]
        int infant;

        #endregion

        async void Init()
        {
            await GetAllAirFlights();
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
