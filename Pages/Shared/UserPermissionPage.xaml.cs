namespace TripBliss.Pages.Shared;

public partial class UserPermissionPage : Controls.CustomControl
{
	public UserPermissionPage()
	{
		InitializeComponent();
	}

    private void TapGestureRecognizer_UserPremision(object sender, TappedEventArgs e)
    {
        if (UserPremision.IsVisible == false)
        {
            UserPremision.IsVisible = true;
        }
        else
        {
            UserPremision.IsVisible = false;
        }
    }

    private void TapGestureRecognizer_RequestPremision(object sender, TappedEventArgs e)
    {
        if (ColcRequestPremision.IsVisible == false)
        {
            ColcRequestPremision.IsVisible = true;
        }
        else
        {
            ColcRequestPremision.IsVisible = false;
        }
    }
}