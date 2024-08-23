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
        [ObservableProperty]
        public ObservableCollection<DistributorCompanyResponse>? favouriteDistributorCompanys = new ObservableCollection<DistributorCompanyResponse>();
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
            //await GetFavouiterDistributors();
            await GetDistributors();
            
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

                var json = await Rep.GetAsync<ObservableCollection<DistributorCompanyResponse>>(ApiConstants.GetfavouritesApi + $"{id}/TravelAgencywithDistributors", UserToken);

                if (json != null)
                {
                    FavouriteDistributorCompanys!.Clear();
                    FavouriteDistributorCompanys = json;
                }
            }

            IsBusy = false;
        }

        async Task AddToFavouiter(string DistributorId)
        {
            IsBusy = true;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string id = Preferences.Default.Get(ApiConstants.userid, "");
                string UserToken = await _service.UserToken();

                var json = await Rep.PostTRAsync<string, DistributorCompanyResponse>(ApiConstants.AddfavouritesApi + $"{id}/TravelAgencywithDistributors", DistributorId, UserToken);

                if (json.Item1 != null)
                {
                    FavouriteDistributorCompanys!.Add(json.Item1!);
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

                string json = await Rep.DeleteStrItemAsync(ApiConstants.DeletefavouritesApi + $"{id}/TravelAgencywithDistributors/{RecordId}", UserToken);

                if (json != null)
                {
                    return json;
                }
                
            }

            IsBusy = false;
            return null;
        }
        #endregion

        #region RelayCommands
        [RelayCommand]
        void AddRequest()
        {
            App.Current!.MainPage!.Navigation.PushAsync(new ChooseDistributorPage(new Tr_C_ChooseDistributorViewModel(Rep,_service,DistributorCompanys), Rep));
        }

        [RelayCommand]
        void BackPressed()
        {
            App.Current!.MainPage!.Navigation.PopAsync();
        }

        [RelayCommand]
        async Task HeartClicked(DistributorCompanyResponse Item)
        {
            if (Item != null)
            {
                bool IsFav =  FavouriteDistributorCompanys!.Contains(Item);
                if (IsFav)
                {
                    string? Stat = await DeletFavouiterDistributors(Item.Id!);
                    FavouriteDistributorCompanys.Remove(Item);
                }
                else
                {
                    await AddToFavouiter(Item.Id!);
                    FavouriteDistributorCompanys.Add(Item);
                }
            }
        }
        #endregion

    }
}
