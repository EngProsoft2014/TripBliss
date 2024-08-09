using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripBliss.ViewModels.TravelAgenciesVieModels.Offer
{
    partial class OfferDetielesViewModdel : BaseViewModel
    {


        [RelayCommand]
        void BackClicked()
        {
            App.Current.MainPage.Navigation.PopAsync();
        }
        [RelayCommand]
        void AddRequest()
        {

        }
    }
}
