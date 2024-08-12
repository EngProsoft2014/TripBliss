﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Models;

namespace TripBliss.ViewModels.DistributorsViewModels.Offer
{
    partial class CreateOfferViewModel : BaseViewModel
    {
        #region Prop
        [ObservableProperty]
        public OfferModel offer;
        #endregion

        public CreateOfferViewModel()
        {
            Offer = new OfferModel();
        }

        [RelayCommand]
        async Task BackPressed()
        {
            await App.Current!.MainPage!.Navigation.PopAsync();
        }
    }
}
