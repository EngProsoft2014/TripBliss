using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using Plugin.FirebasePushNotifications;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.Pages.Shared;

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
        readonly Services.Data.ServicesService _service;
        readonly Services.Data.SignalRService _signalRService;
        readonly Services.Data.INotificationService _notificationService;
        readonly IFirebasePushNotification _firebasePushNotification;

        [ObservableProperty]
        ApplicationUserRequest loginRequest = new ApplicationUserRequest();
        [ObservableProperty]
        ApplicationUserResponse userModel = new ApplicationUserResponse();
        [ObservableProperty]
        CompanyTypeModel oneCompanyType = new CompanyTypeModel();

        [ObservableProperty]
        string companyName;
        [ObservableProperty]
        string companyPhone;

        public SignUpViewModel(IGenericRepository GenericRep, Services.Data.ServicesService service, Services.Data.SignalRService signalRService, Services.Data.INotificationService notificationService, IFirebasePushNotification firebasePushNotification)
        {
            Rep = GenericRep;
            _service = service;
            _signalRService = signalRService;
            _notificationService = notificationService;
        }


        [ObservableProperty]
        ObservableCollection<CompanyTypeModel> lstCompanyType = new ObservableCollection<CompanyTypeModel>
        {
            new CompanyTypeModel() { Id = 2, Name = Preferences.Default.Get("Lan", "en") == "en" ? "Travel Agency" : "شركة سياحة" },
            new CompanyTypeModel() { Id = 3, Name = Preferences.Default.Get("Lan", "en") == "en" ? "Distributor" : "موزع" }
        };

        //public static async Task<ObservableCollection<Country>> GetCountriesAsync()
        //{
        //    var assembly = Assembly.GetExecutingAssembly();

        //    using Stream stream = assembly.GetManifestResourceStream("TripBliss.Resources.Data.countries.json");

        //    using StreamReader reader = new StreamReader(stream);

        //    var json = await reader.ReadToEndAsync();

        //    return JsonSerializer.Deserialize<ObservableCollection<Country>>(json)!;
        //}

        [RelayCommand]
        public async Task ClickRegister(ApplicationUserRequest model)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                if (OneCompanyType == null || OneCompanyType.Id == 0)
                {
                    var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_CompanyType, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
                else if (string.IsNullOrEmpty(CompanyName))
                {
                    var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_CompanyName, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }   
                else if (string.IsNullOrEmpty(CompanyPhone))
                {
                    var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_CompanyPhone, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
                else if (string.IsNullOrEmpty(model?.Email))
                {
                    var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_UserEmail, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
                else if (string.IsNullOrEmpty(model?.UserName))
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

                    if (OneCompanyType?.Id == 3)
                    {
                        model.DistributorCompany = new DistributorCompanyRequest();
                        model.DistributorCompany!.CompanyName = CompanyName;
                        model.DistributorCompany.Email = null;
                        model.DistributorCompany.Phone = CompanyPhone;
                        model.DistributorCompany.Review = 0;
                    }
                    else if (OneCompanyType?.Id == 2)
                    {
                        model.TravelAgencyCompany = new TravelAgencyCompanyRequest();
                        model.TravelAgencyCompany!.CompanyName = CompanyName;
                        model.TravelAgencyCompany.Email = null;
                        model.TravelAgencyCompany.Phone = CompanyPhone;
                        model.TravelAgencyCompany.Review = 0;
                    }

                    model.UserPermision = 1;
                    model.UserCategory = OneCompanyType!.Id;

                    var json = await Rep.PostTRAsync<ApplicationUserRequest, ApplicationUserResponse>(Constants.ApiConstants.RegisterApi, model);

                    if (json.Item1 != null && json.Item2 == null)
                    {
                        UserModel = json.Item1;

                        if (!string.IsNullOrEmpty(UserModel?.Id))
                        {
                            var toast = Toast.Make(TripBliss.Resources.Language.AppResources.RegisterSuccessfully, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                            await toast.Show();

                            await App.Current!.MainPage!.Navigation.PushAsync(new LoginPage(new LoginViewModel(Rep, _service, _signalRService, _notificationService, _firebasePushNotification)));
                            App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 2]);
                        }
                        else
                        {
                            var toast = Toast.Make(TripBliss.Resources.Language.AppResources.user_name_is_not_registered, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                            await toast.Show();
                        }
                    }
                    else
                    {
                        if (json.Item2 != null)
                        {
                            if (json.Item2.errors != null)
                            {
                                StringBuilder builder = new StringBuilder();
                                foreach (var str in json.Item2!.errors!)
                                {
                                    builder.Append(str.Key + " " + str.Value);
                                }
                                var toast = Toast.Make($"{builder.ToString()}", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                                await toast.Show();
                            }
                        }
                    }

                    UserDialogs.Instance.HideHud();
                    IsBusy = false;
                }
            }

        }
    }
}
