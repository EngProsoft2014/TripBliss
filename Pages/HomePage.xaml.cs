using TripBliss.Pages.TravelAgenciesPages.RequestDeatiels;

namespace TripBliss.Pages;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

    private void RquestSelected(object sender, SelectedItemChangedEventArgs e)
    {
		Navigation.PushAsync(new RequestDeatielsPage());
    }
}