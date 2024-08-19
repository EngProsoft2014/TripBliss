using TripBliss.Helpers;
using TripBliss.ViewModels.TravelAgenciesViewModels.CreateRequest;
namespace TripBliss.Pages.TravelAgenciesPages.CreateRequest;

public partial class HotelServicePage : Controls.CustomControl
{
	public HotelServicePage(Tr_C_HotelServiceViewModel model, IGenericRepository generic)
	{
		InitializeComponent();
		BindingContext = model;
	}
}