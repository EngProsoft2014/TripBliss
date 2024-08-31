using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.ViewModels.TravelAgenciesViewModels.CreateRequest;
using TripBliss.ViewModels.TravelAgenciesViewModels.RequestDetails;
namespace TripBliss.Pages.TravelAgenciesPages.RequestDetails;

public partial class TransportaionServicePage : Controls.CustomControl
{
    Tr_D_TransportaionServiceViewModel Model;
    public TransportaionServicePage(Tr_D_TransportaionServiceViewModel model , IGenericRepository generic)
	{
		InitializeComponent();
		this.BindingContext = model;
        Model = model;
		bool cc = model.ServiceModdel.AcceptAgen;
		this.IsEnabled = !cc;

    }

    private void TypePic_SelectedIndexChanged(object sender, EventArgs e)
    {
        var cc = TypePic.SelectedItem as CarTypeResponse;
        if (cc != null)
        {
            BrandPick.ItemsSource = Model.CarBrands.Where(a => a.CarTypeId == cc.Id).ToList();
        }

    }

    private void BrandPick_SelectedIndexChanged(object sender, EventArgs e)
    {
        var cc = BrandPick.SelectedItem as CarBrandResponse;
        if (cc != null)
        {
            ModelPick.ItemsSource = Model.CarModel.Where(a => a.CarBrandId == cc!.Id).ToList();
        }

    }
}