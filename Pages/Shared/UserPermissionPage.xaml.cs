namespace TripBliss.Pages.Shared;

public partial class UserPermissionPage : Controls.CustomControl
{
	public UserPermissionPage()
	{
		InitializeComponent();
	}

    private void TapGestureRecognizer_UserPremision(object sender, TappedEventArgs e)
    {
        if (DetailsRequest.IsVisible == false)
        {
            DetailsRequest.IsVisible = true;
        }
        else
        {
            DetailsRequest.IsVisible = false;
        }
    }

    private void TapGestureRecognizer_RequestTravelAgency(object sender, TappedEventArgs e)
    {
        if (RequestTravelAgency.IsVisible == false)
        {
            RequestTravelAgency.IsVisible = true;
        }
        else
        {
            RequestTravelAgency.IsVisible = false;
        }
    }

    private void TapGestureRecognizer_Offer(object sender, TappedEventArgs e)
    {
        if (Offer.IsVisible == false)
        {
            Offer.IsVisible = true;
        }
        else
        {
            Offer.IsVisible = false;
        }
    }

    private void TapGestureRecognizer_UsersColc(object sender, TappedEventArgs e)
    {
        if (UsersColc.IsVisible == false)
        {
            UsersColc.IsVisible = true;
        }
        else
        {
            UsersColc.IsVisible = false;
        }
    }

    private void TapGestureRecognizer_Documents(object sender, TappedEventArgs e)
    {
        if (Documents.IsVisible == false)
        {
            Documents.IsVisible = true;
        }
        else
        {
            Documents.IsVisible = false;
        }
    }

    private void TapGestureRecognizer_History(object sender, TappedEventArgs e)
    {
        if (History.IsVisible == false)
        {
            History.IsVisible = true;
        }
        else
        {
            History.IsVisible = false;
        }
    }

    private void TapGestureRecognizer_TravelAgencyCompany(object sender, TappedEventArgs e)
    {
        if (TravelAgencyCompany.IsVisible == false)
        {
            TravelAgencyCompany.IsVisible = true;
        }
        else
        {
            TravelAgencyCompany.IsVisible = false;
        }
    }

    
}