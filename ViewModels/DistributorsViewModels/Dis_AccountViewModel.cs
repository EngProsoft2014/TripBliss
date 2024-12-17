using Akavache;
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
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Constants;
using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.Pages.Shared;
using TripBliss.Pages.TravelAgenciesPages;
using TripBliss.ViewModels.TravelAgenciesViewModels;

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
                        CompanyResponse.UrlLogo = json.UrlLogo == null ? "" : json.UrlLogo;
                    }
                }
            }
            else
            {
                await App.Current!.MainPage!.Navigation.PushAsync(new NoInternetPage(Rep,_service));
            }

            IsBusy = false;
        }

        [RelayCommand]
        async Task OpenFullScreenImage()
        {
            IsBusy = false;
            //UserDialogs.Instance.ShowLoading();
            await App.Current!.MainPage!.Navigation.PushAsync(new Pages.MainPopups.FullScreenImagePopup(CompanyResponse.ImageFile!));
            //UserDialogs.Instance.HideHud();
            IsBusy = true;
        }

        #endregion

        #region RelayCommand
        [RelayCommand]
        async Task ConfirmeData()
        {

            if (Constants.Permissions.CheckPermission(Constants.Permissions.UpdateDistributorCompanyAccount))
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
                        locationlatitude = CompanyResponse.locationlatitude,
                        locationlongitude = CompanyResponse.locationlongitude,
                        State = CompanyResponse.State,
                        City = CompanyResponse.City,
                        PostalcodeZIP = CompanyResponse.PostalcodeZIP,
                        Country = CompanyResponse.Country,
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
                        BankAccounNumber = CompanyResponse.BankAccounNumber,
                        BankName = CompanyResponse.BankName,
                        Website = CompanyResponse.Website
                    };
                    string UserToken = await _service.UserToken();

                    string id = Preferences.Default.Get(ApiConstants.distributorCompanyId, "");
                    UserDialogs.Instance.ShowLoading();
                    var json = await Rep.PutAsync<DistributorCompanyRequest>(ApiConstants.PutDistCompanyDetailsApi + id, CompanyRequest, UserToken);
                    UserDialogs.Instance.HideHud();
                    if (json != null)
                    {
                        await GetCompanyDetiles();
                    }
                }

                IsBusy = true;
            }
            else
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.PermissionAlert, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
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

        [RelayCommand]
        async Task DeleteCompany()
        {
            bool answer = await App.Current!.MainPage!.DisplayAlert(TripBliss.Resources.Language.AppResources.Question, TripBliss.Resources.Language.AppResources.Do_you_Want_to_Delete_Company_Account, TripBliss.Resources.Language.AppResources.Yes, TripBliss.Resources.Language.AppResources.No);
            if (answer)
            {
                IsBusy = false;

                string UserToken = await _service.UserToken();
                UserDialogs.Instance.ShowLoading();
                var json = await Rep.PutAsync<string>(ApiConstants.PutDSCompanyAccountDelete + CompanyResponse.Id + "/ToggleActive", null, UserToken);
                UserDialogs.Instance.HideHud();

                if (string.IsNullOrEmpty(json))
                {
                    var toast = Toast.Make(TripBliss.Resources.Language.AppResources.DeleteCompanySuccess, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
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
                    var toast = Toast.Make(TripBliss.Resources.Language.AppResources.ErrorTryAgain, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }

                
                IsBusy = true;
            }
        }

        [RelayCommand]
        async Task MoreDetails()
        {
            var vm = new Tr_ProviderDetailsViewModel(CompanyResponse!.Id!, Rep, _service);
            var page = new Tr_ProviderDetailsPage();
            page.BindingContext = vm;
            await App.Current!.MainPage!.Navigation.PushAsync(page);
        }

        [RelayCommand]
        async Task SelecteAddress()
        {
            IsBusy = true;
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    var popupView = new Pages.MainPopups.AddressPupop();
                    popupView.DidClose += async (str) =>
                    {
                        CompanyResponse.Address = str.FullAddress;
                        CompanyResponse.locationlatitude = str.Latitude.ToString();
                        CompanyResponse.locationlongitude = str.Longitude.ToString();
                        CompanyResponse.State = str.State;
                        CompanyResponse.City = str.City;
                        CompanyResponse.PostalcodeZIP = str.Zip;
                        CompanyResponse.Country = str.Country;
                    };

                    await MopupService.Instance.PushAsync(popupView);
                }
            }
            catch (Exception ex)
            {
                await App.Current!.MainPage!.DisplayAlert("Error", ex.Message, "OK");
            }

            IsBusy = false;
        }
        #endregion
    }
}
