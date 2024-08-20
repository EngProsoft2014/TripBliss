using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.Pages.DistributorsPages.Offer;

namespace TripBliss.ViewModels.DistributorsViewModels.Offer
{
    public partial class Dis_O_OfferDetailsViewModel : BaseViewModel
    {
        #region Prop
        [ObservableProperty]
        OfferModel selectedItem;
        #endregion

        IGenericRepository Rep;
        #region Cons
        public Dis_O_OfferDetailsViewModel(IGenericRepository generic)
        {
           Rep = generic;
        }

        public Dis_O_OfferDetailsViewModel(OfferModel model, IGenericRepository generic)
        {
            Rep = generic;
            SelectedItem = model;
            Lang = Preferences.Default.Get("Lan", "en");
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
            await App.Current.MainPage.Navigation.PushAsync(new OfferViewersPage(new Dis_O_OfferViewersViewModel(Rep)));
        }
        #endregion
    }
}
