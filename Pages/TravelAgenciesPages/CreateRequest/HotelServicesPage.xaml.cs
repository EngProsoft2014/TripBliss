using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.ViewModels.TravelAgenciesViewModels.CreateRequest;
namespace TripBliss.Pages.TravelAgenciesPages.CreateRequest;

public partial class HotelServicePage : Controls.CustomControl
{
    Tr_C_HotelServiceViewModel Model;
    public HotelServicePage(Tr_C_HotelServiceViewModel model, IGenericRepository generic)
	{
		InitializeComponent();
		BindingContext = model;
        Model = model;
	}

    private void LocationPicker(object sender, EventArgs e)
    {
        var cc = LocPick.SelectedItem as LocationResponse;
        HotelPick.ItemsSource = Model.Hoteles.Where(a=>a.LocationId == cc!.Id).ToList();
    }

    private void HotelPicker(object sender, EventArgs e)
    {

    }
}