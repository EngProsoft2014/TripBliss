using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
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
using TripBliss.Pages;
using TripBliss.Pages.DistributorsPages;
using TripBliss.Pages.DistributorsPages.ResponseDetailes;
using TripBliss.Pages.Shared;
using TripBliss.Pages.TravelAgenciesPages;
using TripBliss.Pages.TravelAgenciesPages.CreateRequest;
using TripBliss.Pages.TravelAgenciesPages.RequestDetails;
using TripBliss.ViewModels.ActivateViewModels;
using TripBliss.ViewModels.DistributorsViewModels.ResponseDetails;
using TripBliss.ViewModels.Shared;
using TripBliss.ViewModels.TravelAgenciesViewModels;
using TripBliss.ViewModels.TravelAgenciesViewModels.CreateRequest;


namespace TripBliss.ViewModels.DistributorsViewModels.CreateResponse
{
    partial class Dis_DistributorsViewModel : BaseViewModel
    {
        #region Prop
        [ObservableProperty]
        ObservableCollection<TravelAgencyCompanyResponse> companyResponses = new ObservableCollection<TravelAgencyCompanyResponse>();
        [ObservableProperty]
        public ObservableCollection<TravelAgencyCompanyResponse> agenciesInPage = new ObservableCollection<TravelAgencyCompanyResponse>();
        [ObservableProperty]
        public ObservableCollection<DistributorCompanyResponse> distributorCompanys = new ObservableCollection<DistributorCompanyResponse>();
        [ObservableProperty]
        public ObservableCollection<DistributorCompanyResponse> distributorCompanysInPage = new ObservableCollection<DistributorCompanyResponse>();
        public int PageNumberTr { get; set; }
        public bool IsHasNextTr { get; set; }
        public int PageNumberDis { get; set; }
        public bool IsHasNextDis { get; set; }
        #endregion

        #region Services
        readonly Services.Data.ServicesService _service;
        IGenericRepository Rep;
        #endregion

        #region Cons
        public Dis_DistributorsViewModel(IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
            Init();
        }
        #endregion

        #region Methods
        async void Init()
        {
            PageNumberTr = 1;
            IsHasNextTr = true;
            await LoadAgency();

            PageNumberDis = 1;
            IsHasNextDis = true;
            await GetDistributors();
        }

        public async Task LoadAgency()
        {
            IsBusy = false;

            if (Constants.Permissions.CheckPermission(Constants.Permissions.DS_Show_Agencies))
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    string UserToken = await _service.UserToken();
                    if (!string.IsNullOrEmpty(UserToken))
                    {
                        UserDialogs.Instance.ShowLoading();
                        var json = await Rep.GetAsync<PagenationList<TravelAgencyCompanyResponse>>(ApiConstants.GetTravelCompanysApi + $"/{PageNumberTr}", UserToken);
                        UserDialogs.Instance.HideHud();
                        if (json != null)
                        {
                            PagenationList<TravelAgencyCompanyResponse> AgenciesPage = json;

                            IsHasNextTr = AgenciesPage.HasNextPage;

                            AgenciesInPage = new ObservableCollection<TravelAgencyCompanyResponse>(AgenciesPage?.DataModel!);

                            if (CompanyResponses.Count == 0)
                            {
                                CompanyResponses = new ObservableCollection<TravelAgencyCompanyResponse>(AgenciesInPage.OrderBy(x => x.CompanyName).ToList());
                            }
                            else
                            {
                                if (CompanyResponses != AgenciesInPage)
                                {
                                    AgenciesInPage.ToList().ForEach(f => CompanyResponses.Add(f));
                                }
                            }

                            PageNumberTr += 1;
                        }

                        //var toast = Toast.Make(CompanyResponses.Count().ToString(), CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                        //await toast.Show();
                    }
                }

            }
            else
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.PermissionAlert, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }

            IsBusy = true;


            //if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            //{
            //    string UserToken = await _service.UserToken();
            //    if (!string.IsNullOrEmpty(UserToken))
            //    {
            //        UserDialogs.Instance.ShowLoading();
            //        var json = await Rep.GetAsync<ObservableCollection<TravelAgencyCompanyResponse>>(ApiConstants.GetTravelCompanysApi, UserToken);
            //        UserDialogs.Instance.HideHud();
            //        if (json != null)
            //        {
            //            CompanyResponses = json;
            //        }
            //    }
            //}
        }

        public async Task GetDistributors()
        {
            IsBusy = false;

            //if (Constants.Permissions.CheckPermission(Constants.Permissions.TR_Show_Distributors))
            //{
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string id = Preferences.Default.Get(ApiConstants.distributorCompanyId, "");
                string UserToken = await _service.UserToken();

                UserDialogs.Instance.ShowLoading();
                var json = await Rep.GetAsync<PagenationList<DistributorCompanyResponse>>(ApiConstants.GetDistributorCompaniesByDistributorApi + $"{id}/{PageNumberDis}", UserToken);
                UserDialogs.Instance.HideHud();

                if (json != null)
                {
                    PagenationList<DistributorCompanyResponse> Distributors = json;

                    IsHasNextDis = Distributors.HasNextPage;

                    DistributorCompanysInPage = new ObservableCollection<DistributorCompanyResponse>(Distributors?.DataModel!);

                    if (DistributorCompanys.Count == 0)
                    {
                        DistributorCompanys = new ObservableCollection<DistributorCompanyResponse>(DistributorCompanysInPage.OrderBy(x => x.CompanyName).ToList());
                    }
                    else
                    {
                        if (DistributorCompanys != DistributorCompanysInPage)
                        {
                            DistributorCompanysInPage.ToList().ForEach(f => DistributorCompanys.Add(f));
                        }
                    }
                    PageNumberDis += 1;
                }

                //var toast = Toast.Make(DistributorCompanys.Count().ToString(), CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                //await toast.Show();
            }
            //}
            //else
            //{
            //    var toast = Toast.Make(TripBliss.Resources.Language.AppResources.PermissionAlert, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
            //    await toast.Show();
            //}

            IsBusy = true;
        }

        #endregion

        #region RelayCommand
        [RelayCommand]
        async Task AddRequestDistributor()
        {
            if (!IsBusy)
                return;

            IsBusy = false;

            //if (Constants.Permissions.CheckPermission(Constants.Permissions.TR_Add_Request))
            //{
            UserDialogs.Instance.ShowLoading();

            DistributorCompanyFilterRequest FilterModel = new DistributorCompanyFilterRequest()
            {
                TravelAgencyCompanyId = Preferences.Default.Get(ApiConstants.travelAgencyCompanyId, ""),
                IsHotel = true,
                IsTransportation = true,
                IsAirFlight = true,
                IsVisa = true,
                IsGuide = true
            };
            await App.Current!.MainPage!.Navigation.PushAsync(new ChooseDistributorPage(new Tr_C_ChooseDistributorViewModel(Rep, _service, DistributorCompanys, FilterModel), Rep));

            UserDialogs.Instance.HideHud();
            //}
            //else
            //{
            //    var toast = Toast.Make(TripBliss.Resources.Language.AppResources.PermissionAlert, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
            //    await toast.Show();
            //}

            IsBusy = true;

        }

        [RelayCommand]
        async Task OnBackPressed()
        {
            await App.Current!.MainPage!.Navigation.PopAsync();
        }
        [RelayCommand]
        async Task Selection(TravelAgencyCompanyResponse model)
        {
            var vm = new Dis_ProviderDetailsViewModel(model!.Id!, Rep, _service);
            var page = new Dis_ProviderDetailsPage();
            page.BindingContext = vm;
            await App.Current!.MainPage!.Navigation.PushAsync(page);
        }

        [RelayCommand]
        async Task SelectionDistributor(DistributorCompanyResponse model)
        {
            var vm = new Tr_ProviderDetailsViewModel(model.Id, Rep, _service);
            var page = new Tr_ProviderDetailsPage();
            page.BindingContext = vm;
            await App.Current!.MainPage!.Navigation.PushAsync(page);
        }

        [RelayCommand]
        async Task GetLoadMore()
        {
            if (IsHasNextTr)
            {
                await LoadAgency();
            }
        }


        [RelayCommand]
        async Task GetLoadMoreDistributor()
        {
            if (IsHasNextDis)
            {
                await GetDistributors();
            }
        }
        #endregion


    }
}
