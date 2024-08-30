using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNet.SignalR.Client;
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
    public partial class Dis_D_TransportaionServiceViewModel : BaseViewModel
    {
        #region Prop
        [ObservableProperty]
        ResponseWithDistributorTransportResponse serviceModdel = new ResponseWithDistributorTransportResponse();

        #endregion

        IGenericRepository Rep;
        #region Const
        public Dis_D_TransportaionServiceViewModel(IGenericRepository generic)
        {
            Rep = generic;
        }
        public Dis_D_TransportaionServiceViewModel(ResponseWithDistributorTransportResponse model, IGenericRepository generic)
        {
            Rep = generic;
            ServiceModdel = model;
            Lang = Preferences.Default.Get("Lan", "en");
        } 
        #endregion

        #region RelayCommand
        [RelayCommand]
        async Task OnApply()
        {
            if (ServiceModdel != null)
            {
                //Transportation.Add(ServiceModdel);
                await App.Current!.MainPage!.Navigation.PopAsync();
            }
        }

        [RelayCommand]
        async Task OnBackButtonClicked()
        {
            await App.Current!.MainPage!.Navigation.PopAsync();
        } 
        #endregion
    }
}
