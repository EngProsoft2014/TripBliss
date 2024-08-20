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
        TransportaionServiceModel serviceModdel;
        [ObservableProperty]
        ObservableCollection<TransportaionServiceModel> transportation;
        #endregion

        IGenericRepository Rep;
        #region Const
        public Dis_D_TransportaionServiceViewModel(IGenericRepository generic)
        {
            Rep = generic;
            ServiceModdel = new TransportaionServiceModel();
            Transportation = new ObservableCollection<TransportaionServiceModel>();
        }
        public Dis_D_TransportaionServiceViewModel(TransportaionServiceModel model, IGenericRepository generic)
        {
            Rep = generic;
            ServiceModdel = new TransportaionServiceModel();
            Transportation = new ObservableCollection<TransportaionServiceModel>();
            ServiceModdel = model;
            Lang = Preferences.Default.Get("Lan", "en");
        } 
        #endregion

        #region RelayCommand
        [RelayCommand]
        void OnApply()
        {
            if (ServiceModdel != null)
            {
                Transportation.Add(ServiceModdel);
                App.Current.MainPage.Navigation.PopAsync();
            }
        }

        [RelayCommand]
        void OnBackButtonClicked()
        {
            App.Current.MainPage.Navigation.PopAsync();
        } 
        #endregion
    }
}
