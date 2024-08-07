namespace TripBliss.Pages.TravelAgenciesPages.RequestDeatiels;

public partial class NewRequestPage : ContentPage
{
	public NewRequestPage()
	{
		InitializeComponent();
	}

    private void Hotel_ItemTapped(object sender, ItemTappedEventArgs e)
    {
		Navigation.PushAsync(new HotelServicesPage());
    }

    private void Transportation_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        Navigation.PushAsync(new TransportaionServicePage());
    }

    private void AirFlight_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        Navigation.PushAsync(new AirFlightServicePage());
    }

    private void Visa_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        Navigation.PushAsync(new VisaServicePage());
    }
}