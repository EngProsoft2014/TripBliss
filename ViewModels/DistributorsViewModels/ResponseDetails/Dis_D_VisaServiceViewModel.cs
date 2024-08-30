using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Helpers;
using TripBliss.Models;

namespace TripBliss.ViewModels.DistributorsViewModels.ResponseDetails
{
     public partial class Dis_D_VisaServiceViewModel : BaseViewModel
    {
        #region Prop
        [ObservableProperty]
        ResponseWithDistributorVisaResponse moddel = new ResponseWithDistributorVisaResponse();
        #endregion

        IGenericRepository Rep;
        public Dis_D_VisaServiceViewModel(IGenericRepository generic)
        {
            Rep = generic;

        }
        public Dis_D_VisaServiceViewModel(ResponseWithDistributorVisaResponse model, IGenericRepository generic)
        {
            Rep = generic;
            Moddel = model;
            Lang = Preferences.Default.Get("Lan", "en");
        }

        #region RelayCommand
        [RelayCommand]
        async Task Apply()
        {
            bool answer = await App.Current!.MainPage!.DisplayAlert("Question?", "Are You Accept This Price?", "Yes", "No");
            Moddel!.AcceptPriceDis = answer;
            await App.Current!.MainPage!.Navigation.PopAsync();
        }
        [RelayCommand]
        async Task BackCLicked()
        {
            await App.Current!.MainPage!.Navigation.PopAsync();
        } 
        #endregion
    }
}
