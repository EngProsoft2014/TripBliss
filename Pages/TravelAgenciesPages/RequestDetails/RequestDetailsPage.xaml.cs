
using CommunityToolkit.Maui.Alerts;
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

    private async void SfTabView_SelectionChanged(object sender, Syncfusion.Maui.TabView.TabSelectionChangedEventArgs e)
    {
        if ((int)e.NewIndex == 1)
        {
            if (!Constants.Permissions.CheckPermission(Constants.Permissions.Show_Active_Distributors_for_Request))
            {
                Model.RequestDetailes.ResponseWithDistributor = new List<Models.ResponseWithDistributorResponse>();
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.PermissionAlert, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
        }
    }
}