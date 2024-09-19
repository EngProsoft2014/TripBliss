using TripBliss.Models;
using TripBliss.ViewModels.ActivateViewModels;

namespace TripBliss.Pages.TravelAgenciesPages.RequestDetails;

public partial class VisaAttachmentsPage : Controls.CustomControl
{
	public VisaAttachmentsPage(VisaActivateViewModel model)
	{
		InitializeComponent();
		this.BindingContext = model;	
	}
}