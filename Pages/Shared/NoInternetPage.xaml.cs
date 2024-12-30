using Controls.UserDialogs.Maui;
using TripBliss.Constants;
using TripBliss.Helpers;
using TripBliss.ViewModels;

namespace TripBliss.Pages.Shared;

public partial class NoInternetPage : Controls.CustomControl
{

    #region Service
    readonly IGenericRepository Rep;
    readonly Services.Data.ServicesService _service;
    #endregion

    public NoInternetPage(IGenericRepository GenericRep, Services.Data.ServicesService service)
    {
        InitializeComponent();

        Rep = GenericRep;
        _service = service;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
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

    async void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
    {
        if (e.NetworkAccess != NetworkAccess.Internet)
        {
            // Connection to internet is Not available

        }
        else
        {
            // Connection to internet is available
            await GoAfterConnected();
        }
    }

    public async Task GoAfterConnected()
    {
        UserDialogs.Instance.ShowLoading();
        //await App.Current.MainPage.Navigation.PushAsync(Name);

        await App.Current!.MainPage!.Navigation.PopAsync();
        //App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 2]);

        UserDialogs.Instance.HideHud();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        if (Connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            // Connection to internet is Not available

        }
        else
        {
            // Connection to internet is available
            await GoAfterConnected();
        }
    }
}