using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.ViewModels.TravelAgenciesViewModels;
using TripBliss.ViewModels.TravelAgenciesViewModels.RequestDetails;

namespace TripBliss.Pages.TravelAgenciesPages.RequestDetails;

public partial class PaymentPage : Controls.CustomControl
{
    IGenericRepository Rep;
    readonly Services.Data.ServicesService _service;
    ResponseWithDistributorResponse _distributorResponse;
    Tr_D_PaymentViewModel Model;
    public PaymentPage(Tr_D_PaymentViewModel model, ResponseWithDistributorResponse distributorResponse, IGenericRepository generic, Services.Data.ServicesService service)
	{
		InitializeComponent();
        Model = model;
        Rep = generic;
        _service = service;
        _distributorResponse = distributorResponse; 
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

    [Obsolete]
    protected override bool OnBackButtonPressed()
    {
        // Run the async code on the UI thread
        Dispatcher.Dispatch(() =>
        {
            Action action = async() =>
            {
                new Tr_D_ConfirmResponsePageViewModel(_distributorResponse, Rep, _service);
                await App.Current!.MainPage!.Navigation.PopAsync();
                //await App.Current!.MainPage!.Navigation.PushAsync(new ConfirmResponsePage(new Tr_D_ConfirmResponsePageViewModel(_distributorResponse, Rep, _service), Rep));
                //App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 2]);
            };
        });

        // Return true to prevent the default behavior
        return true;
    }
}