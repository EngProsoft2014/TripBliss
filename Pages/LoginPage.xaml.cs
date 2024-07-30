namespace TripBliss.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

    private async void OnForgot(object sender, TappedEventArgs e)
    {
       await Navigation.PushAsync(new ResetPage());
    }

    private async void OnSignUp(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new SignUpPage());
    }
}