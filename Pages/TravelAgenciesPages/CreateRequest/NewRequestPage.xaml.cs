using TripBliss.Helpers;
using TripBliss.ViewModels.TravelAgenciesViewModels.CreateRequest;

namespace TripBliss.Pages.TravelAgenciesPages.CreateRequest;

public partial class NewRequestPage : Controls.CustomControl
{
	public NewRequestPage(Tr_C_NewRequestViewModel model, IGenericRepository generic)
	{
		InitializeComponent();
		BindingContext = model;
	}
}