using TripBliss.ViewModels.DistributorsViewModels.ResponseDetails;

namespace TripBliss.Pages.DistributorsPages.ResponseDetailes;

public partial class RequestDetailsPage : Controls.CustomControl
{
	public RequestDetailsPage(Dis_D_RequestDetailsViewModel model)
	{
		InitializeComponent();
		BindingContext = model;
	}
}