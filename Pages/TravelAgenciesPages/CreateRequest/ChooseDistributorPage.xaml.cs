
using TripBliss.Helpers;
using TripBliss.ViewModels.TravelAgenciesViewModels.CreateRequest;
namespace TripBliss.Pages.TravelAgenciesPages.CreateRequest;

public partial class ChooseDistributorPage : Controls.CustomControl
{
	
	public ChooseDistributorPage(Tr_C_TravelAgencyViewModel model , IGenericRepository generic)
	{
		InitializeComponent();
		BindingContext = model;
	}

}