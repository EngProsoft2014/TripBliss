using Syncfusion.Maui.Core.Carousel;
using TripBliss.ViewModels.DistributorsViewModels;

namespace TripBliss.Pages.DistributorsPages;

public partial class Dis_AccountPage : Controls.CustomControl
{
    Dis_AccountViewModel ViewModel { get => BindingContext as Dis_AccountViewModel; set => BindingContext = value; }

    public Dis_AccountPage()
	{
		InitializeComponent();
	}

    private void Entry_Focused(object sender, FocusEventArgs e)
    {
        var selectedOption = (sender as Entry).Text;
        ViewModel?.SelecteAddressCommand.Execute(selectedOption);
    }
}