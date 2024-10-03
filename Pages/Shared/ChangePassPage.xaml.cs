namespace TripBliss.Pages.Shared;

public partial class ChangePassPage : Controls.CustomControl
{
	public ChangePassPage()
	{
		InitializeComponent();
	}

    private void TapGestureRecognizer_Tapped_OldPassword(object sender, TappedEventArgs e)
    {
        entryOldPass.IsPassword = (entryOldPass.IsPassword == true) ? false : true;
    }

    private void TapGestureRecognizer_Tapped_NewPassword(object sender, TappedEventArgs e)
    {
        entryNewPass.IsPassword = (entryNewPass.IsPassword == true) ? false : true;
    }
}