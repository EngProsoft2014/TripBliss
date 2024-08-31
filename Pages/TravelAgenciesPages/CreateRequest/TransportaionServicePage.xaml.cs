using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.ViewModels.TravelAgenciesViewModels.CreateRequest;

namespace TripBliss.Pages.TravelAgenciesPages.CreateRequest;

public partial class TransportaionServicePage : Controls.CustomControl
{
    Tr_C_TransportaionServiceViewModel Model;
    public TransportaionServicePage(Tr_C_TransportaionServiceViewModel model, IGenericRepository generic)
	{
		InitializeComponent();
		BindingContext = model;
        Model = model;
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
        if (cc!=null)
        {
            ModelPick.ItemsSource = Model.CarModel.Where(a=>a.CarBrandId == cc!.Id).ToList();
        }
        
    }
}