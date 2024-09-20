using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Helpers;
using TripBliss.Pages.TravelAgenciesPages.Users;

namespace TripBliss.ViewModels.TravelAgenciesViewModels.Users
{
    public partial class Tr_UsersViewModel : BaseViewModel
    {
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
    }

    
}
