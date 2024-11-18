using Syncfusion.Maui.Core.Carousel;
using TripBliss.ViewModels.TravelAgenciesViewModels;

namespace TripBliss.Pages.TravelAgenciesPages;

public partial class Tr_AccountPage : Controls.CustomControl
{

    Tr_AccountViewModel ViewModel { get => BindingContext as Tr_AccountViewModel; set => BindingContext = value; }

    public Tr_AccountPage()
	{
		InitializeComponent();
	}

    private void Entry_Focused(object sender, FocusEventArgs e)
    {
        var selectedOption = (sender as Entry).Text;
        ViewModel?.SelecteAddressCommand.Execute(selectedOption);
    }
}