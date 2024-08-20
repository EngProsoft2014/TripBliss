using TripBliss.ViewModels.DistributorsViewModels.ResponseDetails;

namespace TripBliss.Pages.DistributorsPages.ResponseDetailes;

public partial class AirFlightServicePage : Controls.CustomControl
{
	public AirFlightServicePage(Dis_D_AirFlightServicesViewModel model)
	{
		InitializeComponent();
		BindingContext = model;
	}
}