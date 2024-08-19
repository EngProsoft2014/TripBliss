using TripBliss.ViewModels.TravelAgenciesViewModels.CreateRequest;

namespace TripBliss.Pages.TravelAgenciesPages.CreateRequest;

public partial class AirFlightServicePage : Controls.CustomControl
{

	public AirFlightServicePage(Tr_C_AirFlightServicesViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}


