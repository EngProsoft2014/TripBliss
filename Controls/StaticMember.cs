using Akavache;
using CommunityToolkit.Maui.Core;
using Controls.UserDialogs.Maui;
using Microsoft.AspNet.SignalR.Client.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Constants;
using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.Pages.Shared;
using TripBliss.Services.Data;
using TripBliss.ViewModels;

namespace TripBliss.Controls
{
    static class StaticMember
    {
        #region Const Variables
        public static string SnackBarColor = "#2fa562";
        public static string SnackBarTextColor = "#FFFFFF";
        public static int WayOfTab { get; set; } = 0;
        public static int WayOfPhotosPopup { get; set; } = 0;
        public static bool ShowSendOfferBtn { get; set; } = false;
        public static DateTime EndRequestStatic { get; set; }
        #endregion

        #region Lists
        public static ObservableCollection<OfferModel> LstOffers { get; set; } = new ObservableCollection<OfferModel>();
        #endregion


        #region SnackBar Setting
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
            string actionButtonText = TripBliss.Resources.Language.AppResources.OK;
            Action action = action1;
            TimeSpan duration = TimeSpan.FromSeconds(3);

            var snackbar = CommunityToolkit.Maui.Alerts.Snackbar.Make(text, action, actionButtonText, duration, snackbarOptions);

            await snackbar.Show(cancellationTokenSource.Token);
        }
        #endregion


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

        public async static Task ClearAllData(IGenericRepository generic)
        {
            ServicesService _service = new ServicesService(generic);

            await App.Current!.MainPage!.DisplayAlert(TripBliss.Resources.Language.AppResources.Warning, TripBliss.Resources.Language.AppResources.Found_Problem_Internal_Server, TripBliss.Resources.Language.AppResources.OK);

            string LangValueToKeep = Preferences.Default.Get("Lan", "en");
            Preferences.Default.Clear();
            await BlobCache.LocalMachine.InvalidateAll();
            await BlobCache.LocalMachine.Vacuum();
            Constants.Permissions.LstPermissions.Clear();
            Preferences.Default.Set("Lan", LangValueToKeep);
            await Application.Current!.MainPage!.Navigation.PushAsync(new LoginPage(new LoginViewModel(generic, _service)));
        }
    }
}
