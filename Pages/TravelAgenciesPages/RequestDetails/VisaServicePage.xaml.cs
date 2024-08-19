using TripBliss.Helpers;
using TripBliss.ViewModels.TravelAgenciesViewModels.CreateRequest;

namespace TripBliss.Pages.TravelAgenciesPages.RequestDetails;

public partial class VisaServicePage : Controls.CustomControl
{

	public VisaServicePage(Tr_C_VisaServiceViewModel model ,IGenericRepository generic)
	{
		InitializeComponent();
		this.BindingContext = model;
	}
}