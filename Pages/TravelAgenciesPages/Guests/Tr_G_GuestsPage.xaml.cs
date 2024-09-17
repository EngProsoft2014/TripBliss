using TripBliss.Models.RequestTravelAgencyGuest;
using TripBliss.ViewModels.TravelAgenciesViewModels.Guests;

namespace TripBliss.Pages.TravelAgenciesPages.Guests;

public partial class Tr_G_GuestsPage : Controls.CustomControl
{
    GuestsViewModel ViewModel { get => BindingContext as GuestsViewModel; set => BindingContext = value; }

    public Tr_G_GuestsPage()
	{
		InitializeComponent();
	}

    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        colGuests.ItemsSource = ViewModel.Guests!.Where(x => (x.GuestName!).ToLower().Contains(e.NewTextValue.ToLower()));
    }
}