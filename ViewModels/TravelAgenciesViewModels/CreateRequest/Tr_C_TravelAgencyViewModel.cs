﻿using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using Microsoft.AspNet.SignalR.Client;
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
using TripBliss.Pages.Shared;
using TripBliss.Pages.TravelAgenciesPages;
using TripBliss.Pages.TravelAgenciesPages.CreateRequest;


namespace TripBliss.ViewModels.TravelAgenciesViewModels.CreateRequest
{
    public partial class Tr_C_TravelAgencyViewModel : BaseViewModel
    {
        #region Prop
        [ObservableProperty]
        public ObservableCollection<DistributorCompanyResponse> distributorCompanys = new ObservableCollection<DistributorCompanyResponse>();
        [ObservableProperty]
        public ObservableCollection<DistributorCompanyResponse> distributorCompanysInPage = new ObservableCollection<DistributorCompanyResponse>();
        [ObservableProperty]
        public ObservableCollection<TravelAgencywithDistributorsResponse> favouriteDistributorCompanys = new ObservableCollection<TravelAgencywithDistributorsResponse>();
        [ObservableProperty]
        public int indexTap;
        public int PageNumber { get; set; }
        public bool IsHasNext { get; set; }
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
            Inti();
        }
        #endregion

        #region Methods

        async void Inti()
        {
            if (Constants.Permissions.CheckPermission(Constants.Permissions.TR_Show_Distributors))
            {
                PageNumber = 1;
                IsHasNext = true;
                UserDialogs.Instance.ShowLoading();
                await GetDistributors();
                await GetFavouiterDistributors();
                UserDialogs.Instance.HideHud();
            }
            else
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.PermissionAlert, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
        }

        public async Task GetDistributors()
        {
            IsBusy = false;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string id = Preferences.Default.Get(ApiConstants.travelAgencyCompanyId, "");
                string UserToken = await _service.UserToken();

                UserDialogs.Instance.ShowLoading();
                var json = await Rep.GetAsync<PagenationList<DistributorCompanyResponse>>(ApiConstants.GetDistributorCompaniesApi + $"{id}/{PageNumber}", UserToken);
                UserDialogs.Instance.HideHud();

                if (json != null)
                {
                    PagenationList<DistributorCompanyResponse> Distributors = json;
                  
                    IsHasNext = Distributors.HasNextPage;

                    DistributorCompanysInPage = new ObservableCollection<DistributorCompanyResponse>(Distributors?.DataModel!);

                    if (DistributorCompanys.Count == 0)
                    {
                        DistributorCompanys = new ObservableCollection<DistributorCompanyResponse>(DistributorCompanysInPage.OrderBy(x => x.CompanyName).ToList());
                    }
                    else
                    {
                        if (DistributorCompanys != DistributorCompanysInPage)
                        {
                            DistributorCompanysInPage.ToList().ForEach(f=> DistributorCompanys.Add(f));         
                        }      
                    }
                    PageNumber += 1;
                }

                //var toast = Toast.Make(DistributorCompanys.Count().ToString(), CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                //await toast.Show();
            }


            IsBusy = true;
        }

        
        async Task GetFavouiterDistributors()
        {
            IsBusy = true;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string id = Preferences.Default.Get(ApiConstants.travelAgencyCompanyId, "");
                string UserToken = await _service.UserToken();

                var json = await Rep.GetAsync<ObservableCollection<TravelAgencywithDistributorsResponse>>(ApiConstants.GetfavouritesApi + $"{id}/TravelAgencywithDistributors", UserToken);

                if (json != null)
                {
                    FavouriteDistributorCompanys!.Clear();
                    FavouriteDistributorCompanys = json;
                }
            }

            IsBusy = false;
        }

        async Task AddToFavouiter(TravelAgencywithDistributorsRequest DistributorModel)
        {
            IsBusy = true;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string id = Preferences.Default.Get(ApiConstants.travelAgencyCompanyId, "");
                string UserToken = await _service.UserToken();

                var json = await Rep.PostTRAsync<TravelAgencywithDistributorsRequest, TravelAgencywithDistributorsResponse>(ApiConstants.AddfavouritesApi + $"{id}/TravelAgencywithDistributors", DistributorModel, UserToken);

                if (json.Item1 != null)
                {
                    FavouriteDistributorCompanys!.Add(json.Item1!);
                    DistributorCompanys.Where(x => x.Id == json.Item1!.DistributorCompanyId).FirstOrDefault()!.Favourite = true;
                }
            }


            IsBusy = false;
        }

        async Task<string?> DeletFavouiterDistributors(string RecordId)
        {
            IsBusy = true;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string id = Preferences.Default.Get(ApiConstants.travelAgencyCompanyId, "");
                string UserToken = await _service.UserToken();

                string json = await Rep.PostEAsync(ApiConstants.DeletefavouritesApi + $"{id}/TravelAgencywithDistributors/{RecordId}", UserToken);

                if (!string.IsNullOrEmpty(json) && json == "No Content")
                {
                    return json;
                }
                else
                {
                    var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Failed_favourite_delete, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
            }


            IsBusy = false;
            return null;
        }
        #endregion

        #region RelayCommands
        [RelayCommand]
        async Task AddRequest()
        {
            IsBusy = false;

            if (Constants.Permissions.CheckPermission(Constants.Permissions.TR_Add_Request))
            {
                UserDialogs.Instance.ShowLoading();
                if (IndexTap == 0)
                {
                    await App.Current!.MainPage!.Navigation.PushAsync(new ChooseDistributorPage(new Tr_C_ChooseDistributorViewModel(Rep, _service, DistributorCompanys), Rep));
                }
                else
                {
                    ObservableCollection<DistributorCompanyResponse> LstDisModel = new ObservableCollection<DistributorCompanyResponse>();
                    FavouriteDistributorCompanys.ToList().ForEach(f =>
                    {
                        if (f.DistributorCompany != null)
                        {
                            LstDisModel.Add(f.DistributorCompany!);
                        }
                    });
                    await App.Current!.MainPage!.Navigation.PushAsync(new ChooseDistributorPage(new Tr_C_ChooseDistributorViewModel(Rep, _service, LstDisModel), Rep));
                }

                UserDialogs.Instance.HideHud();
            }
            else
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.PermissionAlert, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }

            IsBusy = true;

        }

        [RelayCommand]
        async Task BackPressed()
        {
            await App.Current!.MainPage!.Navigation.PopAsync();
        }

        [RelayCommand]
        async Task HeartClicked(DistributorCompanyResponse Item)
        {
            if (Item != null)
            {
                TravelAgencywithDistributorsRequest obj = new TravelAgencywithDistributorsRequest
                {
                    DistributorCompanyId = Item.Id,
                };


                if (Item.Favourite!.Value)
                {
                    TravelAgencywithDistributorsResponse? Model = FavouriteDistributorCompanys!.FirstOrDefault(a => a.DistributorCompany!.Id! == Item.Id!);
                    if (Model != null)
                    {
                        string? Stat = await DeletFavouiterDistributors(Model.Id!);
                        if (!string.IsNullOrEmpty(Stat) && Stat == "No Content")
                        {
                            FavouriteDistributorCompanys!.Remove(Model!);
                            DistributorCompanys.Where(x => x.Id == Model.DistributorCompanyId).FirstOrDefault()!.Favourite = false;
                        }
                    }
                }
                else
                {
                    await AddToFavouiter(obj);
                }
            }
        }

        [RelayCommand]
        async Task HeartClickedFavourite(TravelAgencywithDistributorsResponse model)
        {
            if (!string.IsNullOrEmpty(model.DistributorCompanyId) && model.DistributorCompany != null)
            {
                string? Stat = await DeletFavouiterDistributors(model.Id!);
                if (!string.IsNullOrEmpty(Stat) && Stat == "No Content")
                {
                    FavouriteDistributorCompanys!.Remove(model);
                    DistributorCompanys.Where(x => x.Id == model.DistributorCompanyId).FirstOrDefault()!.Favourite = false;
                }
            }
        }
        [RelayCommand]
        async Task Selection(DistributorCompanyResponse model)
        {
            var vm = new Tr_ProviderDetailsViewModel(model.Id, Rep, _service);
            var page = new Tr_ProviderDetailsPage();
            page.BindingContext = vm;
            await App.Current!.MainPage!.Navigation.PushAsync(page);
        }

        [RelayCommand]
        async Task SelectionFevourite(TravelAgencywithDistributorsResponse model)
        {
            var vm = new Tr_ProviderDetailsViewModel(model.DistributorCompanyId!, Rep, _service);
            var page = new Tr_ProviderDetailsPage();
            page.BindingContext = vm;
            await App.Current!.MainPage!.Navigation.PushAsync(page);
        }

        [RelayCommand]
        async Task GetLoadMore()
        {
            if (IsHasNext)
            {
                await GetDistributors();
            }
        }
        #endregion

    }
}
