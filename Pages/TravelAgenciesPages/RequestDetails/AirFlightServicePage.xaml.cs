using TripBliss.Helpers;
using TripBliss.ViewModels.TravelAgenciesViewModels.CreateRequest;
using TripBliss.ViewModels.TravelAgenciesViewModels.RequestDetails;
namespace TripBliss.Pages.TravelAgenciesPages.RequestDetails;

public partial class AirFlightServicePage : Controls.CustomControl
{
	public AirFlightServicePage(Tr_D_AirFlightServicesViewModel model , IGenericRepository generic)
	{
		InitializeComponent();
		BindingContext = model;

    }
}