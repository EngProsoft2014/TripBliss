using TripBliss.Constants;
using TripBliss.Models;
using TripBliss.Models.DistributorCompany;
using TripBliss.ViewModels.TravelAgenciesViewModels.CreateRequest;

namespace TripBliss.Pages.TravelAgenciesPages.CreateRequest;

public partial class ChooseServicesPopup : Mopups.Pages.PopupPage
{
    public ChooseServicesPopup(Tr_C_ChooseDistributorViewModel model)
	{
		InitializeComponent();

        this.BindingContext = model;
    }

}