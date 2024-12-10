using CommunityToolkit.Maui.Alerts;
using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.ViewModels.TravelAgenciesViewModels.RequestDetails;

namespace TripBliss.Pages.TravelAgenciesPages.RequestDetails;

public partial class BankOrCreditPaymentPage : ContentPage
{
    //PaymentsViewModel ViewModel { get => BindingContext as PaymentsViewModel; set => BindingContext = value; }

    IGenericRepository Rep;
    readonly Services.Data.ServicesService _service;
    ResponseWithDistributorResponse _distributorResponse;
    Tr_D_PaymentViewModel Model;
    public BankOrCreditPaymentPage(Tr_D_PaymentViewModel model, ResponseWithDistributorResponse distributorResponse, IGenericRepository generic, Services.Data.ServicesService service)
    {
        InitializeComponent();
        Model = model;
        Rep = generic;
        _service = service;
        _distributorResponse = distributorResponse;
    }

    private async void entryCashNewAmount_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(e.NewTextValue))
        {
            await Model.CalcOutPrice(0);
        }
        else
        {
            await Model.CalcOutPrice(Convert.ToInt32(e.NewTextValue!));
        }
        
    }

    private async void FBCheckBank_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            await Model.CalcOutPrice(0);
        }
    }

    //private void FBCheckCredit_CheckedChanged(object sender, CheckedChangedEventArgs e)
    //{
    //    if (e.Value)
    //    {
    //        Model.PayMethod = 2;
            
    //    }
    //    else
    //    {
    //        Model.PayMethod = 3;
    //    }
    //}

    private void TapGestureRecognizer_TappedCredit(object sender, TappedEventArgs e)
    {
        Model.PayMethod = 2; //Credit Strip
    }

    private void TapGestureRecognizer_TappedBank(object sender, TappedEventArgs e)
    {
        Model.PayMethod = 3; //Bank Transfer
    }

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        var entry = sender as Entry;
        if (entry == null) return;

        // Auto-format to MM/YY after 4 characters
        if (entry.Text.Length == 4 && !entry.Text.Contains("/"))
        {
            entry.Text = entry.Text.Insert(2, "/");
        }

        // Validate input (e.g., valid month and future date)
        if (entry.Text.Length == 5)
        {
            string[] parts = entry.Text.Split('/');
            if (int.TryParse(parts[0], out int month) && int.TryParse(parts[1], out int year))
            {
                if (month < 1 || month > 12 || year < DateTime.Now.Year % 100)
                {
                    entry.TextColor = Colors.Red; // Invalid date
                    Model.IsExpirationDateValid = false;
                }
                else
                {
                    entry.TextColor = Colors.Black; // Valid date
                    Model.ExpirationDate = entry.Text;
                    Model.IsExpirationDateValid = true;
                }
            }
        }
    }



    //private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    //{
    //    if (ViewModel != null && ViewModel.OneInvoice.Id == 0)
    //    {
    //        await App.Current!.MainPage!.Navigation.PushAsync(new MainPage());
    //    }
    //    else
    //    {
    //        await Navigation.PopAsync();
    //    }
    //}

    //private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    //{
    //    if (string.IsNullOrEmpty(e.NewTextValue) != true)
    //    {
    //        ViewModel.OnePayment.Amount = decimal.Parse(e.NewTextValue);
    //        btnPayCredit.Text = string.Format("Pay USD ${0}", e.NewTextValue);
    //        btnPayCash.Text = string.Format("Pay USD ${0}", e.NewTextValue);
    //        lblPayCash.Text = e.NewTextValue;
    //    }
    //    else
    //    {
    //        ViewModel.OnePayment.Amount = ViewModel.OneInvoice.Net;
    //        btnPayCredit.Text = string.Format("Pay USD ${0}", ViewModel.OneInvoice.Net);
    //        btnPayCash.Text = string.Format("Pay USD ${0}", ViewModel.OneInvoice.Net);
    //        lblPayCash.Text = ViewModel.OneInvoice.Net.ToString();
    //    }
    //}

    //private void swtPayCredit_Toggled(object sender, ToggledEventArgs e)
    //{
    //    if (e.Value == true)
    //    {
    //        ViewModel.OnePayment.Amount = ViewModel.OneInvoice.Net;
    //        btnPayCredit.Text = string.Format("Pay USD ${0}", ViewModel.OneInvoice.Net);
    //        btnPayCash.Text = string.Format("Pay USD ${0}", ViewModel.OneInvoice.Net);
    //        lblPayCash.Text = ViewModel.OneInvoice.Net.ToString();
    //    }
    //    else
    //    {
    //        if (string.IsNullOrEmpty(entryNewAmount.Text) != true)
    //        {
    //            ViewModel.OnePayment.Amount = decimal.Parse(entryNewAmount.Text);
    //            btnPayCredit.Text = string.Format("Pay USD ${0}", entryNewAmount.Text);
    //        }
    //        else if (string.IsNullOrEmpty(entryCashNewAmount.Text) != true)
    //        {
    //            ViewModel.OnePayment.Amount = decimal.Parse(entryCashNewAmount.Text);
    //            btnPayCash.Text = string.Format("Pay USD ${0}", entryCashNewAmount.Text);
    //            lblPayCash.Text = entryCashNewAmount.Text;
    //        }
    //        else
    //        {
    //            ViewModel.OnePayment.Amount = ViewModel.OneInvoice.Net;
    //            btnPayCredit.Text = string.Format("Pay USD ${0}", ViewModel.OneInvoice.Net);
    //            btnPayCash.Text = string.Format("Pay USD ${0}", ViewModel.OneInvoice.Net);
    //            lblPayCash.Text = ViewModel.OneInvoice.Net.ToString();
    //        }
    //    }
    //}
}