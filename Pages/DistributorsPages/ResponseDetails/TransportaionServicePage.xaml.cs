using TripBliss.ViewModels.DistributorsViewModels.ResponseDetails;

namespace TripBliss.Pages.DistributorsPages.ResponseDetailes;

public partial class TransportaionServicePage : Controls.CustomControl
{
	public TransportaionServicePage(Dis_D_TransportaionServiceViewModel model)
	{
		InitializeComponent();
		BindingContext = model;
	}
}