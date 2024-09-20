using CommunityToolkit.Maui.Alerts;
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
        ObservableCollection<TravelAgencyCompanyDocResponse> lstDoc = new ObservableCollection<TravelAgencyCompanyDocResponse>();
        #region Servises
        IGenericRepository Rep;

        readonly Services.Data.ServicesService _service;
        #endregion

        #region Cons
        public Tr_DocumentsViewModel(IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
            //Init();
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
        async Task TakePhoto(TravelAgencyCompanyDocResponse model)
        {
            if (string.IsNullOrEmpty(model.NameDoc))
            {
                var toast = Toast.Make("Please Complete This Field Required : File Name.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else
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

                            model.UploadFile = Convert.ToBase64String(Helpers.Utility.ReadToEnd(stream));
                            await DoneUploadDoc(model);

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
            
        }
        [RelayCommand]
        async Task PickPhoto(TravelAgencyCompanyDocResponse model)
        {
            if (string.IsNullOrEmpty(model.NameDoc))
            {
                var toast = Toast.Make("Please Complete This Field Required : File Name.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else
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
                        model.UploadFile = Convert.ToBase64String(Helpers.Utility.ReadToEnd(stream));
                        await DoneUploadDoc(model);
                    }
                }
                catch (Exception ex)
                {

                }
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
                    var json = await Rep.GetAsync<ObservableCollection<TravelAgencyCompanyDocResponse>>($"{ApiConstants.GetTravelDocApi}{Id}/TravelAgencyCompanyDoc", UserToken);
                    UserDialogs.Instance.HideHud();

                    if (json != null)
                    {
                        if (json.Count == 0)
                        {
                            json.Add(new TravelAgencyCompanyDocResponse());
                            json.Add(new TravelAgencyCompanyDocResponse());
                            LstDoc = json;
                        }
                        else if (json.Count == 1)
                        {
                            json.Add(new TravelAgencyCompanyDocResponse());
                            LstDoc = json;
                        }
                        else
                        {
                            LstDoc = json;
                        }
                    }
                }
            }
        }

        async Task DoneUploadDoc(TravelAgencyCompanyDocResponse model)
        {
            IsBusy = false;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                UserDialogs.Instance.ShowLoading();
                string Id = Preferences.Default.Get(ApiConstants.travelAgencyCompanyId, "");
                

                string UserToken = await _service.UserToken();
                var Postjson = await Rep.PostAsync($"{ApiConstants.PostTravelDocApi}{Id}/TravelAgencyCompanyDoc", model!, UserToken);

                UserDialogs.Instance.HideHud();
            }

            IsBusy = true;
        }
    }
}
