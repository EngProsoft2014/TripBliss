
using CommunityToolkit.Maui.Alerts;
using Microsoft.AspNet.SignalR.Client.Http;
using Syncfusion.Maui.Core.Carousel;
using System.Globalization;
using TripBliss.Helpers;
using TripBliss.ViewModels.TravelAgenciesViewModels;
using TripBliss.ViewModels.TravelAgenciesViewModels.CreateRequest;
using TripBliss.ViewModels.TravelAgenciesViewModels.Offer;

namespace TripBliss.Pages.TravelAgenciesPages;

public partial class HomeAgencyPage : Controls.CustomControl
{
    IGenericRepository Rep;
    readonly Services.Data.ServicesService _service;
    Tr_HomeViewModel ViewModel;
    public HomeAgencyPage(Tr_HomeViewModel viewModel,IGenericRepository generic, Services.Data.ServicesService service)
    {
        InitializeComponent();

        this.BindingContext = ViewModel = viewModel;
        Rep = generic;
        _service = service;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        tabMain.SelectedIndex = Controls.StaticMember.WayOfTab;

        if (tabMain.SelectedIndex == 0 && !Constants.Permissions.CheckPermission(Constants.Permissions.Show_Home_Requests))
        {
            var toast = Toast.Make(TripBliss.Resources.Language.AppResources.PermissionAlert, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
            await toast.Show();
        }
    }


    [Obsolete]
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

    private async void tabMain_SelectionChanged(object sender, Syncfusion.Maui.TabView.TabSelectionChangedEventArgs e)
    {
        Controls.StaticMember.WayOfTab = (int)e.NewIndex;
        if ((int)e.NewIndex == 0)
        {
            conviewHome.BindingContext = new Tr_HomeViewModel(Rep, _service);
            if (!Constants.Permissions.CheckPermission(Constants.Permissions.Show_Home_Requests))
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.PermissionAlert, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
        }
        if ((int)e.NewIndex == 1)
        {
            DisConView.BindingContext = new Tr_C_TravelAgencyViewModel(Rep, _service);
            //conviewDist.BindingContext = new Tr_C_TravelAgencyViewModel(Rep);
            //conviewFevourites.BindingContext = new Tr_C_TravelAgencyViewModel(Rep);
        }
        if ((int)e.NewIndex == 2)
        {
            conviewOffers.BindingContext = new Tr_O_ChooseOfferViewModel(Rep);
        }
        if ((int)e.NewIndex == 3)
        {
            conviewHistory.BindingContext = new Tr_HistoryViewModel(Rep, _service);
        }
        if ((int)e.NewIndex == 4)
        {
            conviewMore.BindingContext = new Tr_MoreViewModel(Rep, _service);
        }
    }
}