using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using Mopups.Services;
using System.Collections.ObjectModel;
using TripBliss.Constants;
using TripBliss.Helpers;
using TripBliss.Models;


namespace TripBliss.ViewModels.TravelAgenciesViewModels
{
    public partial class Tr_DocumentsViewModel : BaseViewModel
    {
        [ObservableProperty]
        TravelAgencyCompanyDocResponse doc = new TravelAgencyCompanyDocResponse();
        #region Servises
        IGenericRepository Rep;

        readonly Services.Data.ServicesService _service;
        #endregion

        #region Cons
        public Tr_DocumentsViewModel(IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
            Init();
        }
        #endregion

        #region RelayCommand
        [RelayCommand]
        void OnBackPressed()
        {
            App.Current!.MainPage!.Navigation.PopAsync();
        }

        [RelayCommand]
        async Task OpenFullScreenImage(TravelAgencyCompanyDocResponse model)
        {
            IsBusy = false;
            UserDialogs.Instance.ShowLoading();
            await MopupService.Instance.PushAsync(new Pages.MainPopups.FullScreenImagePopup(model.UploadFile!));
            UserDialogs.Instance.HideHud();
            IsBusy = true;
        }
        [RelayCommand]
        async Task TakePhoto()
        {
            try
            {
                // Check if the camera is available
                if (MediaPicker.Default.IsCaptureSupported)
                {
                    // Capture the photo
                    var photo = await MediaPicker.Default.CapturePhotoAsync();

                    if (photo != null)
                    {
                        // Get the file path to save it
                        var stream = await photo.OpenReadAsync();
                        byte[] bytes = Convert.FromBase64String(Convert.ToBase64String(Helpers.Utility.ReadToEnd(stream)));

                        Doc.UploadFile = Convert.ToBase64String(Helpers.Utility.ReadToEnd(stream));

                    }
                }
                else
                {
                    await App.Current!.MainPage!.DisplayAlert("Error", "Camera not supported on this device.", "OK");
                }
            }
            catch (Exception ex)
            {
                await App.Current!.MainPage!.DisplayAlert("Error", ex.Message, "OK");
            }
        }
        [RelayCommand]
        async Task PickPhoto(TravelAgencyCompanyDocResponse model)
        {
            try
            {
                // Open the photo gallery
                var photo = await MediaPicker.Default.PickPhotoAsync();

                if (photo != null)
                {
                    // Open a stream to read the photo
                    var stream = await photo.OpenReadAsync();
                    byte[] bytes = Convert.FromBase64String(Convert.ToBase64String(Helpers.Utility.ReadToEnd(stream)));
                    Doc.UploadFile = Convert.ToBase64String(Helpers.Utility.ReadToEnd(stream));
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion


        async void Init()
        {
            await GetDocs();

        }

        async Task GetDocs()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();
                if (!string.IsNullOrEmpty(UserToken))
                {
                    string Id = Preferences.Default.Get(ApiConstants.travelAgencyCompanyId,"");
                    UserDialogs.Instance.ShowLoading();
                    var json = await Rep.GetAsync<TravelAgencyCompanyDocResponse>($"{ApiConstants.GetTravelDocApi}{Id}/TravelAgencyCompanyDoc", UserToken);
                    UserDialogs.Instance.HideHud();

                    if (json != null)
                    {
                        Doc = json;
                    }
                }
            }
        }

        [RelayCommand]
        async Task DoneUploadDoc()
        {
            IsBusy = false;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                UserDialogs.Instance.ShowLoading();
                string Id = Preferences.Default.Get(ApiConstants.travelAgencyCompanyId, "");
                

                string UserToken = await _service.UserToken();
                var Postjson = await Rep.PostAsync($"{ApiConstants.PostTravelDocApi}{Id}/TravelAgencyCompanyDoc", Doc!, UserToken);

                UserDialogs.Instance.HideHud();
            }

            IsBusy = true;
        }
    }
}
