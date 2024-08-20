using TripBliss.ViewModels.DistributorsViewModels.ResponseDetails;

namespace TripBliss.Pages.DistributorsPages.ResponseDetailes;

public partial class VisaServicePage : Controls.CustomControl
{
	public VisaServicePage(Dis_D_VisaServiceViewModel model)
	{
		InitializeComponent();
		BindingContext = model;
	}
}