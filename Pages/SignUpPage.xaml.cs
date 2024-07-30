namespace TripBliss.Pages;

public partial class SignUpPage : ContentPage
{
	public SignUpPage()
	{
		InitializeComponent();
	}

    private async void OnSignIn(object sender, TappedEventArgs e)
    {
		await Navigation.PopAsync();
    }

    private async void QrCodeClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new QrCodePage());
    }
}