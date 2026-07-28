
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using Microsoft.Maui;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Constants;
using TripBliss.DataPaginated;
using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.Models.DistributorCompany;
using TripBliss.Pages.TravelAgenciesPages;
using TripBliss.Pages.TravelAgenciesPages.CreateRequest;

namespace TripBliss.ViewModels.TravelAgenciesViewModels.CreateRequest
{
    public partial class Tr_C_ChooseDistributorViewModel : BaseViewModel
    {
        #region Prop
        [ObservableProperty]
        public ObservableCollection<DistributorCompanyResponse>? distributorCompanys = new ObservableCollection<DistributorCompanyResponse>();
        [ObservableProperty]
        public ObservableCollection<DistributorCompanyResponse> distributorCompanysInPage = new ObservableCollection<DistributorCompanyResponse>();
        [ObservableProperty]
        public ObservableCollection<DistributorCompanyResponse>? selectedDistributorCompanys = new ObservableCollection<DistributorCompanyResponse>();
        [ObservableProperty]
        DistributorCompanyFilterRequest filterModel;
        public int PageNumber { get; set; }
        public bool IsHasNext { get; set; }
        public bool IsfirstList { get; set; } = true;
        #endregion

        #region Services
        IGenericRepository Rep;
        readonly Services.Data.ServicesService _service;
        #endregion

        #region Cons
        public Tr_C_ChooseDistributorViewModel(IGenericRepository GenericRep, Services.Data.ServicesService service, ObservableCollection<DistributorCompanyResponse>? List, DistributorCompanyFilterRequest modelFilter)
        {
            Rep = GenericRep;
            _service = service;
            DistributorCompanys = List;
            FilterModel = modelFilter;
            DistributorCompanys.ToList().ForEach(f => f.IsSelected = false);
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
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.select_at_least_one_distribuitor, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else
            {
                await App.Current!.MainPage!.Navigation.PushAsync(new NewRequestPage(new Tr_C_NewRequestViewModel(SelectedDistributorCompanys!, Rep, _service), Rep));
            }   
        }


        [RelayCommand]
        async Task OpenMenuFilter()
        {
            IsBusy = true;
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    await MopupService.Instance.PushAsync(new ChooseServicesPopup(this));
                }
            }
            catch (Exception ex)
            {
                await App.Current!.MainPage!.DisplayAlert("Error", ex.Message, "OK");
            }
            IsBusy = false;
        }


        [RelayCommand]
        public async Task GetDistributors()
        {
            IsBusy = false;

            await MopupService.Instance.PopAsync();

            IsHasNext = true;
            PageNumber = 1;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();

                UserDialogs.Instance.ShowLoading();
                var json = await Rep.PostTRAsync<DistributorCompanyFilterRequest, PagenationList<DistributorCompanyResponse>>(ApiConstants.GetDistributorCompaniesApi + $"{PageNumber}", FilterModel, UserToken);
                UserDialogs.Instance.HideHud();

                if (json.Item1 != null)
                {
                    PagenationList<DistributorCompanyResponse> Distributors = json.Item1;

                    IsHasNext = Distributors.HasNextPage;
                    if (!IsHasNext && DistributorCompanys?.Count <= 10)
                    {
                        DistributorCompanys.Clear();
                    }

                    DistributorCompanysInPage = new ObservableCollection<DistributorCompanyResponse>(Distributors?.DataModel!);

                    if (DistributorCompanys.Count == 0)
                    {
                        DistributorCompanys = new ObservableCollection<DistributorCompanyResponse>(DistributorCompanysInPage.OrderBy(x => x.CompanyName).ToList());
                    }
                    else
                    {
                        if (DistributorCompanys != DistributorCompanysInPage && !IsfirstList)
                        {
                            DistributorCompanysInPage.ToList().ForEach(f => DistributorCompanys.Add(f));
                        }
                        else if (DistributorCompanys != DistributorCompanysInPage && IsfirstList)
                        {
                            DistributorCompanys = new ObservableCollection<DistributorCompanyResponse>(DistributorCompanysInPage.OrderBy(x => x.CompanyName).ToList());
                            IsfirstList = false;
                        }
                    }
                    PageNumber += 1;
                }
                else
                {
                    var toast = Toast.Make($"{json.Item2!.errors!.FirstOrDefault().Value}", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
            }

            IsBusy = true;
        }
        #endregion



        public async void SelectAll(bool IsSelected,string Way, DistributorCompanyResponse? model)
        {
            if (DistributorCompanys?.Count > 0) 
            {
                if (IsSelected)
                {
                    SelectedDistributorCompanys = new ObservableCollection<DistributorCompanyResponse>(DistributorCompanys!);
                    DistributorCompanys.ToList().ForEach(f => f.IsSelected = true);
                }
                else
                {
                    if(model != null)
                    {
                        SelectedDistributorCompanys!.Clear();
                        DistributorCompanys.ToList().ForEach(f => f.IsSelected = false);

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
                                DistributorCompanys.ToList().ForEach(f => f.IsSelected = false);
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
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Sorry_Dont_have_distribuitor, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
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
