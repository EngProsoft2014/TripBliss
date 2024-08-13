
using Mopups.Services;
using System.Globalization;
using TripBliss.Pages.DistributorsPages;
using TripBliss.Resources.Language;

namespace TripBliss.Pages.MainPopups;

public partial class LanguagePopup : Mopups.Pages.PopupPage
{
	public LanguagePopup()
	{
		InitializeComponent();
	}

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
		await MopupService.Instance.PopAsync();
    }

    //Arabic
    private async void TapGestureRecognizer_Tapped_1(object sender, TappedEventArgs e)
    {
        CultureInfo.CurrentCulture = new CultureInfo("ar");
        CultureInfo.CurrentUICulture = new CultureInfo("ar");
        Preferences.Set("Lan","ar");
        //AppResources.Culture = cal;

        await MopupService.Instance.PopAsync();

        App.Current!.MainPage = new HomeDistributorsPage();
    }
}