using TripBliss.Constants;
using TripBliss.Pages.DistributorsPages;
using System.Globalization;
using TripBliss.Helpers;
using TripBliss.Pages.TravelAgenciesPages;
using Akavache;
using TripBliss.Pages.Shared;

namespace TripBliss
{
    public partial class App : Application
    {
        #region Services
        IGenericRepository Rep;
        readonly Services.Data.ServicesService _service;
        #endregion


        [Obsolete]
        public App(IGenericRepository generic, Services.Data.ServicesService service)
        {
            _service = service;
            Rep = generic;

            BlobCache.ApplicationName = "TripBlissDB";
            BlobCache.EnsureInitialized();

            LoadSetting();
            InitializeComponent();

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(ApiConstants.syncFusionLicence);

            if (!string.IsNullOrEmpty(Preferences.Default.Get(ApiConstants.username, "")))
            {
                int CatUser = Preferences.Default.Get(ApiConstants.userCategory, 0);
                if (CatUser != 0)
                {
                    MainPage = CatUser switch
                    {
                        2 => new NavigationPage(new HomeAgencyPage(new ViewModels.TravelAgenciesViewModels.Tr_HomeViewModel(Rep, _service), Rep, _service)),
                        3 => new NavigationPage(new HomeDistributorsPage(new ViewModels.DistributorsViewModels.Dis_HomeViewModel(Rep, _service), Rep, _service)),
                        _ => new NavigationPage(new LoginPage(new ViewModels.LoginViewModel(Rep, _service)))
                    };
                }
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage(new ViewModels.LoginViewModel(Rep, _service)));
            }

            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        private async void Connectivity_ConnectivityChanged(object? sender, ConnectivityChangedEventArgs e)
        {
            if(e.NetworkAccess != NetworkAccess.Internet)
            {
                // Connection to internet is Not available
                await App.Current!.MainPage!.Navigation.PushAsync(new NoInternetPage(Rep, _service));
                return;
            }
        }

        protected async override void OnStart()
        {
            base.OnStart();
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                // Connection to internet is Not available
                await App.Current!.MainPage!.Navigation.PushAsync(new NoInternetPage(Rep, _service));
                return;
            }
        }

        protected async override void OnResume()
        {
            base.OnResume();
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                // Connection to internet is Not available
                await App.Current!.MainPage!.Navigation.PushAsync(new NoInternetPage(Rep, _service));
                return;
            }
        }

        void LoadSetting()
        {
            string Lan = Preferences.Default.Get("Lan", "en");
            if (Lan == "ar")
            {
                CultureInfo.CurrentCulture = new CultureInfo("ar");
                CultureInfo.CurrentUICulture = new CultureInfo("ar");
            }
            else
            {
                CultureInfo.CurrentCulture = new CultureInfo("en");
                CultureInfo.CurrentUICulture = new CultureInfo("en");
            }
        }

    }
}
