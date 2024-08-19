using TripBliss.Helpers;
using TripBliss.ViewModels.TravelAgenciesViewModels.CreateRequest;

namespace TripBliss.Pages.TravelAgenciesPages.CreateRequest;

public partial class TransportaionServicePage : Controls.CustomControl
{
	public TransportaionServicePage(Tr_C_TransportaionServiceViewModel model, IGenericRepository generic)
	{
		InitializeComponent();
		BindingContext = model;
	}
}