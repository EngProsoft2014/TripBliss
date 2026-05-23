using Syncfusion.Maui.Core.Carousel;
using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.ViewModels.DistributorsViewModels;
using TripBliss.ViewModels.TravelAgenciesViewModels.CreateRequest;
namespace TripBliss.Pages.TravelAgenciesPages.CreateRequest;

public partial class HotelServicePage : Controls.CustomControl
{
    Tr_C_HotelServiceViewModel Model;

    public HotelServicePage(Tr_C_HotelServiceViewModel model, IGenericRepository generic)
	{
		InitializeComponent();
        this.BindingContext = Model = model;
	}


    //private void LocationPicker(object sender, EventArgs e)
    //{
    //    var cc = LocPick.SelectedItem as LocationResponse;
    //    if (cc != null) 
    //    {
    //        //comboBoxHoteles.ItemsSource = Model.Hoteles.Where(a => a.LocationId == cc!.Id).ToList();
    //        imgMap.IsVisible = true;
    //    }
        

    //    //var selectedOption = (sender as Picker).SelectedItem;
    //    //if (selectedOption != null)
    //    //{
    //    //    LocationResponse cc = selectedOption as LocationResponse;
    //    //    HotelPick.ItemsSource = Model.Hoteles.Where(a => a.LocationId == cc!.Id).ToList();
    //    //}
    //}

    //private void Entry_Focused(object sender, FocusEventArgs e)
    //{
    //    var selectedOption = (sender as Entry).Text;
    //    Model?.SelecteAddressHotelCommand.Execute(selectedOption);
    //}

    private void HotelEntry_TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        var entry = sender as Entry;
        var selectedOption = entry?.Text;
        Model?.SelecteAddressHotelCommand.Execute(selectedOption);
    }
}