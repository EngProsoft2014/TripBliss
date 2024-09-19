using Mopups.Services;

namespace TripBliss.Pages.MainPopups;

public partial class FullScreenImagePopup : Mopups.Pages.PopupPage
{
	public FullScreenImagePopup()
	{
		InitializeComponent();
	}

    public FullScreenImagePopup( ImageSource sourceImage)
    {
        InitializeComponent();
        imgFullScreen.Source = sourceImage;
    }

    private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        await MopupService.Instance.PopAsync();
    }
}