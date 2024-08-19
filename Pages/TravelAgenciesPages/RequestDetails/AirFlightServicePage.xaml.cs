using TripBliss.Helpers;
using TripBliss.ViewModels.TravelAgenciesViewModels.CreateRequest;
namespace TripBliss.Pages.TravelAgenciesPages.RequestDetails;

public partial class AirFlightServicePage : Controls.CustomControl
{
	public AirFlightServicePage(Tr_C_AirFlightServicesViewModel model , IGenericRepository generic)
	{
		InitializeComponent();
		BindingContext = model;
	}
}