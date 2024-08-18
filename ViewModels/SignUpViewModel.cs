using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Helpers;
using TripBliss.Models;

namespace TripBliss.ViewModels
{
    public class CompanyTypeModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    public partial class SignUpViewModel : BaseViewModel
    {
        readonly IGenericRepository Rep;
        public SignUpViewModel(IGenericRepository GenericRep)
        {
            Rep = GenericRep;
        }

        [ObservableProperty]
        ApplicationUserRequest loginRequest = new ApplicationUserRequest();
        [ObservableProperty]
        ApplicationUserResponse userModel = new ApplicationUserResponse();
        [ObservableProperty]
        ObservableCollection<CompanyTypeModel> lstCompanyType = new ObservableCollection<CompanyTypeModel>
        {
            new CompanyTypeModel() { Id = 1, Name = Preferences.Default.Get("Lan", "en") == "en" ? "Distributor" : "موزع" },
            new CompanyTypeModel() { Id = 2, Name = Preferences.Default.Get("Lan", "en") == "en" ? "Travel Agency" : "شركة سياحة" }
        };
        [ObservableProperty]
        CompanyTypeModel oneCompanyType = new CompanyTypeModel();

        [ObservableProperty]
        string companyName;
        [ObservableProperty]
        string companyPhone;
        [ObservableProperty]
        string companyEmail;


        [RelayCommand]
        public async Task ClickRegister(ApplicationUserRequest model)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                if (OneCompanyType == null)
                {
                    var toast = Toast.Make("Please Complete This Field Required : Company Type.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
                else if (string.IsNullOrEmpty(CompanyName))
                {
                    var toast = Toast.Make("Please Complete This Field Required : Company Name.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
                else if (string.IsNullOrEmpty(CompanyEmail))
                {
                    var toast = Toast.Make("Please Complete This Field Required : Company Email.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
                else if (string.IsNullOrEmpty(CompanyPhone))
                {
                    var toast = Toast.Make("Please Complete This Field Required : Company Phone.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
                else if (string.IsNullOrEmpty(model?.Email))
                {
                    var toast = Toast.Make("Please Complete This Field Required : User Email.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
                else if (string.IsNullOrEmpty(model?.UserName))
                {
                    var toast = Toast.Make("Please Complete This Field Required : User Name.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
                else if (string.IsNullOrEmpty(model?.Password))
                {
                    var toast = Toast.Make("Please Complete This Field Required : Password.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
                else
                {
                    IsBusy = true;
                    UserDialogs.Instance.ShowLoading();

                    if(OneCompanyType?.Id == 1)
                    {
                        model.DistributorCompany = new DistributorCompanyRequest();
                        model.DistributorCompany!.CompanyName = CompanyName;
                        model.DistributorCompany.Email = CompanyEmail;
                        model.DistributorCompany.Phone = CompanyPhone;
                        model.DistributorCompany.Review = 0;
                    }
                    else if(OneCompanyType?.Id == 2)
                    {
                        model.TravelAgencyCompany = new TravelAgencyCompanyRequest();
                        model.TravelAgencyCompany!.CompanyName = CompanyName;
                        model.TravelAgencyCompany.Email = CompanyEmail;
                        model.TravelAgencyCompany.Phone = CompanyPhone;
                        model.TravelAgencyCompany.Review = 0;
                    }

                    model.UserPermision = 1;
                    model.UserCategory = OneCompanyType!.Id;

                    var json = await Rep.PostTRAsync<ApplicationUserRequest, ApplicationUserResponse>(Constants.ApiConstants.RegisterApi, model);

                    if (json != null)
                    {
                        UserModel = json;

                        if (!string.IsNullOrEmpty(UserModel?.Id))
                        {
                            var toast = Toast.Make("Successfully for your Register", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                            await toast.Show();

                            await App.Current!.MainPage!.Navigation.PushAsync(new Pages.LoginPage(new LoginViewModel(Rep)));
                            App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 2]);
                        }
                        else
                        {
                            var toast = Toast.Make("Warning, Your user name is not registered !!", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                            await toast.Show();
                        }
                    }
                    else
                    {
                        var toast = Toast.Make("Warning, Your user name is not registered !!", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                        await toast.Show();
                    }

                    UserDialogs.Instance.HideHud();
                    IsBusy = false;
                }
            }
        }
    }
}
