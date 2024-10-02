using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Constants;
using TripBliss.Helpers;
using TripBliss.Pages;

namespace TripBliss.ViewModels
{
    public partial class ChangePassViewModel : BaseViewModel
    {
        public new class ChangePass
        {
            public string? currentPassword { get; set; }
            public string? newPassword { get; set; }
        }

        #region Prop
        [ObservableProperty]
        ChangePass changeModel = new ChangePass(); 
        #endregion

        #region Services
        readonly Services.Data.ServicesService _service;
        IGenericRepository Rep;
        #endregion

        #region Cons
        public ChangePassViewModel(IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
        } 
        #endregion

        #region RelayCommand
        [RelayCommand]
        async Task ChangeCLick()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                if (string.IsNullOrEmpty(ChangeModel.currentPassword))
                {
                    var toast = Toast.Make("Please Enter a Required : Old Password.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
                else if (string.IsNullOrEmpty(ChangeModel.newPassword))
                {
                    var toast = Toast.Make("Please Enter a Required : New Password.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
                else
                {

                    string UserToken = await _service.UserToken();
                    var json = await Rep.PutAsync<ChangePass>(ApiConstants.PutChangePassApi, ChangeModel, UserToken);

                    if (json.currentPassword != null && json.newPassword != null)
                    {
                        var toast = Toast.Make("Password changed successfully.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                        await toast.Show();

                        Preferences.Default.Clear();
                        await Application.Current!.MainPage!.Navigation.PushAsync(new LoginPage(new LoginViewModel(Rep, _service)));
                    }
                    else
                    {
                        var toast = Toast.Make("Error Please Check Data again.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                        await toast.Show();
                    }
                }

            }
        } 
        #endregion
    }
}
