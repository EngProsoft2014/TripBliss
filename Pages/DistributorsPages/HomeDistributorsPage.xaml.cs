using TripBliss.Helpers;
using TripBliss.ViewModels.DistributorsViewModels;
using TripBliss.ViewModels.DistributorsViewModels.CreateResponse;
using TripBliss.ViewModels.DistributorsViewModels.Offer;

namespace TripBliss.Pages.DistributorsPages;

public partial class HomeDistributorsPage : Controls.CustomControl
{
    IGenericRepository Rep;
    readonly Services.Data.ServicesService _service;
    Dis_HomeViewModel Model;

    [Obsolete]
    public HomeDistributorsPage(Dis_HomeViewModel model,IGenericRepository generic, Services.Data.ServicesService service)
	{
		InitializeComponent();
        Rep = generic;
        _service = service; 
        BindingContext = model;  
        Model = model;
        AgencyView.BindingContext = new Dis_DistributorsViewModel(Rep,_service);
        OffersView.BindingContext = new Dis_O_ChooseOfferViewModel(Rep);
        HistoryView.BindingContext = new Dis_HistoryViewModel(Rep);
        MoreView.BindingContext = new Dis_MoreViewModel(Rep,_service);

        chkAll.IsChecked = true;
        chkAll.Color = Color.FromHex("#46b356");
    }

    [Obsolete]
    protected override bool OnBackButtonPressed()
    {
        // Run the async code on the UI thread
        Dispatcher.Dispatch(() =>
        {
            Action action = () => Application.Current!.Quit();
            Controls.StaticMember.ShowSnackBar("Do you want to exit the program?", Controls.StaticMember.SnackBarColor, Controls.StaticMember.SnackBarTextColor, action);
        });

        // Return true to prevent the default behavior
        return true;
    }

    [Obsolete]
    private void chkAll_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if(chkAll.IsChecked == true && chkActive.IsChecked == false && chkNotActive.IsChecked == false)
        {
            chkAll.Color = Color.FromHex("#46b356");
            chkActive.Color = Color.FromHex("#a1a1a1");
            chkActive.IsChecked = false;
            chkNotActive.Color = Color.FromHex("#a1a1a1");
            chkNotActive.IsChecked = false;
            
        }
        else
        {
            chkAll.IsChecked = false;
        }

        if(chkActive.IsChecked == true && chkAll.IsChecked == false && chkNotActive.IsChecked == false)
        {
            chkAll.Color = Color.FromHex("#a1a1a1");
            chkAll.IsChecked = false;
            chkActive.Color = Color.FromHex("#46b356");
            chkNotActive.Color = Color.FromHex("#a1a1a1");
            chkNotActive.IsChecked = false;
        }
        else
        {
            chkActive.IsChecked = false;
        }
        
        if(chkNotActive.IsChecked == true && chkAll.IsChecked == false && chkActive.IsChecked == false)
        {
            chkAll.Color = Color.FromHex("#a1a1a1");
            chkAll.IsChecked = false;
            chkActive.Color = Color.FromHex("#a1a1a1");
            chkActive.IsChecked = false;
            chkNotActive.Color = Color.FromHex("#46b356");
        }
        else
        {
            chkNotActive.IsChecked = false;
        }
    }



    [Obsolete]
    private void TapGestureRecognizer_Tapped_chkActive(object sender, TappedEventArgs e)
    {
        if (chkActive.IsChecked == false)
        {
            chkActive.IsChecked = true;
            chkAll.Color = Color.FromHex("#a1a1a1");
            chkAll.IsChecked = false;
            chkActive.Color = Color.FromHex("#46b356");
            chkNotActive.Color = Color.FromHex("#a1a1a1");
            chkNotActive.IsChecked = false;
            colRequests.ItemsSource = Model.Requests.Where(a=>a.TotalPriceAgentAccept >0);
        }
        else
        {
            if (chkAll.IsChecked == true || chkNotActive.IsChecked == true)
            {
                chkActive.IsChecked = false;
            }
            else
            {
                chkActive.IsChecked = true;
            }
        }
    }

    [Obsolete]
    private void TapGestureRecognizer_Tapped_chkNotActive(object sender, TappedEventArgs e)
    {
        if (chkNotActive.IsChecked == false)
        {
            chkNotActive.IsChecked = true;
            chkAll.Color = Color.FromHex("#a1a1a1");
            chkAll.IsChecked = false;
            chkActive.Color = Color.FromHex("#a1a1a1");
            chkActive.IsChecked = false;
            chkNotActive.Color = Color.FromHex("#46b356");
            colRequests.ItemsSource = Model.Requests.Where(a => a.TotalPriceAgentAccept == 0);
        }
        else
        {
            if(chkAll.IsChecked == true || chkActive.IsChecked == true)
            {
                chkNotActive.IsChecked = false;
            }
            else
            {
                chkNotActive.IsChecked = true;
            }
        }
    }

    [Obsolete]
    private void TapGestureRecognizer_Tapped_chkAll(object sender, TappedEventArgs e)
    {
        if (chkAll.IsChecked == false)
        {
            chkAll.IsChecked = true;
            chkAll.Color = Color.FromHex("#46b356");
            chkActive.Color = Color.FromHex("#a1a1a1");
            chkActive.IsChecked = false;
            chkNotActive.Color = Color.FromHex("#a1a1a1");
            chkNotActive.IsChecked = false;
            colRequests.ItemsSource = Model.Requests;
        }
        else
        {
            if (chkActive.IsChecked == true || chkNotActive.IsChecked == true)
            {
                chkAll.IsChecked = false;
            }
            else
            {
                chkAll.IsChecked = true;
            }
        }
    }
}