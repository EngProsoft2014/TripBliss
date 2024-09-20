using Syncfusion.Maui.Core.Carousel;
using TripBliss.ViewModels.TravelAgenciesViewModels.Users;

namespace TripBliss.Pages.TravelAgenciesPages.Users;

public partial class Tr_AddUserPage : Controls.CustomControl
{
	public Tr_AddUserPage(Tr_AddUserViewModel model)
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