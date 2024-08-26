using CommunityToolkit.Maui.Alerts;
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
using TripBliss.Pages.TravelAgenciesPages.CreateRequest;

namespace TripBliss.ViewModels.TravelAgenciesViewModels.CreateRequest
{
    public partial class Tr_C_ChooseDistributorViewModel : BaseViewModel
    {
        #region Prop
        [ObservableProperty]
        public ObservableCollection<DistributorCompanyResponse>? distributorCompanys = new ObservableCollection<DistributorCompanyResponse>();
        [ObservableProperty]
        public ObservableCollection<DistributorCompanyResponse>? selectedDistributorCompanys = new ObservableCollection<DistributorCompanyResponse>();
        #endregion

        #region Services
        IGenericRepository Rep;
        readonly Services.Data.ServicesService _service;
        #endregion

        #region Cons
        public Tr_C_ChooseDistributorViewModel(IGenericRepository GenericRep, Services.Data.ServicesService service, ObservableCollection<DistributorCompanyResponse>? List)
        {
            Rep = GenericRep;
            _service = service;
            DistributorCompanys = List;
        } 
        #endregion

        #region RelayCommands

        [RelayCommand]
        void BackPressed()
        {
            App.Current.MainPage.Navigation.PopAsync();
        }
        [RelayCommand]
        void Selection(DistributorCompanyResponse model)
        {
            if (SelectedDistributorCompanys!.Contains(model))
            {
                SelectedDistributorCompanys!.Remove(model);
            }
            else
            {
                SelectedDistributorCompanys!.Add(model);
            }
        }
        [RelayCommand]
        async void Aplly(DistributorCompanyResponse model)
        {
            if (SelectedDistributorCompanys!.Count == 0 )
            {
                var toast = Toast.Make("Please select at least one distribuitor.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else
            {
                await App.Current!.MainPage!.Navigation.PushAsync(new NewRequestPage(new Tr_C_NewRequestViewModel(SelectedDistributorCompanys!, Rep, _service), Rep));
            }

            
        }
        #endregion

        public void SelectAll(bool IsSelected)
        {
            if (IsSelected)
            {
                SelectedDistributorCompanys = new ObservableCollection<DistributorCompanyResponse>(DistributorCompanys);
            }
            else
            {
                SelectedDistributorCompanys!.Clear();
            }
        }
    }
}
