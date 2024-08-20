using TripBliss.ViewModels.DistributorsViewModels.ResponseDetails;

namespace TripBliss.Pages.DistributorsPages.ResponseDetailes;

public partial class HotelServicePage : Controls.CustomControl
{
	public HotelServicePage(Dis_D_HotelServiceViewModel model)
	{
		InitializeComponent();
		BindingContext = model;	
	}
}