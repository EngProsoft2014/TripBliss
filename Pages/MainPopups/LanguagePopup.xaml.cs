
using Controls.UserDialogs.Maui;
using Mopups.Services;
using System.Globalization;
using TripBliss.Extensions;
using TripBliss.Helpers;
using TripBliss.Pages.DistributorsPages;
using TripBliss.Pages.TravelAgenciesPages;
using TripBliss.Resources.Language;

namespace TripBliss.Pages.MainPopups;

public partial class LanguagePopup : Mopups.Pages.PopupPage
{
    IGenericRepository Rep;
    readonly Services.Data.ServicesService _service;
    public LanguagePopup(IGenericRepository generic, Services.Data.ServicesService service)
	{
        InitializeComponent();
        Rep = generic;
        _service = service;
        LoadSetting();
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
		await MopupService.Instance.PopAsync();
    }


    //Arabic
    [Obsolete]
    private async void ArabicTap(object sender, TappedEventArgs e)
    {
        UserDialogs.Instance.ShowLoading();

        CultureInfo cal = new CultureInfo("ar");
        TranslateExtension.Instance.SetCulture(cal);
        Preferences.Default.Set("Lan", "ar");

        imgCheckArabic.IsVisible = true;
        imgCheckEnglish.IsVisible = false;

        LoadSetting();
        await MopupService.Instance.PopAsync();
        Controls.StaticMember.WayOfTab = 0;
        await App.Current!.MainPage!.Navigation.PushAsync(new HomeAgencyPage(new ViewModels.TravelAgenciesViewModels.Tr_HomeViewModel(Rep, _service), Rep, _service));

        UserDialogs.Instance.HideHud();
    }

    //English
    [Obsolete]
    private async void EnglishTap(object sender, TappedEventArgs e)
    {
        UserDialogs.Instance.ShowLoading();

        CultureInfo cal = new CultureInfo("en");
        TranslateExtension.Instance.SetCulture(cal);
        Preferences.Default.Set("Lan", "en");

        imgCheckEnglish.IsVisible = true;
        imgCheckArabic.IsVisible = false;

        LoadSetting();
        await MopupService.Instance.PopAsync();
        Controls.StaticMember.WayOfTab = 0;
        await App.Current!.MainPage!.Navigation.PushAsync(new HomeAgencyPage(new ViewModels.TravelAgenciesViewModels.Tr_HomeViewModel(Rep, _service), Rep, _service));

        UserDialogs.Instance.HideHud();
    }

    [Obsolete]
    void LoadSetting()
    {
        string Lan = Preferences.Default.Get("Lan", "en");
        Color color = Color.FromHex("#008000");
        if (Lan == "ar")
        {
            lblArabic.TextColor = color;

            imgCheckArabic.IsVisible = true;
            imgCheckEnglish.IsVisible = false;

            lblEnglish.TextColor = Color.FromHex("#333");

            Task.Delay(1000);
        } else
        {
            lblEnglish.TextColor = color;

            imgCheckEnglish.IsVisible = true;
            imgCheckArabic.IsVisible = false;

            lblArabic.TextColor = Color.FromHex("#333");

            Task.Delay(1000);
        }
    }
}