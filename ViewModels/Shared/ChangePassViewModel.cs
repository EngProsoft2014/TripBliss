using Akavache;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Constants;
using TripBliss.Helpers;
using TripBliss.Pages;
using TripBliss.Pages.Shared;

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
                    var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Enter_Old_Password, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
                else if (string.IsNullOrEmpty(ChangeModel.newPassword))
                {
                    var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Enter_New_Password, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
                else
                {

                    string UserToken = await _service.UserToken();
                    var json = await Rep.PutAsync<ChangePass>(ApiConstants.PutChangePassApi, ChangeModel, UserToken);

                    if (json.currentPassword != null && json.newPassword != null)
                    {
                        var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Password_changed_successfully, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                        await toast.Show();

                        string LangValueToKeep = Preferences.Default.Get("Lan", "en");
                        Preferences.Default.Clear();
                        await BlobCache.LocalMachine.InvalidateAll();
                        await BlobCache.LocalMachine.Vacuum();
                        Constants.Permissions.LstPermissions.Clear();                        
                        Preferences.Default.Set("Lan", LangValueToKeep);
                        await Application.Current!.MainPage!.Navigation.PushAsync(new LoginPage(new LoginViewModel(Rep, _service)));
                    }
                    else
                    {
                        var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Error_Please_Check_Data_again, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                        await toast.Show();
                    }
                }

            }
        }

        [RelayCommand]
        void OnBackPressed()
        {
            App.Current!.MainPage!.Navigation.PopAsync();
        }
        #endregion
    }
}
