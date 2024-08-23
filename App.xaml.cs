using TripBliss.Pages;
using TripBliss.Constants;
using TripBliss.Pages.DistributorsPages;
using System.Globalization;
using Mopups.PreBaked.PopupPages.Login;
using TripBliss.Helpers;
using TripBliss.Pages.TravelAgenciesPages;

namespace TripBliss
{
    public partial class App : Application
    {
        #region Services
        IGenericRepository Rep;
        readonly Services.Data.ServicesService _service;
        #endregion

        public App(IGenericRepository generic, Services.Data.ServicesService service)
        {
            _service = service;
            Rep = generic;

            LoadSetting();
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(ApiConstants.syncFusionLicence);
            //MainPage = new AppShell();
            //MainPage = new NavigationPage(new HomeDistributorsPage(new Dis_HomeViewModel(Rep),Rep));
            //MainPage = new NavigationPage(new HomeAgencyPage(new ViewModels.TravelAgenciesViewModels.Tr_HomeViewModel(Rep,service), Rep,service));

            if (!string.IsNullOrEmpty(Preferences.Default.Get(ApiConstants.username, "")))
            {
                int CatUser = Preferences.Default.Get(ApiConstants.userCategory, 0);
                if (CatUser != 0)
                {
                    MainPage = CatUser switch
                    {
                        2 => new NavigationPage(new HomeAgencyPage(new ViewModels.TravelAgenciesViewModels.Tr_HomeViewModel(Rep, _service),Rep, _service)),
                        3 => new NavigationPage(new HomeDistributorsPage(new ViewModels.DistributorsViewModels.Dis_HomeViewModel(Rep), Rep, _service)),
                        _ => new NavigationPage(new LoginPage(new ViewModels.LoginViewModel(Rep, _service)))
                    };
                }
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage(new ViewModels.LoginViewModel(Rep, _service)));
            }
                

        }


        void LoadSetting()
        {
            string Lan = Preferences.Default.Get("Lan", "en");
            if (Lan == "ar")
            {
                CultureInfo.CurrentCulture = new CultureInfo("ar");
            }
            else
            {
                CultureInfo.CurrentCulture = new CultureInfo("en");
            }
        }

        protected override void OnStart()
        {
            base.OnStart();
        }
    }
}
