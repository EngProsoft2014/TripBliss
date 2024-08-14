using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Models;
using TripBliss.Pages.TravelAgenciesPages.Offer;

namespace TripBliss.ViewModels.TravelAgenciesViewModels.Offer
{
    partial class ChooseOfferViewModel : BaseViewModel
    {
        [ObservableProperty]
        public ObservableCollection<OfferModel> offers;
        [ObservableProperty]
        OfferModel selectedItem;

        public ChooseOfferViewModel()
        {
            Offers = new ObservableCollection<OfferModel>();
            Lang = Preferences.Default.Get("Lan", "en");

            if (Controls.StaticMember.WayOfTab == 2)
            {
                UserDialogs.Instance.ShowLoading();
                LoadData();
                UserDialogs.Instance.HideHud();
            }

        }

        void LoadData()
        {
            Offers.Add(new OfferModel()
            {
                OfferTitel = "Summer Beach Getaway",
                PriceBeforeOffer = "1500",
                PriceAfterOffer = "1200",
                Description = "Enjoy a tropical beach vacation with 20% off. Includes flights and hotel stay."
            });

            Offers.Add(new OfferModel()
            {
                OfferTitel = "Winter Wonderland Tour",
                PriceBeforeOffer = "2500",
                PriceAfterOffer = "2000",
                Description = "Experience the magic of winter in Europe. 25% off on guided tours and accommodation."
            });

            Offers.Add(new OfferModel()
            {
                OfferTitel = "Black Friday Flight Deals",
                PriceBeforeOffer = "800",
                PriceAfterOffer = "600",
                Description = "Exclusive Black Friday discounts on international flights. Save up to 25%!"
            });

            Offers.Add(new OfferModel()
            {
                OfferTitel = "New Year Cruise Special",
                PriceBeforeOffer = "3000",
                PriceAfterOffer = "2400",
                Description = "Celebrate the New Year on a luxury cruise. Get 20% off on bookings made before December 31."
            });

            Offers.Add(new OfferModel()
            {
                OfferTitel = "Back to School Travel Packages",
                PriceBeforeOffer = "1200",
                PriceAfterOffer = "1000",
                Description = "Special travel packages for students and families. Save 15% on educational tours."
            });

            Offers.Add(new OfferModel()
            {
                OfferTitel = "Holiday Adventure Deals",
                PriceBeforeOffer = "2000",
                PriceAfterOffer = "1600",
                Description = "Discover amazing destinations with our holiday adventure deals. 20% off on all bookings."
            });

        }
        [RelayCommand]
        async void SelectionOffer(OfferModel model)
        {
            var Vm = new OfferDetailsViewModel(model);
            var page = new OfferDetailsPage();
            page.BindingContext = Vm;   
            await App.Current.MainPage.Navigation.PushAsync(page);
        }

    }
}
