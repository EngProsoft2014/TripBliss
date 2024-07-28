using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
//using TripBliss.Models;
using System.Threading.Tasks;
using Akavache;
using System.Reactive.Linq;



namespace TripBliss.Services.Data
{
    public interface IServicesService
    {
        Task<ImageSource> AccountPhoto();
        Task<string> UserToken();
    }


    public class ServicesService : IServicesService
    {

        readonly Helpers.IGenericRepository _ORep;
        public ServicesService(Helpers.IGenericRepository ORep)
        {
            _ORep = ORep;
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
                    //if (Helpers.Settings.Phone != "" && Helpers.Settings.Password != "")
                    //{
                    //    var loginModel = await ORep.GetAsync<EmployeeModel>("api/Login/GetLogin?" + "UserName=" + Helpers.Settings.UserName + "&" + "Password=" + Helpers.Settings.Password + "&" + "PlayerId=" + Helpers.Settings.PlayerId);

                    //    if (loginModel != null)
                    //    {
                    //        MUserToken = loginModel.GernToken;

                    //        await BlobCache.LocalMachine.InsertObject(UserTokenServiceKey, loginModel.GernToken, DateTimeOffset.Now.AddHours(24));

                    //        return loginModel.GernToken;
                    //    }
                    //}
                }
            }
            else
            {
                return MUserToken;
            }

            return MUserToken;
        }
    }
}
