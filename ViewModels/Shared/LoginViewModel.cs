using Akavache;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using Microsoft.Maui.ApplicationModel.Communication;
using Microsoft.Maui.Controls;
using Mopups.PreBaked.Interfaces;
using Newtonsoft.Json;
using Plugin.FirebasePushNotifications;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Constants;
using TripBliss.Controls;
using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.Pages;
using TripBliss.Pages.DistributorsPages;
using TripBliss.Pages.Shared;
using TripBliss.Pages.TravelAgenciesPages;
using TripBliss.Services;
using TripBliss.Services.Data;
using TripBliss.ViewModels.TravelAgenciesViewModels;
using static TripBliss.Helpers.ErrorsResult;



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
        [ObservableProperty]
        bool isRememberMe = false;

        #endregion

        #region Service
        readonly IGenericRepository Rep;
        readonly Services.Data.ServicesService _service;
        readonly SignalRService _signalRService;
        readonly INotificationService _notificationService;
        readonly IFirebasePushNotification _firebasePushNotification;
        #endregion

        #region Cons
        public LoginViewModel(IGenericRepository GenericRep, Services.Data.ServicesService service, SignalRService signalRService, INotificationService notificationService, IFirebasePushNotification firebasePushNotification)
        {
            Rep = GenericRep;
            _service = service;
            _signalRService = signalRService;
            _notificationService = notificationService;
            _firebasePushNotification = firebasePushNotification;

            if (_firebasePushNotification == null)
            {
                _firebasePushNotification = CrossFirebasePushNotification.Current;
            }

            if (Preferences.Default.Get<bool>(ApiConstants.rememberMe, false))
            {
                IsRememberMe = true;
                LoginRequest.UserName = Preferences.Default.Get<string>(ApiConstants.rememberMeUserName, string.Empty);
                LoginRequest.Password = Preferences.Default.Get<string>(ApiConstants.rememberMePassword, string.Empty);
            }
        }
        #endregion

        #region RelayCommand

        [RelayCommand]
        public async Task GoSignUpPage()
        {
            await App.Current!.MainPage!.Navigation.PushAsync(new SignUpPage(new SignUpViewModel(Rep, _service, _signalRService, _notificationService, _firebasePushNotification)));
        }

        [RelayCommand]
        public async Task GoRestPage()
        {
            var vm = new ResetViewModel(Rep, _service);
            var page = new ResetPage(vm);
            page.BindingContext = vm;
            await App.Current!.MainPage!.Navigation.PushAsync(page);
        }

        [RelayCommand]
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

                    if (IsRememberMe)
                    {
                        Preferences.Default.Set(ApiConstants.rememberMe, true);
                        Preferences.Default.Set(ApiConstants.rememberMeUserName, model.UserName);
                        Preferences.Default.Set(ApiConstants.rememberMePassword, model.Password);
                    }
                    else
                    {
                        Preferences.Default.Set(ApiConstants.rememberMe, false);
                        Preferences.Default.Remove(ApiConstants.rememberMeUserName);
                        Preferences.Default.Remove(ApiConstants.rememberMePassword);
                    }
                    //model.UserName = model.UserName.ToLower();
                    //model.Password = model.Password.ToLower();

                    await _firebasePushNotification.RegisterForPushNotificationsAsync();
                    model.FCM_Token = _firebasePushNotification.Token;

                    var json = await Rep.PostTRAsync<ApplicationUserLoginRequest, ApplicationUserResponse>(Constants.ApiConstants.LoginApi, model);

                    if (json.Item1 != null)
                    {
                        UserModel = json.Item1;

                        if (!string.IsNullOrEmpty(UserModel?.Id))
                        {
                            Controls.StaticMember.WayOfTab = 0;

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

                                var vm = new TravelAgenciesViewModels.Tr_HomeViewModel(Rep, _service, _signalRService, _notificationService, _firebasePushNotification);
                                var page = new Pages.TravelAgenciesPages.HomeAgencyPage(new Tr_HomeViewModel(Rep, _service, _signalRService, _notificationService, _firebasePushNotification), Rep, _service, _signalRService, _notificationService, _firebasePushNotification);
                                page.BindingContext = vm;
                                await App.Current!.MainPage!.Navigation.PushAsync(page);
                            }
                            if (string.IsNullOrEmpty(UserModel?.TravelAgencyCompanyId) && !string.IsNullOrEmpty(UserModel?.DistributorCompanyId))
                            {
                                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.SuccessfullyLogin, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                                await toast.Show();

                                Preferences.Default.Set(ApiConstants.review, UserModel!.DistributorCompany!.Review!.Value);

                                var vm = new DistributorsViewModels.Dis_HomeViewModel(Rep, _service, _signalRService, _notificationService, _firebasePushNotification);
                                var page = new Pages.DistributorsPages.HomeDistributorsPage(vm, Rep, _service, _signalRService, _notificationService, _firebasePushNotification);
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
                            await App.Current!.MainPage!.Navigation.PushAsync(new LoginPage(new LoginViewModel(Rep, _service, _signalRService, _notificationService, _firebasePushNotification)));
                            App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 2]);
                        }
                    }
                    else if (json.Item2 != null && json.Item2.errors != null)
                    {
                        if (json!.Item2!.errors!.Keys.Contains("Invalid Confirm Email - بريد إلكتروني مؤكد غير صالح"))
                        {
                            IsNotVerfy = true;
                            string Result = json.Item2.errors.FirstOrDefault().Value.ToString()!;
                            List<string> LstResult = Result.Split('_').ToList();
                            List<string> LstResult2 = LstResult[1].Split('-').ToList();
                            ResendEmail = LstResult2[0].Trim();
                        }

                        var toast = Toast.Make($"{json.Item2.errors.FirstOrDefault().Value}", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                        await toast.Show();
                    }
                    else
                    {
                        var toast = Toast.Make(TripBliss.Resources.Language.AppResources.msgThere_was_a_problem_with_this_procedure, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
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
