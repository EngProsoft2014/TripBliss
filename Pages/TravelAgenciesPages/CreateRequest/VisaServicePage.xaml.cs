using TripBliss.Helpers;
using TripBliss.ViewModels.TravelAgenciesViewModels.CreateRequest;

namespace TripBliss.Pages.TravelAgenciesPages.CreateRequest;

public partial class VisaServicePage : Controls.CustomControl
{
	public VisaServicePage(Tr_C_VisaServiceViewModel model, IGenericRepository generic)
	{
		InitializeComponent();
		BindingContext = model;	
	}
}