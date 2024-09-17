using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.ViewModels.TravelAgenciesViewModels.RequestDetails;
namespace TripBliss.Pages.TravelAgenciesPages.RequestDetails;

public partial class HotelServicePage : Controls.CustomControl
{
    Tr_D_HotelServiceViewModel Model;
    public HotelServicePage(Tr_D_HotelServiceViewModel model,IGenericRepository  generic)
	{
		InitializeComponent();
        Model = model;
    }

    private void LocationPicker(object sender, EventArgs e)
    {
        var cc = LocPick.SelectedItem as LocationResponse;
        HotelPick.ItemsSource = Model.Hoteles.Where(a => a.LocationId == cc!.Id).ToList();
    }
}