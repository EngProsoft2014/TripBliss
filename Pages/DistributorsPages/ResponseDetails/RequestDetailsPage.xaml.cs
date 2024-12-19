using System.Reactive;
using TripBliss.Controls;
using TripBliss.Helpers;
using TripBliss.ViewModels.DistributorsViewModels.ResponseDetails;

namespace TripBliss.Pages.DistributorsPages.ResponseDetailes;


public partial class RequestDetailsPage : Controls.CustomControl
{
    Dis_D_RequestDetailsViewModel Model;
    Dis_D_PaymentViewModel PaymmentModel;
    #region Services
    readonly Services.Data.ServicesService _service;
    IGenericRepository Rep;
    #endregion
    public RequestDetailsPage(Dis_D_RequestDetailsViewModel model, Dis_D_PaymentViewModel modelPay, IGenericRepository generic, Services.Data.ServicesService service)
	{
		InitializeComponent();
        Rep = generic;
        _service = service;
        BindingContext = Model = model;
        PaymmentModel = modelPay;
    }


    private async void FBCheck_CheckedChanged_1(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value && Model != null)
        {
            await PaymmentModel.CalcOutPrice(0);
            AmmountEntry.Text = "";
        }
    }

    private void SfTabView_SelectionChanged(object sender, Syncfusion.Maui.TabView.TabSelectionChangedEventArgs e)
    {
        if (e.NewIndex == 2)
        {
            // = new Dis_D_PaymentViewModel(Model.Response.Id!, Model.Response.TotalPriceAgentAccept, Model.Response.TotalPayment, Rep, _service)
            PaymmentView.BindingContext = PaymmentModel;
        } 
    }

    private async void AmmountEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(e.NewTextValue))
        {
            await PaymmentModel.CalcOutPrice(0);
        }
        else
        {
            decimal cc = Convert.ToDecimal(e.NewTextValue);
            int num = (int)Math.Round(cc);
            await PaymmentModel.CalcOutPrice(num);
        }
    }



    //[Obsolete]
    //protected override bool OnBackButtonPressed()
    //{
    //    // Run the async code on the UI thread
    //    Dispatcher.Dispatch(() =>
    //    {
    //        Action action = async () =>
    //        {
    //            new Tr_D_ConfirmResponsePageViewModel(_distributorResponse, Rep, _service);
    //            await App.Current!.MainPage!.Navigation.PopAsync();
    //            //await App.Current!.MainPage!.Navigation.PushAsync(new ConfirmResponsePage(new Tr_D_ConfirmResponsePageViewModel(_distributorResponse, Rep, _service), Rep));
    //            //App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 2]);
    //        };
    //    });

    //    // Return true to prevent the default behavior
    //    return true;
    //}
}

