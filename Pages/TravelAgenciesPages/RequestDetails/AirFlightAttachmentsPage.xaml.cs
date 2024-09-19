using TripBliss.ViewModels.ActivateViewModels;

namespace TripBliss.Pages.TravelAgenciesPages.RequestDetails;

public partial class AirFlightAttachmentsPage : Controls.CustomControl
{
	public AirFlightAttachmentsPage(AirFlightActivateViewModel model)
	{
		InitializeComponent();

		this.BindingContext = model;
	}
}