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
using TripBliss.Pages.Shared;
using TripBliss.Services;


namespace TripBliss.ViewModels.DistributorsViewModels
{
    public partial class Dis_DocumentsViewModel : BaseViewModel
    {
        [ObservableProperty]
        ObservableCollection<DistributorCompanyDocResponse> lstDoc = new ObservableCollection<DistributorCompanyDocResponse>();
        [ObservableProperty]
        int reviewVM;

        #region Servises
        IGenericRepository Rep;

        readonly Services.Data.ServicesService _service;
        #endregion

        #region Cons
        public Dis_DocumentsViewModel(IGenericRepository generic, Services.Data.ServicesService service)
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
        async Task OpenFullScreenImage(DistributorCompanyDocResponse model)
        {
            if (model!.UrlUploadFile!.Contains(".pdf"))
            {
                //var vm = new PdfViewerViewModel(model.UrlUploadFile);
                //var page = new PdfViewerPage();
                //page.BindingContext = vm;
                //await App.Current!.MainPage!.Navigation.PushAsync(page);

                await App.Current!.MainPage!.Navigation.PushAsync(new PdfViewerPage(model.UrlUploadFile));
            }
            else
            {
                if (string.IsNullOrEmpty(model.UrlUploadFile))
                {
                    var toast = Toast.Make(TripBliss.Resources.Language.AppResources.No_image_here, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
                else
                {
                    ImageSource sou = ImageSource.FromUri(new Uri(model.UrlUploadFile!)); 
                    IsBusy = false;
                    UserDialogs.Instance.ShowLoading();
                    await MopupService.Instance.PushAsync(new Pages.MainPopups.FullScreenImagePopup(sou));
                    UserDialogs.Instance.HideHud();
                    IsBusy = true;
                }
                
            }
            
        }

        [RelayCommand]
        async Task TakePhoto(DistributorCompanyDocResponse model)
        {
            if (Constants.Permissions.CheckPermission(Constants.Permissions.Edit_Documents))
            {
                if (string.IsNullOrEmpty(model.NameDoc))
                {
                    var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_FileName, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
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
                            await App.Current!.MainPage!.DisplayAlert(TripBliss.Resources.Language.AppResources.error, TripBliss.Resources.Language.AppResources.Camera_not_supported, TripBliss.Resources.Language.AppResources.OK);
                        }
                    }
                    catch (Exception ex)
                    {
                        await App.Current!.MainPage!.DisplayAlert(TripBliss.Resources.Language.AppResources.error, ex.Message, TripBliss.Resources.Language.AppResources.OK);
                    }
                }
            }
            else
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.PermissionAlert, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }

        }
        [RelayCommand]
        async Task PickPhoto(DistributorCompanyDocResponse model)
        {
            if (Constants.Permissions.CheckPermission(Constants.Permissions.Edit_Documents))
            {
                if (string.IsNullOrEmpty(model.NameDoc))
                {
                    var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_FileName, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
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
            else
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.PermissionAlert, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }

        }
        [RelayCommand]
        private async Task PickPdf(DistributorCompanyDocResponse model)
        {
            if (Constants.Permissions.CheckPermission(Constants.Permissions.Edit_Documents))
            {
                if (string.IsNullOrEmpty(model.NameDoc))
                {
                    var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_FileName, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
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
                        await App.Current!.MainPage!.DisplayAlert(TripBliss.Resources.Language.AppResources.error, $"{ex.Message}", TripBliss.Resources.Language.AppResources.OK);
                    }
                }
            }
            else
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.PermissionAlert, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
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
                    string Id = Preferences.Default.Get(ApiConstants.distributorCompanyId, "") ;
                    string uri = $"{ApiConstants.GetDistDocApi}{Id}/DistributorCompanyDoc";
                    
                    var json = await Rep.GetAsync<ObservableCollection<DistributorCompanyDocResponse>>(uri, UserToken);                    

                    if (json != null)
                    {
                        if (json.Count == 0)
                        {
                            json.Add(new DistributorCompanyDocResponse());
                            json.Add(new DistributorCompanyDocResponse());
                            LstDoc = json;
                        }
                        else if (json.Count == 1)
                        {
                            json[0].UrlUploadFile = $"{Helpers.Utility.ServerUrl}{json[0].UrlUploadFile}";
                            if (json[0].UrlUploadFile.EndsWith(".pdf"))
                            {
                                json[0].Extension = "pdf";
                            }
                            json.Add(new DistributorCompanyDocResponse());
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
                UserDialogs.Instance.HideHud();
            }

        }

        async Task DoneUploadDoc(DistributorCompanyDocResponse model)
        {
            IsBusy = false;
            
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string Id = Preferences.Default.Get(ApiConstants.distributorCompanyId, "");
                string uri = $"{ApiConstants.GetTravelDocApi}{Id}/DistributorCompanyDoc";

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
