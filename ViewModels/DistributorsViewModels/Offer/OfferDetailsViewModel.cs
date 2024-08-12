using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Models;
using TripBliss.Pages.DistributorsPages.Offer;

namespace TripBliss.ViewModels.DistributorsViewModels.Offer
{
    partial class OfferDetailsViewModel : BaseViewModel
    {
        #region Prop
        [ObservableProperty]
        OfferModel selectedItem; 
        #endregion

        #region Cons
        public OfferDetailsViewModel()
        {

        }

        public OfferDetailsViewModel(OfferModel model)
        {
            SelectedItem = model;
        } 
        #endregion


        #region RelayCommand
        [RelayCommand]
        async void BackClicked()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
        [RelayCommand]
        void AddRequest()
        {

        }

        [RelayCommand]
        async void GoToViewers()
        {
            await App.Current.MainPage.Navigation.PushAsync(new OfferViewersPage());
        }
        #endregion
    }
}
