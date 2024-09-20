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
using TripBliss.Pages.TravelAgenciesPages.Users;

namespace TripBliss.ViewModels.TravelAgenciesViewModels.Users
{
    public partial class Tr_UsersViewModel : BaseViewModel
    {
        [ObservableProperty]
        ObservableCollection<ApplicationUserRequest> applicationUsers = new ObservableCollection<ApplicationUserRequest>();
        #region Services
        readonly Services.Data.ServicesService _service;
        IGenericRepository Rep;
        #endregion
        public Tr_UsersViewModel(IGenericRepository generic, Services.Data.ServicesService service)
        {
            _service = service;
            Rep = generic;
        }

        [RelayCommand]
        async Task BackPressed()
        {
            await App.Current!.MainPage!.Navigation.PopAsync();
        }
        [RelayCommand]
        async Task AddUserClick()
        {
            var vm = new Tr_AddUserViewModel(Rep,_service);
            var page = new Tr_AddUserPage(vm);
            page.BindingContext = vm;
            await App.Current!.MainPage!.Navigation.PushAsync(page);
        }

        async Task GetDocs()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();
                if (!string.IsNullOrEmpty(UserToken))
                {
                    string Id = Preferences.Default.Get(ApiConstants.travelAgencyCompanyId, "");
                    UserDialogs.Instance.ShowLoading();
                    var json = await Rep.GetAsync<ObservableCollection<ApplicationUserRequest>>($"{ApiConstants.GetUserApi}{Id}", UserToken);
                    UserDialogs.Instance.HideHud();

                    if (json != null)
                    {
                        ApplicationUsers = json;
                    }
                }
            }
        }
    }

    
}
