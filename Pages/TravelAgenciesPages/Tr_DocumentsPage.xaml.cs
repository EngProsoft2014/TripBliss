using TripBliss.ViewModels.TravelAgenciesViewModels;

namespace TripBliss.Pages.TravelAgenciesPages;

public partial class Tr_DocumentsPage : Controls.CustomControl
{
	public Tr_DocumentsPage(Tr_DocumentsViewModel model)
	{
		InitializeComponent();
		this.BindingContext = model;
	}
}