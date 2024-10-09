using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Helpers;
using TripBliss.Models;


namespace TripBliss.ViewModels.Shared
{
    public partial class UserPermissionViewModel : BaseViewModel
    {
        [ObservableProperty]
        ObservableCollection<PermissonClass> userPermissions = new ObservableCollection<PermissonClass>();
        [ObservableProperty]
        ObservableCollection<PermissonClass> requestPermissions = new ObservableCollection<PermissonClass>();

        #region Services
        readonly Services.Data.ServicesService _service;
        IGenericRepository Rep;
        #endregion

        #region Cons
        public UserPermissionViewModel(IGenericRepository generic, Services.Data.ServicesService service)
        {
            _service = service;
            Rep = generic;
            LoadData();
        } 
        #endregion

        #region RelayCommand
        [RelayCommand]
        async Task BackPressed()
        {
            await App.Current!.MainPage!.Navigation.PopAsync();
        }
        #endregion

        void LoadData()
        {
            UserPermissions.Add(new PermissonClass
            {
                Name = "Edit Company Data",
                Statues = false,
            });
            UserPermissions.Add(new PermissonClass
            {
                Name = "Add User",
                Statues = false,
            });

            RequestPermissions.Add(new PermissonClass
            {
                Name = "Add Request",
                Statues = true,
            });
            RequestPermissions.Add(new PermissonClass
            {
                Name = "Accept Request",
                Statues = true,
            });
        }
    }
}
