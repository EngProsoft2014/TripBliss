using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Syncfusion.Maui.Data;
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
            DistributorCompanys.ForEach(f => f.IsSelected = false);
        }
        #endregion


        #region RelayCommands

        [RelayCommand]
        void BackPressed()
        {
            App.Current!.MainPage!.Navigation.PopAsync();
        }

        [RelayCommand]
        void Selection(DistributorCompanyResponse model)
        {
            if (SelectedDistributorCompanys!.Contains(model))
            {
                SelectedDistributorCompanys!.Remove(model);
                DistributorCompanys!.Where(x => x.Id == model.Id).FirstOrDefault()!.IsSelected = false;
                model.IsSelected = false;
            }
            else
            {
                SelectedDistributorCompanys!.Add(model);
                DistributorCompanys!.Where(x => x.Id == model.Id).FirstOrDefault()!.IsSelected = true;
                model.IsSelected = true;
            }
        }

        [RelayCommand]
        async Task Apply(DistributorCompanyResponse model)
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

        public async void SelectAll(bool IsSelected,string Way, DistributorCompanyResponse? model)
        {
            if (DistributorCompanys?.Count > 0) 
            {
                if (IsSelected)
                {
                    SelectedDistributorCompanys = new ObservableCollection<DistributorCompanyResponse>(DistributorCompanys!);
                    DistributorCompanys.ForEach(f => f.IsSelected = true);
                }
                else
                {
                    if(model != null)
                    {
                        SelectedDistributorCompanys!.Clear();
                        DistributorCompanys.ForEach(f => f.IsSelected = false);

                        model.IsSelected = true;
                        SelectedDistributorCompanys.Add(model);
                        DistributorCompanys.Where(_ => _.Id == model.Id).FirstOrDefault()!.IsSelected = true;
                    }
                    else
                    {
                        if(DistributorCompanys.Count == SelectedDistributorCompanys!.Count)
                        {
                            if(Way == "all")
                            {
                                SelectedDistributorCompanys!.Clear();
                                DistributorCompanys.ForEach(f => f.IsSelected = false);
                            }
                        }
                        else
                        {
                            if (Way == "signal")
                            {
                                SelectedDistributorCompanys!.Clear();
                                SelectedDistributorCompanys = new ObservableCollection<DistributorCompanyResponse>(DistributorCompanys!.Where(x=> x.IsSelected == true).ToList());
                            }

                        }

                    }           
                }
            }
            else
            {
                var toast = Toast.Make("Sorry Don't have distribuitor.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
        }

        //public void Search(string Name)
        //{
        //    if (Name != null)
        //    {
        //        DistributorCompanys = new ObservableCollection<DistributorCompanyResponse>(OrginalDistributorCompanys!.Where(a => a.CompanyName!.Contains(Name)).ToList());
        //    }
        //}
    }
}
