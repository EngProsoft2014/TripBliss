using CommunityToolkit.Maui.Alerts;
using Controls.UserDialogs.Maui;
using Mopups.Services;
using Syncfusion.Maui.Core.Carousel;
using TripBliss.Constants;
using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.Services.Data;
using TripBliss.ViewModels.DistributorsViewModels.ResponseDetails;

namespace TripBliss.Pages.DistributorsPages.ResponseDetailes;

public partial class Dis_ComplaintPopup : Mopups.Pages.PopupPage
{

    public Dis_ComplaintPopup(Dis_D_PaymentViewModel viewModel)
    {
		InitializeComponent();

        this.BindingContext = viewModel;
    }


    private async void Button_Clicked(object sender, EventArgs e)
    {
		await MopupService.Instance.PopAsync();
    }

}