using TripBliss.ViewModels.ActivateViewModels;

namespace TripBliss.Pages.TravelAgenciesPages.ActivateDetailsPages;

public partial class AirFlightAttachmentsPage : Controls.CustomControl
{
    AirFlightActivateViewModel Model;
    public AirFlightAttachmentsPage(AirFlightActivateViewModel model)
    {
        InitializeComponent();
        this.BindingContext = model;
        Model = model;

        //Init();
    }

    //async void Init()
    //{

    //    if (Model.TOD == "T")
    //    {
    //        chkDist.IsChecked = true;
    //        chkTarvelAgency.IsChecked = false;
    //        TarvelAgency_Filter();
    //    }
    //    else
    //    {
    //        chkTarvelAgency.IsChecked = true;
    //        chkDist.IsChecked = false;
    //        Dist_Filter();
    //    }
    //}

    //private void TarvelAgency_Tapped(object sender, TappedEventArgs e)
    //{
    //    TarvelAgency_Filter();
    //}

    //private void Dist_Tapped(object sender, TappedEventArgs e)
    //{
    //    Dist_Filter();
    //}


    //void TarvelAgency_Filter()
    //{
    //    if (chkDist.IsChecked == true)
    //    {
    //        chkTarvelAgency.Color = Color.FromHex("#46b356");
    //        chkTarvelAgency.IsChecked = true;
    //        chkDist.Color = Color.FromHex("#a1a1a1");
    //        chkDist.IsChecked = false;
    //        lstPictures.ItemsSource = Model.LstAirFlightDetails.Where(a => !string.IsNullOrEmpty(a.TravelAgencyCompanyName) && string.IsNullOrEmpty(a.DistributorCompanyName));
    //    }
    //    else
    //    {
    //        if (chkDist.IsChecked == true)
    //        {
    //            chkTarvelAgency.IsChecked = false;
    //        }
    //        else
    //        {
    //            chkTarvelAgency.IsChecked = true;
    //        }
    //    }
    //}

    //void Dist_Filter()
    //{
    //    if (chkTarvelAgency.IsChecked == true)
    //    {
    //        chkDist.Color = Color.FromHex("#46b356");
    //        chkDist.IsChecked = true;
    //        chkTarvelAgency.Color = Color.FromHex("#a1a1a1");
    //        chkTarvelAgency.IsChecked = false;
    //        lstPictures.ItemsSource = Model.LstAirFlightDetails.Where(a => !string.IsNullOrEmpty(a.DistributorCompanyName) && string.IsNullOrEmpty(a.TravelAgencyCompanyName)).ToList();
    //    }
    //    else
    //    {
    //        if (chkTarvelAgency.IsChecked == true)
    //        {
    //            chkDist.IsChecked = false;
    //        }
    //        else
    //        {
    //            chkDist.IsChecked = true;
    //        }
    //    }
    //}
}