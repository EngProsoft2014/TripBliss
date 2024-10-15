using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using Syncfusion.Maui.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Constants;
using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.Models.DistributorCompany;
using TripBliss.Pages;
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
        public ObservableCollection<TravelAgencywithDistributorsResponse> favouriteDistributorCompanys = new ObservableCollection<TravelAgencywithDistributorsResponse>();
        [ObservableProperty]
        public int indexTap;
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

        async Task Inti()
        {
            if (Constants.Permissions.CheckPermission(Constants.Permissions.TR_Show_Distributors))
            {
                UserDialogs.Instance.ShowLoading();
                await GetDistributors();
                await GetFavouiterDistributors();
                UserDialogs.Instance.HideHud();
            }
            else
            {
                var toast = Toast.Make("Permission not allowed for this action.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
        }
        async Task GetDistributors()
        {
            IsBusy = true;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string id = Preferences.Default.Get(ApiConstants.travelAgencyCompanyId, "");
                string UserToken = await _service.UserToken();

                var json = await Rep.GetAsync<ObservableCollection<DistributorCompanyResponse>>(ApiConstants.GetDistributorCompaniesApi + $"{id}", UserToken);

                if (json != null)
                {

                    DistributorCompanys!.Clear();
                    DistributorCompanys = json;
                }
            }

            IsBusy = false;
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

        async Task<string?> DeletFavouiterDistributors(int RecordId)
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
                    var toast = Toast.Make("Failed favourite delete", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
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
            if (Constants.Permissions.CheckPermission(Constants.Permissions.TR_Add_Request))
            {
                if (IndexTap == 0)
                {
                    await App.Current!.MainPage!.Navigation.PushAsync(new ChooseDistributorPage(new Tr_C_ChooseDistributorViewModel(Rep, _service, DistributorCompanys), Rep));
                }
                else
                {
                    ObservableCollection<DistributorCompanyResponse> LstDisModel = new ObservableCollection<DistributorCompanyResponse>();
                    FavouriteDistributorCompanys.ForEach(f =>
                    {
                        if (f.DistributorCompany != null)
                        {
                            LstDisModel.Add(f.DistributorCompany!);
                        }
                    });
                    await App.Current!.MainPage!.Navigation.PushAsync(new ChooseDistributorPage(new Tr_C_ChooseDistributorViewModel(Rep, _service, LstDisModel), Rep));
                }
            }
            else
            {
                var toast = Toast.Make("Permission not allowed for this action.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }

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
                        string? Stat = await DeletFavouiterDistributors(Model!.Id);
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
                string? Stat = await DeletFavouiterDistributors(model.Id);
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
            var vm = new Tr_ProviderDetailsViewModel(model.Id,Rep,_service);
            var page = new Tr_ProviderDetailsPage();
            page.BindingContext = vm;
            await App.Current!.MainPage!.Navigation.PushAsync(page);
        }
        #endregion

    }
}
