using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Models;

namespace TripBliss.ViewModels.TravelAgenciesViewModels.Offer
{
    partial class OfferDetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        OfferModel selectedItem;

        public OfferDetailsViewModel()
        {

        }

        public OfferDetailsViewModel(OfferModel model)
        {
            SelectedItem = model;
        }


        [RelayCommand]
        async void BackClicked()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
        [RelayCommand]
        void AddRequest()
        {

        }
    }
}
