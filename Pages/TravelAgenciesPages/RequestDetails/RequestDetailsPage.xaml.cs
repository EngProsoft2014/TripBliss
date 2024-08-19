using TripBliss.Helpers;
using TripBliss.ViewModels.TravelAgenciesViewModels.RequestDetails;
namespace TripBliss.Pages.TravelAgenciesPages.RequestDetails;

public partial class RequestDetailsPage : Controls.CustomControl
{
	public RequestDetailsPage(Tr_D_RequestDetailsViewModel model)
	{
		InitializeComponent();
		this.BindingContext = model;
	}
}