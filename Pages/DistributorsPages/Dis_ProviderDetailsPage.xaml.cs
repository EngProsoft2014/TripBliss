namespace TripBliss.Pages.DistributorsPages;

public partial class Dis_ProviderDetailsPage : Controls.CustomControl
{
	public Dis_ProviderDetailsPage()
	{
		InitializeComponent();

        if(Controls.StaticMember.ShowSendOfferBtn == true)
        {
            btnShowSendOffer.IsVisible = true;
        }
        else
        {
            btnShowSendOffer.IsVisible = false;
        }
	}

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        Controls.StaticMember.ShowSendOfferBtn = false;
    }
}