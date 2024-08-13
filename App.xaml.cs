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
            string Lan = Preferences.Get("Lan","en");
            InitializeComponent();

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(ApiConstants.syncFusionLicence);
            //MainPage = new AppShell();
            MainPage = new NavigationPage(new HomeDistributorsPage());
            //MainPage = new NavigationPage(new LoginPage());
            if (Lan == "ar")
            {
                CultureInfo.CurrentCulture = new CultureInfo("ar");
            }
           
        }
    }
}
