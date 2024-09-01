using TripBliss.Helpers;
using TripBliss.ViewModels.TravelAgenciesViewModels.CreateRequest;
using TripBliss.ViewModels.TravelAgenciesViewModels.RequestDetails;

namespace TripBliss.Pages.TravelAgenciesPages.RequestDetails;

public partial class VisaServicePage : Controls.CustomControl
{

	public VisaServicePage(Tr_D_VisaServiceViewModel model ,IGenericRepository generic)
	{
		InitializeComponent();
		this.BindingContext = model;
	}
}