using System.Globalization;

namespace TripBliss.Controls;

public partial class CustomControl : ContentPage
{
	public CustomControl()
	{
		InitializeComponent();

        string Lan = Preferences.Default.Get("Lan", "en");
        if (Lan == "ar")
        {
            this.FlowDirection = FlowDirection.RightToLeft;
        }
        else
        {
            this.FlowDirection = FlowDirection.LeftToRight;
        }
    }
}