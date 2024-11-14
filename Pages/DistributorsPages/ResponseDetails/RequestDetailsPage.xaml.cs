using TripBliss.ViewModels.DistributorsViewModels.ResponseDetails;
using TripBliss.ViewModels.TravelAgenciesViewModels.RequestDetails;

namespace TripBliss.Pages.DistributorsPages.ResponseDetailes;

public partial class RequestDetailsPage : Controls.CustomControl
{
    Tr_D_PaymentViewModel Model;
    public RequestDetailsPage(Dis_D_RequestDetailsViewModel model)
	{
		InitializeComponent();
		BindingContext = model;
	}


    private async void FBCheck_CheckedChanged_1(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value && Model != null)
        {
            await Model.CalcOutPrice(0);
            AmmountEntry.Text = "";
        }
    }

    private void SfTabView_SelectionChanged(object sender, Syncfusion.Maui.TabView.TabSelectionChangedEventArgs e)
    {
        if (e.NewIndex == 2)
        {
            //PaymmentView.BindingContext = new Dis_D_PaymentViewModel();
        } 
    }
}