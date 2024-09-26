using TripBliss.Models;
using TripBliss.ViewModels.ActivateViewModels;

namespace TripBliss.Pages.TravelAgenciesPages.RequestDetails;

public partial class VisaAttachmentsPage : Controls.CustomControl
{
	VisaActivateViewModel Model;
    public VisaAttachmentsPage(VisaActivateViewModel model)
	{
		InitializeComponent();
		this.BindingContext = model;	
		Model = model;
        if (Model.TOD == "T")
        {
            TarvelAgency_Filter();
        }
        else
        {
            Dist_Filter();
        }
    }

    private void TarvelAgency_Tapped(object sender, TappedEventArgs e)
    {
        TarvelAgency_Filter();
    }

    private void Dist_Tapped(object sender, TappedEventArgs e)
    {
        Dist_Filter();
    }


    void TarvelAgency_Filter()
    {
        if (chkDist.IsChecked == true)
        {
            chkTarvelAgency.Color = Color.FromHex("#46b356");
            chkTarvelAgency.IsChecked = true;
            chkDist.Color = Color.FromHex("#a1a1a1");
            chkDist.IsChecked = false;
            lstPictures.ItemsSource = Model.LstVisaDetails.Where(a => a.TravelAgencyCompanyName != null);
        }
        else
        {
            if (chkDist.IsChecked == true)
            {
                chkTarvelAgency.IsChecked = false;
            }
            else
            {
                chkTarvelAgency.IsChecked = true;
            }
        }
    }

    void Dist_Filter()
    {
        if (chkTarvelAgency.IsChecked == true)
        {
            chkDist.Color = Color.FromHex("#46b356");
            chkDist.IsChecked = true;
            chkTarvelAgency.Color = Color.FromHex("#a1a1a1");
            chkTarvelAgency.IsChecked = false;
            lstPictures.ItemsSource = Model.LstVisaDetails.Where(a => a.DistributorCompanyName != null).ToList();
        }
        else
        {
            if (chkTarvelAgency.IsChecked == true)
            {
                chkDist.IsChecked = false;
            }
            else
            {
                chkDist.IsChecked = true;
            }
        }
    }
}