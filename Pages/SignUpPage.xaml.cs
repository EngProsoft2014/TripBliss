namespace TripBliss.Pages;

public partial class SignUpPage : Controls.CustomControl
{
	public SignUpPage()
	{
		InitializeComponent();
	}

    private async void OnSignIn(object sender, TappedEventArgs e)
    {
		await Navigation.PopAsync();
    }

}