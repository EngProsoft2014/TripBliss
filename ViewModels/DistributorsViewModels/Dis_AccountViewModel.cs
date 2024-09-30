using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using Microsoft.AspNet.SignalR.Client.Http;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Constants;
using TripBliss.Helpers;
using TripBliss.Models;

namespace TripBliss.ViewModels.DistributorsViewModels
{
    public partial class Dis_AccountViewModel : BaseViewModel
    {
        #region Prop
        [ObservableProperty]
        DistributorCompanyResponse companyResponse;
        [ObservableProperty]
        DistributorCompanyRequest companyRequest; 
        #endregion

        #region Services
        readonly Services.Data.ServicesService _service;
        IGenericRepository Rep;
        #endregion

        #region Cons
        public Dis_AccountViewModel(IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
            Init();
        }
        #endregion

        #region Methods
        async void Init()
        {
            await GetCompanyDetiles();
        }

        async Task GetCompanyDetiles()
        {
            IsBusy = true;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string id = Preferences.Default.Get(ApiConstants.distributorCompanyId, "");
                string UserToken = await _service.UserToken();
                if (!string.IsNullOrEmpty(UserToken))
                {
                    UserDialogs.Instance.ShowLoading();
                    var json = await Rep.GetAsync<DistributorCompanyResponse>(ApiConstants.GetDistCompanyDetailsApi + id, UserToken);
                    UserDialogs.Instance.HideHud();
                    if (json != null)
                    {
                        json.ImageFile = ImageSource.FromUri(new Uri($"{Helpers.Utility.ServerUrl}{json.UrlLogo}"));
                        CompanyResponse = json;

                    }
                }

            }

            IsBusy = false;
        }

        [RelayCommand]
        async Task OpenFullScreenImage()
        {
            IsBusy = false;
            UserDialogs.Instance.ShowLoading();
            await MopupService.Instance.PushAsync(new Pages.MainPopups.FullScreenImagePopup(CompanyResponse.ImageFile!));
            UserDialogs.Instance.HideHud();
            IsBusy = true;
        }

        #endregion

        #region RelayCommand
        [RelayCommand]
        async Task ConfirmeData()
        {


            IsBusy = false;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                CompanyRequest = new DistributorCompanyRequest
                {
                    CompanyName = CompanyResponse.CompanyName,
                    Extension = CompanyResponse.Extension,
                    Logo = CompanyResponse.Logo,
                    Address = CompanyResponse.Address,
                    Email = CompanyResponse.Email,
                    ExpireDateAcc = CompanyResponse.ExpireDateAcc,
                    Phone = CompanyResponse.Phone,
                    Policy = CompanyResponse.Policy,
                    Review = CompanyResponse.Review,
                    SendWithAllBulk = CompanyResponse.SendWithAllBulk,
                    ImgFile = CompanyResponse.ImgFile,
                    StripePassword = CompanyResponse.StripePassword,
                    StripeSecretKey = CompanyResponse.StripeSecretKey,
                    StripeUsername = CompanyResponse.StripeUsername,
                    Website = CompanyResponse.Website   
                };
                string UserToken = await _service.UserToken();

                string id = Preferences.Default.Get(ApiConstants.distributorCompanyId, "");
                var json = await Rep.PutAsync<DistributorCompanyRequest>(ApiConstants.PutDistCompanyDetailsApi + id, CompanyRequest, UserToken);

                if (json != null)
                {
                    await GetCompanyDetiles();
                }
            }

            IsBusy = true;
        }
        [RelayCommand]
        async Task BackClicked()
        {
            await App.Current!.MainPage!.Navigation.PopAsync();
        }

        [RelayCommand]
        async Task OpenAddImagesPopup()
        {
            IsBusy = false;

            var page = new Pages.MainPopups.AddAttachmentsPopup();
            page.ImageClose += async (img, imgPath) =>
            {
                if (!string.IsNullOrEmpty(img))
                {
                    byte[] bytes = Convert.FromBase64String(img);
                    CompanyResponse.ImageFile = ImageSource.FromStream(() => new MemoryStream(bytes));
                    CompanyResponse.ImgFile = bytes;
                    CompanyResponse.Extension = Path.GetExtension(imgPath);
                    
                }
                await MopupService.Instance.PopAsync();
            };

            await MopupService.Instance.PushAsync(page);

            IsBusy = true;
        }
        #endregion
    }
}
