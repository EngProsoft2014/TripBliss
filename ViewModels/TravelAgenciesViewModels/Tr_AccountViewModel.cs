using Akavache;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Constants;
using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.Pages.MainPopups;
using TripBliss.Pages.Shared;

namespace TripBliss.ViewModels.TravelAgenciesViewModels
{
    public partial class Tr_AccountViewModel : BaseViewModel
    {
        #region Prop
        [ObservableProperty]
        TravelAgencyCompanyResponse companyResponse;
        [ObservableProperty]
        TravelAgencyCompanyRequest companyRequest;
        #endregion

        #region Services
        readonly Services.Data.ServicesService _service;
        IGenericRepository Rep;
        #endregion

        #region Cons
        public Tr_AccountViewModel(IGenericRepository generic, Services.Data.ServicesService service)
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
            IsBusy = false;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string id = Preferences.Default.Get(ApiConstants.travelAgencyCompanyId, "");
                string UserToken = await _service.UserToken();
                if (!string.IsNullOrEmpty(UserToken))
                {
                    UserDialogs.Instance.ShowLoading();
                    var json = await Rep.GetAsync<TravelAgencyCompanyResponse>(ApiConstants.GetTravelCompanyDetailsApi + id, UserToken);
                    UserDialogs.Instance.HideHud();
                    if (json != null)
                    {
                        json.ImageFile = ImageSource.FromUri(new Uri($"{Helpers.Utility.ServerUrl}{json.UrlLogo}"));
                        CompanyResponse = json;

                    }
                }

            }

            IsBusy = true;
        }

        #endregion

        #region RelayCommand

        [RelayCommand]
        async Task OpenFullScreenImage()
        {
            IsBusy = false;
            UserDialogs.Instance.ShowLoading();
            await MopupService.Instance.PushAsync(new Pages.MainPopups.FullScreenImagePopup(CompanyResponse.ImageFile!));
            UserDialogs.Instance.HideHud();
            IsBusy = true;
        }
        [RelayCommand]
        async Task ConfirmeData()
        {
            IsBusy = false;

            string Result = "";
            if (TOD == "T")
            {
                Result = Constants.Permissions.UpdateTravelAgencyCompanyAccount;
            }
            else
            {
                Result = Constants.Permissions.UpdateDistributorCompanyAccount;
            }

            if (Constants.Permissions.CheckPermission(Result))
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    CompanyRequest = new TravelAgencyCompanyRequest
                    {
                        CompanyName = CompanyResponse.CompanyName,
                        Extension = CompanyResponse.Extension,
                        Logo = CompanyResponse.Logo,
                        Address = CompanyResponse.Address,
                        Email = CompanyResponse.Email,
                        ExpireDateAcc = CompanyResponse.ExpireDateAcc,
                        Phone = CompanyResponse.Phone,
                        Review = CompanyResponse.Review,
                        ImgFile = CompanyResponse.ImgFile,
                        ShowAllDistributors = CompanyResponse.ShowAllDistributors,
                        Website = CompanyResponse.Website,

                    };
                    string UserToken = await _service.UserToken();

                    string id = Preferences.Default.Get(ApiConstants.travelAgencyCompanyId, "");
                    var json = await Rep.PutAsync<TravelAgencyCompanyRequest>(ApiConstants.PutTravelCompanyDetailsApi + id, CompanyRequest, UserToken);

                    if (json != null)
                    {
                        await GetCompanyDetiles();
                    }
                }
            }
            else
            {
                var toast = Toast.Make("Permission not allowed for this action.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
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

            Controls.StaticMember.WayOfPhotosPopup = 1; //Only Account page Remove select pdf 

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

            bool answer = await App.Current!.MainPage!.DisplayAlert("Question?", "Do you Want to Delete Company Account?", "Yes", "No");
            if (answer)
            {
                IsBusy = false;
                UserDialogs.Instance.ShowLoading();

                string UserToken = await _service.UserToken();

                var json = await Rep.PutAsync<string>(ApiConstants.PutTRCompanyAccountDelete + CompanyResponse.Id + "/ToggleActive", null, UserToken);

                if (string.IsNullOrEmpty(json))
                {
                    var toast = Toast.Make("Successfully, for company deleted.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();

                    Preferences.Default.Clear();
                    await BlobCache.LocalMachine.InvalidateAll();
                    await BlobCache.LocalMachine.Vacuum();
                    Constants.Permissions.LstPermissions.Clear();
                    await Application.Current!.MainPage!.Navigation.PushAsync(new LoginPage(new LoginViewModel(Rep, _service)));
                }
                else
                {
                    var toast = Toast.Make("Error Please Try again.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }

                UserDialogs.Instance.HideHud();
                IsBusy = true;
            }

        }
        #endregion
    }
}
