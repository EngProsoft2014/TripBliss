
using Mopups.Services;
using System.Globalization;
using TripBliss.Extensions;
using TripBliss.Pages.DistributorsPages;
using TripBliss.Resources.Language;

namespace TripBliss.Pages.MainPopups;

public partial class LanguagePopup : Mopups.Pages.PopupPage
{
	public LanguagePopup()
	{
        InitializeComponent();
        LoadSetting();
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
		await MopupService.Instance.PopAsync();
    }

    //Arabic
    private async void ArabicTap(object sender, TappedEventArgs e)
    {
        var cal = new CultureInfo("ar");
        LocalizationResourceManager.Instance.SetCulture(cal);
        Preferences.Default.Set("Lan","ar");

        LoadSetting();
        await MopupService.Instance.PopAsync();

    }

    private async void EnglishTap(object sender, TappedEventArgs e)
    {
        var cal = new CultureInfo("en");
        LocalizationResourceManager.Instance.SetCulture(cal);
        Preferences.Default.Set("Lan", "en");

        LoadSetting();
        await MopupService.Instance.PopAsync();
    }

    void LoadSetting()
    {
        var Lan = Preferences.Default.Get("Lan", "en");
        Color color = Color.FromHex("#008000");
        if (Lan == "ar")
        {
            lblArabic.TextColor = color;
            cheArabic.IsChecked = true;
        } else
        {
            lblEnglish.TextColor = color;
            cheEnglish.IsChecked = true;
        }
    }
}