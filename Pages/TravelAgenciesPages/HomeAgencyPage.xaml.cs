
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
    public HomeAgencyPage(Tr_HomeViewModel viewModel,IGenericRepository generic, Services.Data.ServicesService service)
    {
        InitializeComponent();

        this.BindingContext = viewModel;
        Rep = generic;
        _service = service;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        tabMain.SelectedIndex = Controls.StaticMember.WayOfTab;
    }

    [Obsolete]
    protected override bool OnBackButtonPressed()
    {
        // Run the async code on the UI thread
        Dispatcher.Dispatch(() =>
        {
            Action action = () => Application.Current!.Quit();
            Controls.StaticMember.ShowSnackBar("Do you want to exit the program?", Controls.StaticMember.SnackBarColor, Controls.StaticMember.SnackBarTextColor, action);
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
            conviewHistory.BindingContext = new Tr_HistoryViewModel(Rep);
        }
        if ((int)e.NewIndex == 4)
        {
            conviewMore.BindingContext = new Tr_MoreViewModel(Rep, _service);
        }
    }
}