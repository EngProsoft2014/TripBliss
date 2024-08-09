using TripBliss.Models;

namespace TripBliss.Pages.TravelAgenciesPages.Offers;

public partial class OfferDeteliesPage : ContentPage
{
	public OfferDeteliesPage(OfferModdel moddel)
	{
		InitializeComponent();
		Discraption.Text = moddel.Description;
		OfferTitel.Text = moddel.OfferTitel;
	}
}