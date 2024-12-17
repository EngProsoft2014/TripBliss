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

            // Subscribe to page appearing
            Application.Current.PageAppearing += OnPageAppearing;

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

        //===================================================================

        private void OnPageAppearing(object sender, Page e)
        {
            if (e is ContentPage page && page.Content != null)
            {
                // Wrap ContentPage.Content in a Grid if needed
                if (page.Content is not Layout layout || layout.GestureRecognizers.Count == 0)
                {
                    var grid = new Grid();
                    grid.Children.Add(page.Content);
                    page.Content = grid;

                    // Add tap gesture to the Grid
                    var tapGesture = new TapGestureRecognizer();
                    tapGesture.Tapped += (s, args) => CloseKeyboard(page);
                    grid.GestureRecognizers.Add(tapGesture);
                }
            }
        }

        private void CloseKeyboard(ContentPage page)
        {
            // Find all focusable controls and unfocus them
            foreach (var control in FindByType<VisualElement>(page.Content))
            {
                if (control is Entry || control is Editor || control is SearchBar)
                {
                    control.Unfocus();
                }
            }
        }

        private IEnumerable<T> FindByType<T>(Element root) where T : Element
        {
            if (root is T match)
            {
                yield return match;
            }

            // Traverse through logical children
            foreach (var child in root.LogicalChildren)
            {
                foreach (var descendant in FindByType<T>(child))
                {
                    yield return descendant;
                }
            }
        }
    }
}
