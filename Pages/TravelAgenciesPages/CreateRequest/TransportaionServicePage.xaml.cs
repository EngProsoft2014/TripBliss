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
        var cc2 = BrandPick.SelectedItem as CarBrandResponse;

        if (cc != null && cc2 != null)
        {
            if(cc.Id != cc2.CarTypeId) 
            {
                Model.SelectrdBrand = Model.CarBrands.FirstOrDefault(a => a.CarTypeId == cc.Id)!;
                Model.SelectrdModel = Model.CarModel.FirstOrDefault(a => a.CarBrandId == Model.SelectrdBrand.Id)!;
            }
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