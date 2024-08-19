using TripBliss.ViewModels.TravelAgenciesViewModels.CreateRequest;

namespace TripBliss.Pages.TravelAgenciesPages.CreateRequest;

public partial class NewRequestPage : Controls.CustomControl
{
	public NewRequestPage(NewRequestViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}