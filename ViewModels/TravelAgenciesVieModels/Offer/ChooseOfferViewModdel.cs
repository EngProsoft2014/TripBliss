using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Models;
using TripBliss.Pages.TravelAgenciesPages.Offers;

namespace TripBliss.ViewModels.TravelAgenciesVieModels.Offer
{
    partial class ChooseOfferViewModdel : BaseViewModel
    {
        public ObservableCollection<OfferModdel> offers { get; set; }
        [ObservableProperty]
        OfferModdel selectedItem;

        public ChooseOfferViewModdel()
        {
            offers = new ObservableCollection<OfferModdel>();
            LoadData();
        }

        void LoadData()
        {
            offers.Add(new OfferModdel()
            {
                OfferTitel = "Summer Beach Getaway",
                PriceBeforeOffer = "1500",
                PriceAfterOffer = "1200",
                Description = "Enjoy a tropical beach vacation with 20% off. Includes flights and hotel stay."
            });

            offers.Add(new OfferModdel()
            {
                OfferTitel = "Winter Wonderland Tour",
                PriceBeforeOffer = "2500",
                PriceAfterOffer = "2000",
                Description = "Experience the magic of winter in Europe. 25% off on guided tours and accommodation."
            });

            offers.Add(new OfferModdel()
            {
                OfferTitel = "Black Friday Flight Deals",
                PriceBeforeOffer = "800",
                PriceAfterOffer = "600",
                Description = "Exclusive Black Friday discounts on international flights. Save up to 25%!"
            });

            offers.Add(new OfferModdel()
            {
                OfferTitel = "New Year Cruise Special",
                PriceBeforeOffer = "3000",
                PriceAfterOffer = "2400",
                Description = "Celebrate the New Year on a luxury cruise. Get 20% off on bookings made before December 31."
            });

            offers.Add(new OfferModdel()
            {
                OfferTitel = "Back to School Travel Packages",
                PriceBeforeOffer = "1200",
                PriceAfterOffer = "1000",
                Description = "Special travel packages for students and families. Save 15% on educational tours."
            });

            offers.Add(new OfferModdel()
            {
                OfferTitel = "Holiday Adventure Deals",
                PriceBeforeOffer = "2000",
                PriceAfterOffer = "1600",
                Description = "Discover amazing destinations with our holiday adventure deals. 20% off on all bookings."
            });

        }
        [RelayCommand]
        async void SelectionOffer(OfferModdel model)
        {
            var Vm = new OfferDetielesViewModdel(SelectedItem);
            var page = new OfferDeteliesPage();
            page.BindingContext = Vm;   
            await App.Current.MainPage.Navigation.PushAsync(page);
        }

    }
}
