using Mopups.Services;
using TripBliss.ViewModels.TravelAgenciesViewModels.RequestDetails;

namespace TripBliss.Pages.TravelAgenciesPages.RequestDetails;

public partial class Tr_ComplaintPopup : Mopups.Pages.PopupPage
{

    public Tr_ComplaintPopup(Tr_D_PaymentViewModel viewModel)
	{
		InitializeComponent();

        this.BindingContext = viewModel;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await MopupService.Instance.PopAsync();
    }
}