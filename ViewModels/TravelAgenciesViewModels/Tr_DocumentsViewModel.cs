using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using Mopups.Services;
using SkiaSharp;
using Syncfusion.Pdf;
using System.Collections.ObjectModel;
using System.Text;
using TripBliss.Constants;
using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.Pages;
using TripBliss.Pages.Shared;
using TripBliss.Services;


namespace TripBliss.ViewModels.TravelAgenciesViewModels
{
    public partial class Tr_DocumentsViewModel : BaseViewModel
    {
        [ObservableProperty]
        ObservableCollection<TravelAgencyCompanyDocResponse> lstDoc = new ObservableCollection<TravelAgencyCompanyDocResponse>();
        [ObservableProperty]
        int reviewVM;

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
            Application.Current!.MainPage!.Navigation.PopAsync();
        }

        [RelayCommand]
        async Task OpenFullScreenImage(TravelAgencyCompanyDocResponse model)
        {
            if (model!.UrlUploadFile!.Contains(".pdf"))
            {
                //var vm = new PdfViewerViewModel(model.UrlUploadFile);
                //var page = new PdfViewerPage();
                //page.BindingContext = vm;
                //await App.Current!.MainPage!.Navigation.PushAsync(page);

                await Application.Current!.MainPage!.Navigation.PushAsync(new PdfViewerPage(model.UrlUploadFile));
            }
            else
            {
                if (string.IsNullOrEmpty(model.UrlUploadFile))
                {
                    var toast = Toast.Make(Resources.Language.AppResources.No_image_here, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
                else
                {
                    ImageSource sou = ImageSource.FromUri(new Uri(model.UrlUploadFile!));
                    IsBusy = false;
                    //UserDialogs.Instance.ShowLoading();
                    await App.Current!.MainPage!.Navigation.PushAsync(new Pages.MainPopups.FullScreenImagePopup(sou));
                    //UserDialogs.Instance.HideHud();
                    IsBusy = true;
                }

            }

        }

        [RelayCommand]
        async Task TakePhoto(TravelAgencyCompanyDocResponse model)
        {
            if (Constants.Permissions.CheckPermission(Constants.Permissions.Edit_Documents))
            {
                if (string.IsNullOrEmpty(model.NameDoc))
                {
                    var toast = Toast.Make(Resources.Language.AppResources.Required_FileName, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
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
                                using var stream = await photo.OpenReadAsync();
                                using var memoryStream = new MemoryStream();

                                // Load the image into SkiaSharp and resize it
                                using var originalBitmap = SKBitmap.Decode(stream);
                                var resizedBitmap = originalBitmap.Resize(new SKImageInfo(800, 600), SKFilterQuality.Medium);

                                using var image = SKImage.FromBitmap(resizedBitmap);
                                using var data = image.Encode(SKEncodedImageFormat.Jpeg, 75); // Compression level: 75%
                                data.SaveTo(memoryStream);

                                // Convert the compressed image to Base64
                                model.ImgFile = Convert.ToBase64String(memoryStream.ToArray());
                                model.Extension = Path.GetExtension(photo.FullPath);

                                await DoneUploadDoc(model);
                            }
                        }
                        else
                        {
                            await Application.Current!.MainPage!.DisplayAlert(Resources.Language.AppResources.error, Resources.Language.AppResources.Camera_not_supported, Resources.Language.AppResources.OK);
                        }
                    }
                    catch (Exception ex)
                    {
                        await Application.Current!.MainPage!.DisplayAlert(Resources.Language.AppResources.error, ex.Message, Resources.Language.AppResources.OK);
                    }
                }
            }
            else
            {
                var toast = Toast.Make(Resources.Language.AppResources.PermissionAlert, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }

        }
        [RelayCommand]
        async Task PickPhoto(TravelAgencyCompanyDocResponse model)
        {
            if (Constants.Permissions.CheckPermission(Constants.Permissions.Edit_Documents))
            {
                if (string.IsNullOrEmpty(model.NameDoc))
                {
                    var toast = Toast.Make(Resources.Language.AppResources.Required_FileName, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
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
                            using var stream = await photo.OpenReadAsync();
                            using var memoryStream = new MemoryStream();

                            // Load the image into SkiaSharp and resize it
                            using var originalBitmap = SKBitmap.Decode(stream);
                            var resizedBitmap = originalBitmap.Resize(new SKImageInfo(800, 600), SKFilterQuality.Medium);

                            using var image = SKImage.FromBitmap(resizedBitmap);
                            using var data = image.Encode(SKEncodedImageFormat.Jpeg, 75); // Compression level: 75%
                            data.SaveTo(memoryStream);

                            // Convert the compressed image to Base64
                            model.ImgFile = Convert.ToBase64String(memoryStream.ToArray());
                            model.Extension = Path.GetExtension(photo.FullPath);

                            await DoneUploadDoc(model);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            else
            {
                var toast = Toast.Make(Resources.Language.AppResources.PermissionAlert, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }

        }
        [RelayCommand]
        private async Task PickPdf(TravelAgencyCompanyDocResponse model)
        {
            if (Constants.Permissions.CheckPermission(Constants.Permissions.Edit_Documents))
            {
                if (string.IsNullOrEmpty(model.NameDoc))
                {
                    var toast = Toast.Make(Resources.Language.AppResources.Required_FileName, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
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
                            model.ImgFile = Convert.ToBase64String(Utility.ReadToEnd(stream));
                            model.Extension = ".pdf";
                            await DoneUploadDoc(model);
                        }
                    }
                    catch (Exception ex)
                    {
                        await Application.Current!.MainPage!.DisplayAlert(Resources.Language.AppResources.error, $"{ex.Message}", Resources.Language.AppResources.OK);
                    }
                }
            }
            else
            {
                var toast = Toast.Make(Resources.Language.AppResources.PermissionAlert, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }

        }
        #endregion

        #region Methods
        async void Init()
        {
            ReviewVM = Preferences.Default.Get(ApiConstants.review, int.Parse("0"));
            await GetDocs();

        }

        async Task GetDocs() // TravelAgencyCompanyDocResponse used for Distributors and Travel Agency
        {

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();
                UserDialogs.Instance.ShowLoading();
                if (!string.IsNullOrEmpty(UserToken))
                {
                    string Id = Preferences.Default.Get(ApiConstants.travelAgencyCompanyId, "");
                    string uri = $"{ApiConstants.GetTravelDocApi}{Id}/TravelAgencyCompanyDoc";

                    var json = await Rep.GetAsync<ObservableCollection<TravelAgencyCompanyDocResponse>>(uri, UserToken);

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
                            json[0].UrlUploadFile = $"{Utility.ServerUrl}{json[0].UrlUploadFile}";
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
                                item.UrlUploadFile = $"{Utility.ServerUrl}{item.UrlUploadFile}";
                                if (item.UrlUploadFile.EndsWith(".pdf"))
                                {
                                    item.Extension = "pdf";
                                }
                            }
                        }
                    }
                }
                UserDialogs.Instance.HideHud();
            }

        }

        async Task DoneUploadDoc(TravelAgencyCompanyDocResponse model)
        {
            IsBusy = false;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {

                string Id = Preferences.Default.Get(ApiConstants.travelAgencyCompanyId, "");
                string uri = $"{ApiConstants.GetTravelDocApi}{Id}/TravelAgencyCompanyDoc";

                string UserToken = await _service.UserToken();

                UserDialogs.Instance.ShowLoading();
                var Postjson = await Rep.PostAsync(uri, model!, UserToken);
                UserDialogs.Instance.HideHud();

                if (!string.IsNullOrEmpty(Postjson.Id))
                {
                    await GetDocs();
                }
            }

            IsBusy = true;
        }
        #endregion
    }
}
