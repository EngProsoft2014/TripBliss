using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Models;

namespace TripBliss.ViewModels.TravelAgenciesVieModels.Offer
{
    partial class OfferDetielesViewModdel : BaseViewModel
    {
        [ObservableProperty]
        OfferModdel selectedItem;

        public OfferDetielesViewModdel()
        {

        }

        public OfferDetielesViewModdel(OfferModdel model)
        {
            SelectedItem = model;
        }


        [RelayCommand]
        void BackClicked()
        {
            App.Current.MainPage.Navigation.PopAsync();
        }
        [RelayCommand]
        void AddRequest()
        {

        }
    }
}
