using Syncfusion.Maui.Core.Carousel;
using TripBliss.ViewModels.Shared.UsersViewModels;

namespace TripBliss.Pages.Users;

public partial class AddUserPage : Controls.CustomControl
{
	public AddUserPage(AddUserViewModel model)
	{
		InitializeComponent();
		this.BindingContext = model;

        entryEmail.Completed += (object sender, EventArgs e) =>
        {
            entryPassword.Focus();
        };
        entryPassword.Completed += (object sender, EventArgs e) =>
        {
           model.ClickRegisterCommand.Execute(model.AddModel);
        };
    }
    
    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        entryPassword.IsPassword = (entryPassword.IsPassword == true) ? false : true;
    }
}