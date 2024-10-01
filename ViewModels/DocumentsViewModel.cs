using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using Mopups.Services;
using Syncfusion.Pdf;
using System.Collections.ObjectModel;
using System.Text;
using TripBliss.Constants;
using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.Pages;
using TripBliss.Services;


namespace TripBliss.ViewModels
{
    public partial class DocumentsViewModel : BaseViewModel
    {
        [ObservableProperty]
        ObservableCollection<TravelAgencyCompanyDocResponse> lstDoc = new ObservableCollection<TravelAgencyCompanyDocResponse>();
        [ObservableProperty]
        string reviewVM;

        #region Servises
        IGenericRepository Rep;

        readonly Services.Data.ServicesService _service;
        #endregion

        #region Cons
        public DocumentsViewModel(IGenericRepository generic, Services.Data.ServicesService service)
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
            if (model!.UrlUploadFile!.Contains(".pdf"))
            {
                var vm = new PdfViewerViewModel(model.UrlUploadFile);
                var page = new PdfViewerPage();
                page.BindingContext = vm;
                await App.Current!.MainPage!.Navigation.PushAsync(page);
            }
            else
            {
                if (string.IsNullOrEmpty(model.UrlUploadFile))
                {
                    var toast = Toast.Make("No image here.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
                else
                {
                    ImageSource sou = ImageSource.FromUri(new Uri(model.UrlUploadFile!)); ;
                    IsBusy = false;
                    UserDialogs.Instance.ShowLoading();
                    await MopupService.Instance.PushAsync(new Pages.MainPopups.FullScreenImagePopup(sou));
                    UserDialogs.Instance.HideHud();
                    IsBusy = true;
                }
                
            }
            
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
                            model.ImgFile = Convert.ToBase64String(Helpers.Utility.ReadToEnd(stream));
                            model.Extension = Path.GetExtension(photo.FullPath);
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
                        model.ImgFile = Convert.ToBase64String(Helpers.Utility.ReadToEnd(stream));
                        model.Extension = Path.GetExtension(photo.FullPath);
                        await DoneUploadDoc(model);
                    }
                }
                catch (Exception ex)
                {

                }
            }
            
        }
        [RelayCommand]
        private async Task PickPdf(TravelAgencyCompanyDocResponse model)
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
                    var result = await FilePicker.Default.PickAsync(new PickOptions
                    {
                        PickerTitle = "Select a PDF file",
                        FileTypes = FilePickerFileType.Pdf
                    });

                    if (result != null && result.FileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                    {
                        var stream = await result.OpenReadAsync();
                        model.ImgFile = Convert.ToBase64String(Helpers.Utility.ReadToEnd(stream));
                        model.Extension = ".pdf";
                        await DoneUploadDoc(model);
                    }
                }
                catch (Exception ex)
                {
                    await App.Current!.MainPage!.DisplayAlert("Info", $"Error picking file: {ex.Message}", "OK");
                }
            }
            
        }
        #endregion

        #region Methods
        async void Init()
        {
            ReviewVM = Preferences.Default.Get(ApiConstants.review, "");
            await GetDocs();

        }

        async Task GetDocs() // TravelAgencyCompanyDocResponse used for Distributors and Travel Agency
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();
                if (!string.IsNullOrEmpty(UserToken))
                {
                    string Id = Preferences.Default.Get(ApiConstants.travelAgencyCompanyId, "") ;
                    string uri = $"{ApiConstants.GetTravelDocApi}{Id}/TravelAgencyCompanyDoc";
                    if (Id == "")
                    {
                        Id = Preferences.Default.Get(ApiConstants.distributorCompanyId, "");
                        uri = $"{ApiConstants.GetDistDocApi}{Id}/DistributorCompanyDoc";
                    }
                    UserDialogs.Instance.ShowLoading();
                    var json = await Rep.GetAsync<ObservableCollection<TravelAgencyCompanyDocResponse>>(uri, UserToken);
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
                            json[0].UrlUploadFile = $"{Helpers.Utility.ServerUrl}{json[0].UrlUploadFile}";
                            if (json[0].UrlUploadFile.EndsWith(".pdf"))
                            {
                                json[0].Extension = "pdf";
                            }
                            json.Add(new TravelAgencyCompanyDocResponse());
                            LstDoc = json;
                        }
                        else
                        {
                            LstDoc = json;
                            foreach (var item in LstDoc)
                            {
                                item.UrlUploadFile = $"{Helpers.Utility.ServerUrl}{item.UrlUploadFile}";
                                if (item.UrlUploadFile.EndsWith(".pdf"))
                                {
                                    item.Extension = "pdf";
                                } 
                            }
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
                string uri = $"{ApiConstants.GetTravelDocApi}{Id}/TravelAgencyCompanyDoc";
                if (Id == "")
                {
                    Id = Preferences.Default.Get(ApiConstants.distributorCompanyId, "");
                    uri = $"{ApiConstants.GetDistDocApi}{Id}/DistributorCompanyDoc";
                }
                string UserToken = await _service.UserToken();
                var Postjson = await Rep.PostAsync(uri, model!, UserToken);
                if (Postjson!.Id != null || Postjson.Id != 0)
                {
                    await GetDocs();
                }
                UserDialogs.Instance.HideHud();
            }

            IsBusy = true;
        }
        #endregion
    }
}
