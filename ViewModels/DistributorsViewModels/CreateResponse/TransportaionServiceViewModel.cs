using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Models;

namespace TripBliss.ViewModels.DistributorsViewModels.CreateResponse
{
    partial class TransportaionServiceViewModel : BaseViewModel
    {
        #region Prop
        [ObservableProperty]
        TransportaionServiceModel serviceModdel;
        [ObservableProperty]
        ObservableCollection<TransportaionServiceModel> transportation;
        #endregion

        #region Const
        public TransportaionServiceViewModel()
        {
            ServiceModdel = new TransportaionServiceModel();
            Transportation = new ObservableCollection<TransportaionServiceModel>();
        }
        public TransportaionServiceViewModel(TransportaionServiceModel model)
        {
            ServiceModdel = new TransportaionServiceModel();
            Transportation = new ObservableCollection<TransportaionServiceModel>();
            ServiceModdel = model;
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
