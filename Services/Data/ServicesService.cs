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
                        string Pass = await App.Current!.MainPage!.DisplayPromptAsync("Info", "Your Token expired, Please Enter Your Password", "Ok");

                        ApplicationUserLoginRequest model = new ApplicationUserLoginRequest()
                        {
                            UserName = Preferences.Default.Get(ApiConstants.username, ""),
                            Password = Pass
                        };

                        var loginModel = await Rep.PostTRAsync<ApplicationUserLoginRequest, ApplicationUserResponse>(Constants.ApiConstants.LoginApi, model);

                        if (loginModel.Item1 != null)
                        {
                            MUserToken = loginModel.Item1.Token!;

                            await BlobCache.LocalMachine.InsertObject(UserTokenServiceKey, loginModel.Item1.Token!, DateTimeOffset.Now.AddMinutes(30));

                            return loginModel.Item1.Token!;
                        }
                    }
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
