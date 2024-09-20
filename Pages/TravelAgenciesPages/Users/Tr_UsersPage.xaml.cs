using TripBliss.ViewModels.TravelAgenciesViewModels.Users;

namespace TripBliss.Pages.TravelAgenciesPages.Users;

public partial class Tr_UsersPage : Controls.CustomControl
{
	public Tr_UsersPage(Tr_UsersViewModel model)
	{
		InitializeComponent();
		this.BindingContext = model;
	}
}