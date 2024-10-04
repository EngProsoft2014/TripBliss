
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Constants;
using TripBliss.Helpers;
using TripBliss.Models.Account;
using TripBliss.Pages;
using TripBliss.Pages.MainPopups;
using TripBliss.Pages.Shared;

namespace TripBliss.ViewModels
{
    public partial class ProfileViewModel : BaseViewModel
    {
        [ObservableProperty]
        AccountResponse account;
        #region Service
        readonly IGenericRepository Rep;
        readonly Services.Data.ServicesService _service;
        #endregion

        #region Cons
        public ProfileViewModel(IGenericRepository GenericRep, Services.Data.ServicesService service)
        {
            Rep = GenericRep;
            _service = service;
            Init();
        }
        #endregion

        #region RelayCommand
        [RelayCommand]
        async Task BackClicked()
        {
            await App.Current!.MainPage!.Navigation.PopAsync();
        }
        [RelayCommand]
        async Task ChangePassClick()
        {
            var vm = new ChangePassViewModel(Rep,_service);
            var page = new ChangePassPage();
            page.BindingContext = vm;
            await App.Current!.MainPage!.Navigation.PushAsync(page);
        }
        #endregion

        #region Methods
        async void Init()
        {
            await GetData();
        }
        async Task GetData()
        {
            IsBusy = false;
            UserDialogs.Instance.ShowLoading();
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string id = Preferences.Default.Get(ApiConstants.travelAgencyCompanyId, "");
                string UserToken = await _service.UserToken();
                if (!string.IsNullOrEmpty(UserToken))
                {

                    var json = await Rep.GetAsync<AccountResponse>(ApiConstants.GetUserAccountApi, UserToken);

                    if (json != null)
                    {
                        Account = json;
                    }
                }
                UserDialogs.Instance.HideHud();
            }

            IsBusy = true;
        } 
        #endregion
    }
}
