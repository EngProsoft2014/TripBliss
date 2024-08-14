using System.Globalization;
using System.Reactive.Joins;

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
            CultureInfo.CurrentCulture = new CultureInfo("ar");
        }
        else
        {
            this.FlowDirection = FlowDirection.LeftToRight;
            CultureInfo.CurrentCulture = new CultureInfo("en");
        }
    }

}