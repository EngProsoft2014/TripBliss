using TripBliss.ViewModels.DistributorsViewModels;


namespace TripBliss.Pages.DistributorsPages;

public partial class Dis_DocumentsPage : Controls.CustomControl
{
	public Dis_DocumentsPage(Dis_DocumentsViewModel model)
	{
		InitializeComponent();
		this.BindingContext = model;
	}
}