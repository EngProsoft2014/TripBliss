using Controls.UserDialogs.Maui;
using Microsoft.AspNet.SignalR.Client.Http;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Models;

namespace TripBliss.Controls
{
    static class StaticMember
    {
        public static int WayOfTab { get; set; } = 0;

        public static ObservableCollection<RequestClassModel> LstRequestClass { get; set; } = new ObservableCollection<RequestClassModel>();
        public static ObservableCollection<DistributorsModel> LstDistributors { get; set; } = new ObservableCollection<DistributorsModel>();
        public static ObservableCollection<OfferModel> LstOffers { get; set; } = new ObservableCollection<OfferModel>();
        public static ObservableCollection<RequestClassModel> LstHistories { get; set; } = new ObservableCollection<RequestClassModel>();

        public static void LoadStartData()
        {
            UserDialogs.Instance.ShowLoading();
            //first Tab
            LstRequestClass.Add(new RequestClassModel()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                RugestName = "Group 1",
                DistName = "Tark",
                Statues = "Active",
                Services = "Hotel - Tickting - Transportion"

            });
            LstRequestClass.Add(new RequestClassModel()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                RugestName = "Group 2",
                DistName = "Ali",
                Statues = "Not Active",
                Services = "Hotel - Tickting - Transportion"

            });
            LstRequestClass.Add(new RequestClassModel()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                RugestName = "Group 3",
                DistName = "Mohammed",
                Statues = "Not Active",
                Services = "Hotel - Tickting - Transportion"

            });
            LstRequestClass.Add(new RequestClassModel()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                RugestName = "Group 4",
                DistName = "Abdullah",
                Statues = "Active",
                Services = "Hotel - Tickting - Transportion"

            });
            LstRequestClass.Add(new RequestClassModel()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                RugestName = "Group 5",
                DistName = "Hassn",
                Statues = "Active",
                Services = "Hotel - Tickting - Transportion"

            });
            LstRequestClass.Add(new RequestClassModel()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                RugestName = "Group 6",
                DistName = "Omar",
                Statues = "Not Active",
                Services = "Hotel - Tickting - Transportion"

            });
            LstRequestClass.Add(new RequestClassModel()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                RugestName = "Group 7",
                DistName = "Tark",
                Statues = "Active",
                Services = "Hotel - Tickting - Transportion"

            });
            LstRequestClass.Add(new RequestClassModel()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                RugestName = "Group 1",
                DistName = "Tark",
                Statues = "Not Active",
                Services = "Hotel - Tickting - Transportion"

            });

            //scond Tab
            LstDistributors.Add(new DistributorsModel
            {
                Address = "Egypt",
                Name = "Akl Group",
                Phone = "+20155154110",
                Rate = "4.5",
                Services = "Hotel - Ticketing - Transportation"
            });
            LstDistributors.Add(new DistributorsModel
            {
                Address = "Saudi Arabia",
                Name = "Al Faisal Company",
                Phone = "+966123456789",
                Rate = "4.2",
                Services = "Hotel - Transportation"
            });
            LstDistributors.Add(new DistributorsModel
            {
                Address = "United Arab Emirates",
                Name = "Dubai Services",
                Phone = "+971987654321",
                Rate = "4.7",
                Services = "Hotel - Ticketing - Tours"
            });
            LstDistributors.Add(new DistributorsModel
            {
                Address = "Qatar",
                Name = "Qatar Hospitality",
                Phone = "+974654321987",
                Rate = "4.3",
                Services = "Hotel - Transportation - Ticketing"
            });
            LstDistributors.Add(new DistributorsModel
            {
                Address = "Kuwait",
                Name = "Kuwait Travels",
                Phone = "+965321654987",
                Rate = "4.6",
                Services = "Hotel - Ticketing - Transportation"
            });
            LstDistributors.Add(new DistributorsModel
            {
                Address = "Bahrain",
                Name = "Bahrain Tour Services",
                Phone = "+973789456123",
                Rate = "4.4",
                Services = "Hotel - Tours - Transportation"
            });
            LstDistributors.Add(new DistributorsModel
            {
                Address = "Oman",
                Name = "Oman Travel Agency",
                Phone = "+968456123789",
                Rate = "4.8",
                Services = "Hotel - Ticketing - Transportation"
            });

            //third Tab
            LstOffers.Add(new OfferModel()
            {
                OfferTitel = "Summer Beach Getaway",
                PriceBeforeOffer = "1500",
                PriceAfterOffer = "1200",
                Description = "Enjoy a tropical beach vacation with 20% off. Includes flights and hotel stay."
            });
            LstOffers.Add(new OfferModel()
            {
                OfferTitel = "Winter Wonderland Tour",
                PriceBeforeOffer = "2500",
                PriceAfterOffer = "2000",
                Description = "Experience the magic of winter in Europe. 25% off on guided tours and accommodation."
            });
            LstOffers.Add(new OfferModel()
            {
                OfferTitel = "Black Friday Flight Deals",
                PriceBeforeOffer = "800",
                PriceAfterOffer = "600",
                Description = "Exclusive Black Friday discounts on international flights. Save up to 25%!"
            });
            LstOffers.Add(new OfferModel()
            {
                OfferTitel = "New Year Cruise Special",
                PriceBeforeOffer = "3000",
                PriceAfterOffer = "2400",
                Description = "Celebrate the New Year on a luxury cruise. Get 20% off on bookings made before December 31."
            });
            LstOffers.Add(new OfferModel()
            {
                OfferTitel = "Back to School Travel Packages",
                PriceBeforeOffer = "1200",
                PriceAfterOffer = "1000",
                Description = "Special travel packages for students and families. Save 15% on educational tours."
            });
            LstOffers.Add(new OfferModel()
            {
                OfferTitel = "Holiday Adventure Deals",
                PriceBeforeOffer = "2000",
                PriceAfterOffer = "1600",
                Description = "Discover amazing destinations with our holiday adventure deals. 20% off on all bookings."
            });

            //fourth Tab
            LstHistories.Add(new RequestClassModel()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                RugestName = "Group 1",
                DistName = "Tark",
                Statues = "Active",
                Services = "Hotel - Tickting - Transportion"

            });
            LstHistories.Add(new RequestClassModel()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                RugestName = "Group 2",
                DistName = "Ali",
                Statues = "Not Active",
                Services = "Hotel - Tickting - Transportion"

            });
            LstHistories.Add(new RequestClassModel()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                RugestName = "Group 3",
                DistName = "Mohammed",
                Statues = "Not Active",
                Services = "Hotel - Tickting - Transportion"

            });
            LstHistories.Add(new RequestClassModel()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                RugestName = "Group 4",
                DistName = "Abdullah",
                Statues = "Active",
                Services = "Hotel - Tickting - Transportion"

            });
            LstHistories.Add(new RequestClassModel()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                RugestName = "Group 5",
                DistName = "Hassn",
                Statues = "Active",
                Services = "Hotel - Tickting - Transportion"

            });
            LstHistories.Add(new RequestClassModel()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                RugestName = "Group 6",
                DistName = "Omar",
                Statues = "Not Active",
                Services = "Hotel - Tickting - Transportion"

            });
            LstHistories.Add(new RequestClassModel()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                RugestName = "Group 7",
                DistName = "Tark",
                Statues = "Active",
                Services = "Hotel - Tickting - Transportion"

            });
            LstHistories.Add(new RequestClassModel()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                RugestName = "Group 1",
                DistName = "Tark",
                Statues = "Not Active",
                Services = "Hotel - Tickting - Transportion"

            });

            UserDialogs.Instance.HideHud();
        }
    }
}
