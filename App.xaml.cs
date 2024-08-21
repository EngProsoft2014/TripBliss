using TripBliss.Pages;
using TripBliss.Constants;
using TripBliss.Pages.DistributorsPages;
using System.Globalization;
using Mopups.PreBaked.PopupPages.Login;
using TripBliss.Helpers;
using TripBliss.Pages.TravelAgenciesPages;
using TripBliss.ViewModels.DistributorsViewModels;
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
            MainPage = new NavigationPage(new HomeAgencyPage(new ViewModels.TravelAgenciesViewModels.Tr_HomeViewModel(Rep,service), Rep,service));
            //MainPage = new NavigationPage(new LoginPage(new ViewModels.LoginViewModel(Rep)));
            //MainPage = new NavigationPage(new LoginPage(new ViewModels.LoginViewModel(Rep)));
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

            Controls.StaticMember.LoadStartData(Rep,_service);
        }
    }
}
