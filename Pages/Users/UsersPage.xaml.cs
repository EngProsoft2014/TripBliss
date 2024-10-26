using TripBliss.ViewModels.Shared.UsersViewModels;

namespace TripBliss.Pages.Users;

public partial class UsersPage : Controls.CustomControl
{
	public UsersPage(UsersViewModel model)
	{
		InitializeComponent();
		this.BindingContext = model;
	}
}