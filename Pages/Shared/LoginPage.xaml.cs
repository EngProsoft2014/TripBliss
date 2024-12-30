using TripBliss.Helpers;
using TripBliss.ViewModels;
using TripBliss.ViewModels.TravelAgenciesViewModels;

namespace TripBliss.Pages.Shared;

public partial class LoginPage : Controls.CustomControl
{
    LoginViewModel ViewModel { get => BindingContext as LoginViewModel; set => BindingContext = value; }

    public LoginPage(LoginViewModel vm)
	{
        InitializeComponent();
        
        BindingContext = vm;

        entryEmail.Completed += (object sender, EventArgs e) =>
        {
            entryPassword.Focus();
        };
        entryPassword.Completed += (object sender, EventArgs e) =>
        {
            ViewModel.ClickLoginCommand.Execute(ViewModel.LoginRequest);
        };
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
    }


    protected override bool OnBackButtonPressed()
    {
        // Run the async code on the UI thread
        Dispatcher.Dispatch(() =>
        {
            Action action = () => Application.Current!.Quit();
            Controls.StaticMember.ShowSnackBar(TripBliss.Resources.Language.AppResources.Do_you_want_to_exit_the_program, Controls.StaticMember.SnackBarColor, Controls.StaticMember.SnackBarTextColor, action);
        });

        // Return true to prevent the default behavior
        return true;
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        entryPassword.IsPassword = (entryPassword.IsPassword == true) ? false : true;
    }
}