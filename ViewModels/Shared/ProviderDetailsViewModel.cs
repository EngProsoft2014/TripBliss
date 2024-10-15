using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Models;

namespace TripBliss.ViewModels.Shared
{
    public partial class ProviderDetailsViewModel : BaseViewModel
    {
        #region Prop
        [ObservableProperty]
        TravelAgencyCompanyResponse distributorModel = new TravelAgencyCompanyResponse(); 
        #endregion

        #region Cons
        public ProviderDetailsViewModel()
        {

        }

        public ProviderDetailsViewModel(TravelAgencyCompanyResponse travelAgencyCompany)
        {
            if (string.IsNullOrEmpty(travelAgencyCompany.UrlLogo))
            {
                travelAgencyCompany.UrlLogo = "";
            }
            else
            {
                travelAgencyCompany.UrlLogo = $"{Helpers.Utility.ServerUrl}{travelAgencyCompany.UrlLogo}";
            }
            DistributorModel = travelAgencyCompany;
        }
        #endregion

        #region RelayCommand
        [RelayCommand]
        async Task BackPressed()
        {
            await App.Current!.MainPage!.Navigation.PopAsync();
        } 
        #endregion
    }
}
