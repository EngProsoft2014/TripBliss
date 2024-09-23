using TripBliss.ViewModels;

namespace TripBliss.Pages;

public partial class DocumentsPage : Controls.CustomControl
{
	public DocumentsPage(DocumentsViewModel model)
	{
		InitializeComponent();
		this.BindingContext = model;
	}
}