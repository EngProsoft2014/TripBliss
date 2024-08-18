namespace TripBliss.Pages.DistributorsPages;

public partial class HomeDistributorsPage : Controls.CustomControl
{
	public HomeDistributorsPage()
	{
		InitializeComponent();
	}

    [Obsolete]
    protected override bool OnBackButtonPressed()
    {
        // Run the async code on the UI thread
        Dispatcher.Dispatch(() =>
        {
            Action action = () => Application.Current!.Quit();
            Controls.StaticMember.ShowSnackBar("Do you want to exit the program?", Controls.StaticMember.SnackBarColor, Controls.StaticMember.SnackBarTextColor, action);
        });

        // Return true to prevent the default behavior
        return true;
    }
}