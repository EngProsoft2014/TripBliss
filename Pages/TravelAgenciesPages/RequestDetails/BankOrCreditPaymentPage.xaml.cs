using CommunityToolkit.Maui.Alerts;

namespace TripBliss.Pages.TravelAgenciesPages.RequestDetails;

public partial class BankOrCreditPaymentPage : ContentPage
{
    PaymentsViewModel ViewModel { get => BindingContext as PaymentsViewModel; set => BindingContext = value; }

    public CashOrCreditPaymentPage()
    {
        InitializeComponent();
    }

    private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        if (ViewModel != null && ViewModel.OneInvoice.Id == 0)
        {
            await App.Current!.MainPage!.Navigation.PushAsync(new MainPage());
        }
        else
        {
            await Navigation.PopAsync();
        }
    }

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(e.NewTextValue) != true)
        {
            ViewModel.OnePayment.Amount = decimal.Parse(e.NewTextValue);
            btnPayCredit.Text = string.Format("Pay USD ${0}", e.NewTextValue);
            btnPayCash.Text = string.Format("Pay USD ${0}", e.NewTextValue);
            lblPayCash.Text = e.NewTextValue;
        }
        else
        {
            ViewModel.OnePayment.Amount = ViewModel.OneInvoice.Net;
            btnPayCredit.Text = string.Format("Pay USD ${0}", ViewModel.OneInvoice.Net);
            btnPayCash.Text = string.Format("Pay USD ${0}", ViewModel.OneInvoice.Net);
            lblPayCash.Text = ViewModel.OneInvoice.Net.ToString();
        }
    }

    private void swtPayCredit_Toggled(object sender, ToggledEventArgs e)
    {
        if (e.Value == true)
        {
            ViewModel.OnePayment.Amount = ViewModel.OneInvoice.Net;
            btnPayCredit.Text = string.Format("Pay USD ${0}", ViewModel.OneInvoice.Net);
            btnPayCash.Text = string.Format("Pay USD ${0}", ViewModel.OneInvoice.Net);
            lblPayCash.Text = ViewModel.OneInvoice.Net.ToString();
        }
        else
        {
            if (string.IsNullOrEmpty(entryNewAmount.Text) != true)
            {
                ViewModel.OnePayment.Amount = decimal.Parse(entryNewAmount.Text);
                btnPayCredit.Text = string.Format("Pay USD ${0}", entryNewAmount.Text);
            }
            else if (string.IsNullOrEmpty(entryCashNewAmount.Text) != true)
            {
                ViewModel.OnePayment.Amount = decimal.Parse(entryCashNewAmount.Text);
                btnPayCash.Text = string.Format("Pay USD ${0}", entryCashNewAmount.Text);
                lblPayCash.Text = entryCashNewAmount.Text;
            }
            else
            {
                ViewModel.OnePayment.Amount = ViewModel.OneInvoice.Net;
                btnPayCredit.Text = string.Format("Pay USD ${0}", ViewModel.OneInvoice.Net);
                btnPayCash.Text = string.Format("Pay USD ${0}", ViewModel.OneInvoice.Net);
                lblPayCash.Text = ViewModel.OneInvoice.Net.ToString();
            }
        }
    }



}