namespace TripBliss.Pages.DistributorsPages;

public partial class AddRequestPage : ContentPage
{
	public AddRequestPage()
	{
		InitializeComponent();
	}


    private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
		Navigation.PushAsync(new NewRequestPage());
    }
}