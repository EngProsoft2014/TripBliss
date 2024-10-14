using TripBliss.Models.ResponseWithDistributor;
using TripBliss.ViewModels.TravelAgenciesViewModels.RequestDetails;

namespace TripBliss.Pages.TravelAgenciesPages.RequestDetails;

public partial class Tr_ReviewPopup : Mopups.Pages.PopupPage
{
    public Tr_ReviewPopup(Tr_D_ReviewViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

}