using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.ViewModels.TravelAgenciesViewModels.CreateRequest;
using TripBliss.ViewModels.TravelAgenciesViewModels.RequestDetails;
namespace TripBliss.Pages.TravelAgenciesPages.RequestDetails;

public partial class NewRequestPage : Controls.CustomControl
{
	public NewRequestPage(Tr_D_NewRequestViewModel model , IGenericRepository generic)
	{
		InitializeComponent();
        BindingContext = model;
	}

}