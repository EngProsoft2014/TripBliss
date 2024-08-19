using TripBliss.Helpers;
using TripBliss.ViewModels.TravelAgenciesViewModels.CreateRequest;
namespace TripBliss.Pages.TravelAgenciesPages.RequestDetails;

public partial class TransportaionServicePage : Controls.CustomControl
{
	public TransportaionServicePage(Tr_C_TransportaionServiceViewModel model , IGenericRepository generic)
	{
		InitializeComponent();
		this.BindingContext = model;
	}
}