
using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.ViewModels.TravelAgenciesViewModels.CreateRequest;
namespace TripBliss.Pages.TravelAgenciesPages.CreateRequest;

public partial class ChooseDistributorPage : Controls.CustomControl
{
    //Tr_C_ChooseDistributorViewModel ViewModel { get => BindingContext as Tr_C_ChooseDistributorViewModel; set => BindingContext = value; }

    Tr_C_ChooseDistributorViewModel Model;
    public ChooseDistributorPage(Tr_C_ChooseDistributorViewModel model , IGenericRepository generic)
	{
		InitializeComponent();
		BindingContext = model;
        Model = model;
	}

    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        Model.SelectAll(e.Value,"all", null);
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        Model.SelectAll(false, "signal", sender as DistributorCompanyResponse);

        chkBoxSelectAll.IsChecked = Model.SelectedDistributorCompanys!.Count == Model.DistributorCompanys!.Count ? true : false;       
    }

    //private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    //{
    //    if (sender is Border border)
    //    {      
    //        VisualStateManager.GoToState(border, "Selected");
    //    }

    //}
}