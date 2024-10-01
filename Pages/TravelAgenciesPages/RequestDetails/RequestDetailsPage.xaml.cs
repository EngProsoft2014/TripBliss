using TripBliss.Helpers;
using TripBliss.ViewModels.TravelAgenciesViewModels.RequestDetails;
namespace TripBliss.Pages.TravelAgenciesPages.RequestDetails;

public partial class RequestDetailsPage : Controls.CustomControl
{
    Tr_D_RequestDetailsViewModel Model;
    public RequestDetailsPage(Tr_D_RequestDetailsViewModel model)
	{
		InitializeComponent();
		this.BindingContext = Model = model;
	}

    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        ItemsCollectionView.ItemsSource = Model?.RequestDetailes?.ResponseWithDistributor?.Where(x => (x.DistributorCompanyName!).ToLower().Contains(e.NewTextValue.ToLower()));

    }
}