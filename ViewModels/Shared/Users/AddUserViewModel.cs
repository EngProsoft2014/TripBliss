using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Constants;
using TripBliss.Helpers;
using TripBliss.Models;

namespace TripBliss.ViewModels.Users
{
    public partial class AddUserViewModel : BaseViewModel
    {
        #region Prop
        [ObservableProperty]
        ApplicationUserRequest addModel = new ApplicationUserRequest(); 
        #endregion

        #region Services
        readonly Services.Data.ServicesService _service;
        IGenericRepository Rep;
        public delegate void AddUserDelegte();
        public event AddUserDelegte AddUserClose;
        #endregion

        #region Cons
        public AddUserViewModel(IGenericRepository generic, Services.Data.ServicesService service)
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

        [RelayCommand]
        public async Task ClickRegister(ApplicationUserRequest model)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                if (string.IsNullOrEmpty(AddModel.Email))
                {
                    var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_UserEmail, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
                else if (string.IsNullOrEmpty(AddModel.Password))
                {
                    var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_Password, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
                else
                {
                    IsBusy = true;
                    UserDialogs.Instance.ShowLoading();
                    model.UserPermision = 2;
                    model.UserCategory = Preferences.Default.Get(ApiConstants.userCategory, 0);
                    if (model.UserCategory == 3)
                    {
                        model.DistributorCompanyId = Preferences.Default.Get(ApiConstants.distributorCompanyId, "");
                    }
                    else
                    {
                        model.TravelAgencyCompanyId = Preferences.Default.Get(ApiConstants.travelAgencyCompanyId, "");
                    }
                    var json = await Rep.PostTRAsync<ApplicationUserRequest, ApplicationUserResponse>(Constants.ApiConstants.RegisterApi, model);

                    if (json.Item1 != null && json.Item2 == null)
                    {
                        AddModel = new ApplicationUserRequest();
                        var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Successfully_createuser, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                        await toast.Show();
                        AddUserClose.Invoke();
                    }
                    else
                    {
                        var toast = Toast.Make($"{json.Item2!.errors!.FirstOrDefault().Value}", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                        await toast.Show();
                    }

                    UserDialogs.Instance.HideHud();
                    IsBusy = false;
                }
            }
        } 
        #endregion
    }
}
