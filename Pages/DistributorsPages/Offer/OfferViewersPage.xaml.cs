using TripBliss.ViewModels.DistributorsViewModels.Offer;

namespace TripBliss.Pages.DistributorsPages.Offer;

public partial class OfferViewersPage : Controls.CustomControl
{
	public OfferViewersPage(Dis_O_OfferViewersViewModel model)
	{
		InitializeComponent();
		BindingContext = model;
	}
}