using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Pages.TravelAgenciesPages;
using TripBliss.Pages.DistributorsPages;
using TripBliss.Models;
using TripBliss.Helpers;
using Akavache;
using Controls.UserDialogs.Maui;
using CommunityToolkit.Maui.Alerts;
using Newtonsoft.Json;
using TripBliss.Controls;
using TripBliss.ViewModels.TravelAgenciesViewModels;
using TripBliss.Services;
using TripBliss.Pages;
using TripBliss.Constants;
using Mopups.PreBaked.Interfaces;
using TripBliss.Services.Data;
using System.Reactive.Linq;
using System.ComponentModel.DataAnnotations;
using static TripBliss.Helpers.ErrorsResult;
using TripBliss.Pages.Shared;
using Microsoft.Maui.ApplicationModel.Communication;



namespace TripBliss.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        new class VerfyEmail
        {
            [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
            public string? Email { get; set; }
        }

        #region Property

        [ObservableProperty]
        ApplicationUserResponse userModel = new ApplicationUserResponse();
        [ObservableProperty]
        ApplicationUserLoginRequest loginRequest = new ApplicationUserLoginRequest();
        [ObservableProperty]
        bool isNotVerfy = false;
        [ObservableProperty]
        int timeRemaining = 0;
        [ObservableProperty]
        string resendEmail;

        #endregion

        #region Service
        readonly IGenericRepository Rep;
        readonly Services.Data.ServicesService _service;
        #endregion

        #region Cons
        public LoginViewModel(IGenericRepository GenericRep, Services.Data.ServicesService service)
        {
            Rep = GenericRep;
            _service = service;
        }
        #endregion

        #region RelayCommand

        [RelayCommand]
        public async Task GoSignUpPage()
        {
            await App.Current!.MainPage!.Navigation.PushAsync(new SignUpPage(new SignUpViewModel(Rep, _service)));
        }

        [RelayCommand]
        public async Task GoRestPage()
        {
            var vm = new ResetViewModel(Rep,_service);
            var page = new ResetPage(vm);
            page.BindingContext = vm;
            await App.Current!.MainPage!.Navigation.PushAsync(page);
        }

        [RelayCommand]
        [Obsolete]
        public async Task ClickLogin(ApplicationUserLoginRequest model)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                if (string.IsNullOrEmpty(model?.UserName))
                {
                    var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_UserName, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
                else if (string.IsNullOrEmpty(model?.Password))
                {
                    var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_Password, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
                else
                {
                    IsBusy = true;
                    UserDialogs.Instance.ShowLoading();

                    //model.UserName = model.UserName.ToLower();
                    //model.Password = model.Password.ToLower();

                    var json = await Rep.PostTRAsync<ApplicationUserLoginRequest, ApplicationUserResponse>(Constants.ApiConstants.LoginApi, model);

                    if (json.Item1 != null)
                    {
                        UserModel = json.Item1;

                        if (!string.IsNullOrEmpty(UserModel?.Id))
                        {

                            Preferences.Default.Set(ApiConstants.userid, UserModel.Id);
                            Preferences.Default.Set(ApiConstants.email, UserModel.Email);
                            Preferences.Default.Set(ApiConstants.username, UserModel.UserName);
                            Preferences.Default.Set(ApiConstants.userPermision, UserModel.UserPermision);
                            Preferences.Default.Set(ApiConstants.userCategory, UserModel.UserCategory);
                            Preferences.Default.Set(ApiConstants.travelAgencyCompanyId, UserModel.TravelAgencyCompanyId);
                            Preferences.Default.Set(ApiConstants.distributorCompanyId, UserModel.DistributorCompanyId);
                            Preferences.Default.Set(ApiConstants.permissions, JsonConvert.SerializeObject(UserModel.Permissions));

                            await BlobCache.LocalMachine.InsertObject(ServicesService.UserTokenServiceKey, UserModel?.Token, DateTimeOffset.Now.AddMinutes(43200));

                            Constants.Permissions.LstPermissions = UserModel?.Permissions!;
                            //Constants.Permissions.DecodeJwtToClass(UserModel?.Token!);

                            if (!string.IsNullOrEmpty(UserModel?.TravelAgencyCompanyId) && string.IsNullOrEmpty(UserModel?.DistributorCompanyId))
                            {
                                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.SuccessfullyLogin, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                                await toast.Show();

                                Preferences.Default.Set(ApiConstants.review, UserModel!.TravelAgencyCompany!.Review!.Value);

                                var vm = new TravelAgenciesViewModels.Tr_HomeViewModel(Rep, _service);
                                var page = new Pages.TravelAgenciesPages.HomeAgencyPage(new Tr_HomeViewModel(Rep, _service), Rep, _service);
                                page.BindingContext = vm;
                                await App.Current!.MainPage!.Navigation.PushAsync(page);
                            }
                            if (string.IsNullOrEmpty(UserModel?.TravelAgencyCompanyId) && !string.IsNullOrEmpty(UserModel?.DistributorCompanyId))
                            {
                                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.SuccessfullyLogin, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                                await toast.Show();

                                Preferences.Default.Set(ApiConstants.review, UserModel!.DistributorCompany!.Review!.Value);

                                var vm = new DistributorsViewModels.Dis_HomeViewModel(Rep, _service);
                                var page = new Pages.DistributorsPages.HomeDistributorsPage(vm, Rep, _service);
                                page.BindingContext = vm;
                                await App.Current!.MainPage!.Navigation.PushAsync(page);
                            }
                            if (string.IsNullOrEmpty(UserModel?.TravelAgencyCompanyId) && string.IsNullOrEmpty(UserModel?.DistributorCompanyId))
                            {
                                var toast1 = Toast.Make(TripBliss.Resources.Language.AppResources.This_account_is_an_admin, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                                await toast1.Show();
                            }
                        }
                        else
                        {

                            var toast = Toast.Make(TripBliss.Resources.Language.AppResources.user_name_is_not_registered, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                            await toast.Show();
                            await App.Current!.MainPage!.Navigation.PushAsync(new LoginPage(new LoginViewModel(Rep, _service)));
                            App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 2]);
                        }
                    }
                    else
                    {
                        if (json!.Item2!.errors!.Keys.Contains("Invalid Confirm Email"))
                        {
                            IsNotVerfy = true;
                            string Result = json.Item2.errors.FirstOrDefault().Value.ToString()!;
                            List<string> LstResult = Result.Split('_').ToList();
                            ResendEmail = LstResult[1].Trim();
                        }

                        var toast = Toast.Make($"{json.Item2.errors.FirstOrDefault().Value}", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                        await toast.Show();
                    }

                    UserDialogs.Instance.HideHud();
                    IsBusy = false;
                }
            }
        }

        [RelayCommand]
        async Task VerfivationClick()
        {
            IsBusy = false;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string email = await App.Current!.MainPage!.DisplayPromptAsync("Info", "Please enter your Email", "Ok");
                VerfyEmail model = new VerfyEmail { Email = email };
                if (model.Email != null)
                {
                    string UserToken = await _service.UserToken();
                    UserDialogs.Instance.ShowLoading();
                    var Postjson = await Rep.PostTRAsync<VerfyEmail, ErrorResult>($"{ApiConstants.PostVerifyApi}", model!, UserToken);
                    UserDialogs.Instance.HideHud();
                    if (Postjson.Item2 == null)
                    {

                        TimeRemaining = 60;
                        while (TimeRemaining > 0)
                        {
                            IsNotVerfy = false;
                            TimeRemaining--;
                            await Task.Delay(1000); // Wait for 1 second
                        }

                        IsNotVerfy = true;
                    }
                    else
                    {
                        var toast = Toast.Make(TripBliss.Resources.Language.AppResources.enter_vaild_Email, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                        await toast.Show();
                    }

                }
                else
                {
                    var toast = Toast.Make(TripBliss.Resources.Language.AppResources.enter_your_Email, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
            }

            IsBusy = true;
        }

        #endregion
    }
}
