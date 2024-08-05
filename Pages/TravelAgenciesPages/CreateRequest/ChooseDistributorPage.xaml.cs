namespace TripBliss.Pages.TravelAgenciesPages.CreateRequest;

public partial class ChooseDistributorPage : ContentPage
{
	public ChooseDistributorPage()
	{
		InitializeComponent();
	}


    private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
		Navigation.PushAsync(new NewRequestPage());
    }
}