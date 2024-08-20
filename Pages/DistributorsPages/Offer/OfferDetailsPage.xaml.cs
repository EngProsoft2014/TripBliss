using TripBliss.Models;
using TripBliss.ViewModels.DistributorsViewModels.Offer;

namespace TripBliss.Pages.DistributorsPages.Offer;

public partial class OfferDetailsPage : Controls.CustomControl
{
	public OfferDetailsPage(Dis_O_OfferDetailsViewModel model)
	{
		InitializeComponent();
		BindingContext = model;	
		
	}
}