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


namespace TripBliss.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        readonly IGenericRepository Rep;

        public LoginViewModel(IGenericRepository GenericRep)
        {
            Rep = GenericRep;
        }


        #region Property

        [ObservableProperty]
        ApplicationUserResponse userModel = new ApplicationUserResponse();
        [ObservableProperty]
        ApplicationUserLoginRequest loginRequest = new ApplicationUserLoginRequest();
        #endregion

        #region RelayCommand

        [RelayCommand]
        public async Task GoSignUpPage()
        {
            await App.Current!.MainPage!.Navigation.PushAsync(new SignUpPage(new SignUpViewModel(Rep)));
        }

        [RelayCommand]
        public async Task GoRestPage()
        {
            await App.Current!.MainPage!.Navigation.PushAsync(new ResetPage());
        }

        [RelayCommand]
        public async Task ClickLogin(ApplicationUserLoginRequest model)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                if (string.IsNullOrEmpty(model?.UserName))
                {
                    var toast = Toast.Make("Please Complete This Field Required : User Name.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
                else if(string.IsNullOrEmpty(model?.Password))
                {
                    var toast = Toast.Make("Please Complete This Field Required : Password.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
                else
                {
                    IsBusy = true;
                    UserDialogs.Instance.ShowLoading();

                    var json = await Rep.PostTRAsync<ApplicationUserLoginRequest, ApplicationUserResponse>(Constants.ApiConstants.LoginApi, model);

                    if (json != null)
                    {
                        UserModel = json;

                        if (!string.IsNullOrEmpty(UserModel?.Id))
                        {
                            var toast = Toast.Make("Successfully for your Login", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                            await toast.Show();

                            Preferences.Default.Set("userid", UserModel?.Id);
                            Preferences.Default.Set("email", UserModel?.Email);
                            Preferences.Default.Set("username", UserModel?.UserName);
                            Preferences.Default.Set("password", UserModel?.Password);
                            Preferences.Default.Set("userPermision", UserModel?.UserPermision);
                            Preferences.Default.Set("userCategory", UserModel?.UserCategory);
                            Preferences.Default.Set("travelAgencyCompanyId", UserModel?.TravelAgencyCompanyId);
                            Preferences.Default.Set("distributorCompanyId", UserModel?.DistributorCompanyId);

                            if (!string.IsNullOrEmpty(UserModel?.TravelAgencyCompanyId) && string.IsNullOrEmpty(UserModel?.DistributorCompanyId))
                            {
                                var vm = new TravelAgenciesViewModels.HomeViewModel();
                                var page = new Pages.TravelAgenciesPages.HomeAgencyPage();
                                page.BindingContext = vm;
                                await App.Current!.MainPage!.Navigation.PushAsync(page);
                            }
                            if (string.IsNullOrEmpty(UserModel?.TravelAgencyCompanyId) && !string.IsNullOrEmpty(UserModel?.DistributorCompanyId))
                            {
                                var vm = new DistributorsViewModels.HomeDistributorViewModel();
                                var page = new Pages.DistributorsPages.HomeDistributorsPage();
                                page.BindingContext = vm;
                                await App.Current!.MainPage!.Navigation.PushAsync(page);
                            }
                        }
                        else
                        {
                            var toast = Toast.Make("Warning, Your user name is not registered !!", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                            await toast.Show();
                            await App.Current!.MainPage!.Navigation.PushAsync(new Pages.LoginPage(new LoginViewModel(Rep)));
                            App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 2]);
                        }
                    }
                    else
                    {
                        var toast = Toast.Make("Don't found this account, call me please.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                        await toast.Show();
                    }

                    UserDialogs.Instance.HideHud();
                    IsBusy = false;
                }
            }
        }

        #endregion
    }
}
