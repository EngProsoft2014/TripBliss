using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Helpers;

namespace TripBliss.ViewModels.Shared
{
    public partial class UserPermissionViewModel : BaseViewModel
    {
        #region Services
        readonly Services.Data.ServicesService _service;
        IGenericRepository Rep;
        #endregion

        #region Cons
        public UserPermissionViewModel(IGenericRepository generic, Services.Data.ServicesService service)
        {
            _service = service;
            Rep = generic;
        } 
        #endregion

        #region RelayCommand
        [RelayCommand]
        async Task BackPressed()
        {
            await App.Current!.MainPage!.Navigation.PopAsync();
        }
        #endregion
    }
}
