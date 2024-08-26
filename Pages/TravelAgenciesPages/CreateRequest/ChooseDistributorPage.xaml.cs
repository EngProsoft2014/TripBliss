
using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.ViewModels.TravelAgenciesViewModels.CreateRequest;
namespace TripBliss.Pages.TravelAgenciesPages.CreateRequest;

public partial class ChooseDistributorPage : Controls.CustomControl
{
    Tr_C_ChooseDistributorViewModel Model;
    public ChooseDistributorPage(Tr_C_ChooseDistributorViewModel model , IGenericRepository generic)
	{
		InitializeComponent();
		BindingContext = model;
        Model = model;
	}

    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        Model.SelectAll(e.Value);
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        if (sender is Border border)
        {      
            VisualStateManager.GoToState(border, "Selected");
        }
        
    }
}