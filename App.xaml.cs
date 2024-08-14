using TripBliss.Pages;
using TripBliss.Constants;
using TripBliss.Pages.DistributorsPages;
using System.Globalization;
namespace TripBliss
{
    public partial class App : Application
    {
        
        public App()
        {
            LoadSetting();
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(ApiConstants.syncFusionLicence);
            //MainPage = new AppShell();
            //MainPage = new NavigationPage(new HomeDistributorsPage());
            MainPage = new NavigationPage(new LoginPage());
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
    }
}
