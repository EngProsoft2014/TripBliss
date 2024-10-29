using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
//using TripBliss.Models;
using System.Threading.Tasks;
using Akavache;
using System.Reactive.Linq;
using TripBliss.Models;
using TripBliss.Constants;
using CommunityToolkit.Maui.Alerts;
using Controls.UserDialogs.Maui;
using TripBliss.Pages.Shared;



namespace TripBliss.Services.Data
{
    public interface IServicesService
    {
        Task<ImageSource> AccountPhoto();
        Task<string> UserToken();
    }


    public class ServicesService : IServicesService
    {

        readonly Helpers.IGenericRepository Rep;
        public ServicesService(Helpers.IGenericRepository ORep)
        {
            Rep = ORep;
        }

        ImageSource Photo;
        string ServiceKey = "ServiceKey";

        //EmployeeModel loginModel;
        string MUserToken;
        public static string UserTokenServiceKey = "UserTokenServiceKey";

        public async Task<ImageSource> AccountPhoto()
        {
            //Photo = Controls.StaticMembers.AccountImg;
            await BlobCache.LocalMachine.InsertObject(ServiceKey, Photo, DateTimeOffset.Now.AddYears(1));
            return Photo;
        }


        public async Task<string> UserToken()
        {
            try
            {
                MUserToken = await BlobCache.LocalMachine.GetObject<string>(UserTokenServiceKey);
            }
            catch (Exception)
            {
                MUserToken = null;
            }

            if (MUserToken == null)
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {        
                    if (!string.IsNullOrEmpty(Preferences.Default.Get(ApiConstants.username, "")))
                    {
                        string Pass = await App.Current!.MainPage!.DisplayPromptAsync(TripBliss.Resources.Language.AppResources.Info, TripBliss.Resources.Language.AppResources.Your_Token_expired_Please_Enter_Your_Password, TripBliss.Resources.Language.AppResources.OK);

                        ApplicationUserLoginRequest model = new ApplicationUserLoginRequest()
                        {
                            UserName = Preferences.Default.Get(ApiConstants.username, ""),
                            Password = Pass
                        };

                        UserDialogs.Instance.ShowLoading();
                        var loginModel = await Rep.PostTRAsync<ApplicationUserLoginRequest, ApplicationUserResponse>(Constants.ApiConstants.LoginApi, model);
                        UserDialogs.Instance.HideHud();

                        if (loginModel.Item1 != null)
                        {
                            MUserToken = loginModel.Item1.Token!;

                            await BlobCache.LocalMachine.InsertObject(UserTokenServiceKey, loginModel.Item1.Token!, DateTimeOffset.Now.AddMinutes(43200));

                            return loginModel.Item1.Token!;
                        }
                        else
                        {
                            var toast = Toast.Make(TripBliss.Resources.Language.AppResources.PasswordInvalid, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                            await toast.Show();
                        }
                    }
                }
                else
                {
                    await App.Current!.MainPage!.Navigation.PushAsync(new NoInternetPage(Rep, this));
                }
            }
            else
            {
                return MUserToken;
            }

            return MUserToken!;
        }
    }
}
