using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.ViewModels.TravelAgenciesViewModels.Offer;
namespace TripBliss.Pages.TravelAgenciesPages.Offer;

public partial class OfferDetailsPage : Controls.CustomControl
{
	public OfferDetailsPage(Tr_O_OfferDetailsViewModel model , IGenericRepository generic)
	{
		InitializeComponent();
		BindingContext = model;
		
	}
}