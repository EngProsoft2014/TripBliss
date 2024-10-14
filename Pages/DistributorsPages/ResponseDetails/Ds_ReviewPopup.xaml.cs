using TripBliss.Models.ResponseWithDistributor;
using TripBliss.ViewModels.DistributorsViewModels.ResponseDetails;


namespace TripBliss.Pages.DistributorsPages.ResponseDetailes;

public partial class Ds_ReviewPopup : Mopups.Pages.PopupPage
{
    public Ds_ReviewPopup(Dis_ReviewViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

}