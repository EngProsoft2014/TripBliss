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
        ResponseWithDistributorAirFlightResponse moddel = new ResponseWithDistributorAirFlightResponse();


        #endregion

        IGenericRepository Rep;
        public Dis_D_AirFlightServicesViewModel(IGenericRepository generic)
        {
            Rep = generic;

        }
        public Dis_D_AirFlightServicesViewModel(ResponseWithDistributorAirFlightResponse model, IGenericRepository generic)
        {
            Rep = generic;
            Moddel = model;
        }

        #region RelayCommand
        [RelayCommand]
        async Task OnBackPressed()
        {
            await App.Current!.MainPage!.Navigation.PopAsync();
        }
        [RelayCommand]
        async Task AplyClicked()
        {
            bool answer = await App.Current!.MainPage!.DisplayAlert("Question?", "Are You Accept This Price?", "Yes", "No");
            Moddel!.AcceptPriceDis = answer;
            await App.Current!.MainPage!.Navigation.PopAsync();
        } 
        #endregion
    }
}
