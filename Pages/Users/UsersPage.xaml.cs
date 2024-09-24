using TripBliss.ViewModels.Users;

namespace TripBliss.Pages.Users;

public partial class UsersPage : Controls.CustomControl
{
	public UsersPage(UsersViewModel model)
	{
		InitializeComponent();
		this.BindingContext = model;
	}
}