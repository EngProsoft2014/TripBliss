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
    public RequestDetailsPage(Dis_D_RequestDetailsViewModel model, IGenericRepository generic, Services.Data.ServicesService service)
	{
		InitializeComponent();
        Rep = generic;
        _service = service;
        BindingContext = Model = model;
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
            PaymmentView.BindingContext = PaymmentModel = new Dis_D_PaymentViewModel(Model.Response.Id!, Model.Response.TotalPriceAgentAccept, Model.Response.TotalPayment, Rep, _service);
        } 
    }
}