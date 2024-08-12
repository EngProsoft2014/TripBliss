using TripBliss.Pages;
using TripBliss.Constants;
using TripBliss.Pages.DistributorsPages;
namespace TripBliss
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(ApiConstants.syncFusionLicence);
            //MainPage = new AppShell();
            MainPage = new NavigationPage(new HomeDistributorsPage());
            //MainPage = new NavigationPage(new LoginPage());
           
        }
    }
}
