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

namespace TripBliss.ViewModels.DistributorsViewModel.ResponseDeatiels
{
    partial class TransportaionServiceViewModdel : BaseViewModel
    {
        #region Prop
        [ObservableProperty]
        TransportaionServiceModdel serviceModdel;
        [ObservableProperty]
        ObservableCollection<TransportaionServiceModdel> transportation;
        #endregion

        #region Const
        public TransportaionServiceViewModdel()
        {
            ServiceModdel = new TransportaionServiceModdel();
            Transportation = new ObservableCollection<TransportaionServiceModdel>();
        }
        public TransportaionServiceViewModdel(TransportaionServiceModdel model)
        {
            ServiceModdel = new TransportaionServiceModdel();
            Transportation = new ObservableCollection<TransportaionServiceModdel>();
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
