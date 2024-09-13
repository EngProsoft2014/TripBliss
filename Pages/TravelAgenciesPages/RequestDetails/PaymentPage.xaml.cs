using TripBliss.ViewModels.TravelAgenciesViewModels.RequestDetails;

namespace TripBliss.Pages.TravelAgenciesPages.RequestDetails;

public partial class PaymentPage : Controls.CustomControl
{
    Tr_D_PaymentViewModel Model;
    public PaymentPage(Tr_D_PaymentViewModel model)
	{
		InitializeComponent();
        Model = model;
    }

    private async void AmmountEntry_TextChanged(object sender, TextChangedEventArgs e)
    {

        if (string.IsNullOrEmpty(e.NewTextValue))
        {
            await Model.CalcOutPrice(0);
        }
        else
        {
            await Model.CalcOutPrice(Convert.ToInt32(e.NewTextValue));
        }
    }


    private async void FBCheck_CheckedChanged_1(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value && Model != null)
        {
            await Model.CalcOutPrice(0);
            AmmountEntry.Text = "";
        }
    }
}