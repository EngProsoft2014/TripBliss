using TripBliss.ViewModels.DistributorsViewModels.Offer;

namespace TripBliss.Pages.DistributorsPages.Offer;

public partial class CreateOfferPage : Controls.CustomControl
{
	public CreateOfferPage(Dis_O_CreateOfferViewModel model)
	{
		InitializeComponent();
		BindingContext = model;
	}
}