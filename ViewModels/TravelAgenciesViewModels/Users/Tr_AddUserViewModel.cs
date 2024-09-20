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

namespace TripBliss.ViewModels.TravelAgenciesViewModels.Users
{
    public partial class Tr_AddUserViewModel : BaseViewModel
    {
        [ObservableProperty]
        ApplicationUserRequest addModel = new ApplicationUserRequest();

        #region Services
        readonly Services.Data.ServicesService _service;
        IGenericRepository Rep;
        #endregion
        public Tr_AddUserViewModel(IGenericRepository generic, Services.Data.ServicesService service)
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
        public async Task ClickRegister(ApplicationUserRequest model)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                if (string.IsNullOrEmpty(AddModel.Email))
                {
                    var toast = Toast.Make("Please Complete This Field Required : Email.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                } 
                else if (string.IsNullOrEmpty(AddModel.Password))
                {
                    var toast = Toast.Make("Please Complete This Field Required : Password.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
                else
                {
                    IsBusy = true;
                    UserDialogs.Instance.ShowLoading();
                    model.UserPermision = 2;
                    model.UserCategory = Preferences.Default.Get(ApiConstants.userCategory,0);
                    var json = await Rep.PostTRAsync<ApplicationUserRequest, ApplicationUserResponse>(Constants.ApiConstants.RegisterApi, model);

                    if (json.Item1 != null && json.Item2 == null)
                    {
                        
                    }
                    else
                    {
                        if (json.Item2 != null)
                        {
                            if (json.Item2.errors != null)
                            {
                                StringBuilder builder = new StringBuilder();
                                foreach (var str in json.Item2!.errors!)
                                {
                                    builder.Append(str.Key + " " + str.Value);
                                }
                                var toast = Toast.Make($"Warning, {builder.ToString()}", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                                await toast.Show();
                            }
                        }
                    }

                    UserDialogs.Instance.HideHud();
                    IsBusy = false;
                }
            }
        }
    }
}
