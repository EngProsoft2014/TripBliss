using TripBliss.Helpers;
using TripBliss.ViewModels.DistributorsViewModels;
using TripBliss.ViewModels.DistributorsViewModels.CreateResponse;
using TripBliss.ViewModels.DistributorsViewModels.Offer;

namespace TripBliss.Pages.DistributorsPages;

public partial class HomeDistributorsPage : Controls.CustomControl
{
    IGenericRepository Rep;
	public HomeDistributorsPage(Dis_HomeViewModel model,IGenericRepository generic)
	{
		InitializeComponent();
        Rep = generic;
        BindingContext = model;
        AgencyView.BindingContext = new Dis_DistributorsViewModel(Rep);
        OffersView.BindingContext = new Dis_O_ChooseOfferViewModel(Rep);
        HistoryView.BindingContext = new Dis_HistoryViewModel(Rep);
        MoreView.BindingContext = new Dis_MoreViewModel(Rep);
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
}