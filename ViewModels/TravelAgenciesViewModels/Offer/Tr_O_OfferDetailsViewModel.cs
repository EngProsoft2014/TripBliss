﻿using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Helpers;
using TripBliss.Models;

namespace TripBliss.ViewModels.TravelAgenciesViewModels.Offer
{
    public partial class Tr_O_OfferDetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        OfferModel selectedItem;

        IGenericRepository Rep;
        public Tr_O_OfferDetailsViewModel(IGenericRepository generic)
        {
            Rep = generic;
        }

        public Tr_O_OfferDetailsViewModel(OfferModel model , IGenericRepository generic)
        {
            Rep = generic;
            Lang = Preferences.Default.Get("Lan", "en");
            SelectedItem = model;
        }


        [RelayCommand]
        async void BackClicked()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
        [RelayCommand]
        async void AddRequest()
        {
            if (Constants.Permissions.CheckPermission(Constants.Permissions.TR_Add_Request_from_offer))
            {

            }
            else
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.PermissionAlert, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
        }
    }
}
