﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Models;

namespace TripBliss.ViewModels.DistributorsViewModel.Offer
{
    partial class OfferDetielesViewModdel : BaseViewModel
    {
        #region Prop
        [ObservableProperty]
        OfferModdel selectedItem; 
        #endregion

        #region Cons
        public OfferDetielesViewModdel()
        {

        }

        public OfferDetielesViewModdel(OfferModdel model)
        {
            SelectedItem = model;
        } 
        #endregion


        #region RelayCommand
        [RelayCommand]
        void BackClicked()
        {
            App.Current.MainPage.Navigation.PopAsync();
        }
        [RelayCommand]
        void AddRequest()
        {

        } 
        #endregion
    }
}
