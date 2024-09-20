using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.ViewModels.TravelAgenciesViewModels.CreateRequest;
using TripBliss.ViewModels.TravelAgenciesViewModels.RequestDetails;
namespace TripBliss.Pages.TravelAgenciesPages.RequestDetails;

public partial class ConfirmResponsePage : Controls.CustomControl
{
	public ConfirmResponsePage(Tr_D_ConfirmResponsePageViewModel model , IGenericRepository generic)
	{
		InitializeComponent();
        BindingContext = model;
	}

}