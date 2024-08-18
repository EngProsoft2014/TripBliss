using TripBliss.ViewModels;

namespace TripBliss.Pages;

public partial class SignUpPage : Controls.CustomControl
{
	public SignUpPage(SignUpViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    private async void OnSignIn(object sender, TappedEventArgs e)
    {
		await Navigation.PopAsync();
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        entryPassword.IsPassword = (entryPassword.IsPassword == true) ? false : true;
    }
}