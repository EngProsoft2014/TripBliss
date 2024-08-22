using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.Pages;
using TripBliss.Pages.TravelAgenciesPages.CreateRequest;


namespace TripBliss.ViewModels.TravelAgenciesViewModels.CreateRequest
{
    public partial class Tr_C_TravelAgencyViewModel : BaseViewModel
    {
        #region Prop
        [ObservableProperty]
        public ObservableCollection<DistributorCompanyResponse>? distributorCompanys = new ObservableCollection<DistributorCompanyResponse>(); 
        #endregion

        #region Services
        IGenericRepository Rep;
        readonly Services.Data.ServicesService _service;
        #endregion

        #region Cons
        public Tr_C_TravelAgencyViewModel(IGenericRepository GenericRep, Services.Data.ServicesService service)
        {
            Rep = GenericRep;
            _service = service;
            DistributorCompanys = Controls.StaticMember.LstDistributorCompanys;

        }
        #endregion

        #region RelayCommands
        [RelayCommand]
        void AddRequest()
        {
            App.Current.MainPage.Navigation.PushAsync(new ChooseDistributorPage(this, Rep));
        }

        [RelayCommand]
        void OnBackPressed()
        {
            App.Current.MainPage.Navigation.PopAsync();
        }
        [RelayCommand]
        void OnSelection()
        {
            App.Current.MainPage.Navigation.PushAsync(new NewRequestPage(new Tr_C_NewRequestViewModel(Rep, _service), Rep));
        } 
        #endregion

    }
}
