using CommunityToolkit.Maui.Core;
using Controls.UserDialogs.Maui;
using Microsoft.AspNet.SignalR.Client.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Constants;
using TripBliss.Helpers;
using TripBliss.Models;

namespace TripBliss.Controls
{
    static class StaticMember
    {
        #region Const Variables
        public static string SnackBarColor = "#187ad6";
        public static string SnackBarTextColor = "#FFFFFF";
        public static int WayOfTab { get; set; } = 0;
        public static int WayOfPhotosPopup { get; set; } = 0;
        #endregion

        #region Lists
        public static ObservableCollection<RequestClassModel> LstRequestClass { get; set; } = new ObservableCollection<RequestClassModel>();
        public static ObservableCollection<DistributorCompanyResponse> LstDistributorCompanys { get; set; } = new ObservableCollection<DistributorCompanyResponse>();
        public static ObservableCollection<OfferModel> LstOffers { get; set; } = new ObservableCollection<OfferModel>();
        public static ObservableCollection<RequestClassModel> LstHistories { get; set; } = new ObservableCollection<RequestClassModel>();
        #endregion

        #region SnackBar Setting
        [Obsolete]
        public static async void ShowSnackBar(string Message, string BKColor, string TextColor, Action action1)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            var snackbarOptions = new SnackbarOptions
            {
                BackgroundColor = Color.FromHex(BKColor),
                TextColor = Color.FromHex(TextColor),
                ActionButtonTextColor = Color.FromHex(TextColor),
                CornerRadius = new CornerRadius(10),
                Font = Microsoft.Maui.Font.SystemFontOfSize(14),
                ActionButtonFont = Microsoft.Maui.Font.SystemFontOfSize(14),
            };
            string text = Message;
            string actionButtonText = "Ok";
            Action action = action1;
            TimeSpan duration = TimeSpan.FromSeconds(3);

            var snackbar = CommunityToolkit.Maui.Alerts.Snackbar.Make(text, action, actionButtonText, duration, snackbarOptions);

            await snackbar.Show(cancellationTokenSource.Token);
        }
        #endregion

        #region Services
        static IGenericRepository? Rep;
        static Services.Data.ServicesService? _service;
        #endregion

        public static async Task Load_Tr_StartData(IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
            //UserDialogs.Instance.ShowLoading();
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

            //UserDialogs.Instance.HideHud();
        }

        public static double Clamp(this double self, double min, double max)
        {
            if (max < min)
            {
                return max;
            }
            else if (self < min)
            {
                return min;
            }
            else if (self > max)
            {
                return max;
            }

            return self;
        }
    }
}
