
using TripBliss.ViewModels.TravelAgenciesViewModels;
using TripBliss.ViewModels.TravelAgenciesViewModels.CreateRequest;
using TripBliss.ViewModels.TravelAgenciesViewModels.Offer;

namespace TripBliss.Pages.TravelAgenciesPages;

public partial class HomeAgencyPage : Controls.CustomControl
{
    public HomeAgencyPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        tabMain.SelectedIndex = Controls.StaticMember.WayOfTab;
    }

    private async void tabMain_SelectionChanged(object sender, Syncfusion.Maui.TabView.TabSelectionChangedEventArgs e)
    {
        Controls.StaticMember.WayOfTab = (int)e.NewIndex;
        if ((int)e.NewIndex == 0)
        {
            conviewHome.BindingContext = new HomeViewModel();
        }
        if ((int)e.NewIndex == 1)
        {
            conviewDist.BindingContext = new TravelAgencyViewModel();
            conviewFevourites.BindingContext = new TravelAgencyViewModel();
        }
        if ((int)e.NewIndex == 2)
        {
            conviewOffers.BindingContext = new ChooseOfferViewModel();
        }
        if ((int)e.NewIndex == 3)
        {
            conviewHistory.BindingContext = new HistoryViewModel();
        }
        if ((int)e.NewIndex == 4)
        {
            conviewMore.BindingContext = new MoreViewModel();
        }
    }
}