using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using Mopups.Services;
using Syncfusion.Maui.DataSource.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TripBliss.Constants;
using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.Models.ResponseWithDistributorVisaDetails;
using TripBliss.Pages;
using TripBliss.Pages.Shared;

namespace TripBliss.ViewModels.ActivateViewModels
{
    public partial class VisaActivateViewModel : BaseViewModel
    {
        #region Prop
        [ObservableProperty]
        ResponseWithDistributorVisaResponse model = new ResponseWithDistributorVisaResponse();

        [ObservableProperty]
        ResponseWithDistributorVisaDetailsResponse activeVisa = new ResponseWithDistributorVisaDetailsResponse();

        [ObservableProperty]
        ObservableCollection<ResponseWithDistributorVisaDetailsResponse> lstVisaDetails = new ObservableCollection<ResponseWithDistributorVisaDetailsResponse>();
        [ObservableProperty]
        ObservableCollection<ResponseWithDistributorVisaDetailsResponse> lstTrVisaDetails = new ObservableCollection<ResponseWithDistributorVisaDetailsResponse>();
        [ObservableProperty]
        ObservableCollection<ResponseWithDistributorVisaDetailsResponse> lstDisVisaDetails = new ObservableCollection<ResponseWithDistributorVisaDetailsResponse>();


        [ObservableProperty]
        ImageSource imageFile;

        [ObservableProperty]
        int isTROrDS;

        [ObservableProperty]
        bool isCheckedTR;

        [ObservableProperty]
        bool isCheckedDS;

        [ObservableProperty]
        bool isAllowEdit;

        [ObservableProperty]
        bool isRequestHistory;
        #endregion

        #region Services
        readonly Services.Data.ServicesService _service;
        IGenericRepository Rep;
        #endregion

        #region Con
        public VisaActivateViewModel(bool _IsRequestHistoryTR, bool _IsRequestHistoryDS, ResponseWithDistributorVisaResponse detailsResponse, IGenericRepository generic, Services.Data.ServicesService service)
        {
            _service = service;
            Rep = generic;
            Model = detailsResponse;
            IsRequestHistory = TOD == "T" ? _IsRequestHistoryTR : _IsRequestHistoryDS;
            Init();
        }
        #endregion

        #region Methods 
        async void Init()
        {
            await GetImage();

            if (TOD == "T")
            {
                await GetTRVisaAttachment();
            }
            else
            {
                await GetDSVisaAttachment();
            }
        }

        async Task GetImage()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {

                if (Model != null)
                {
                    string UserToken = await _service.UserToken();
                    if (!string.IsNullOrEmpty(UserToken))
                    {
                        UserDialogs.Instance.ShowLoading();
                        var json = await Rep.GetAsync<ObservableCollection<ResponseWithDistributorVisaDetailsResponse>>($"{ApiConstants.GetVisaImageApi}{Model.ResponseWithDistributorId}/{Model.Id}", UserToken);
                        UserDialogs.Instance.HideHud();

                        if (json != null)
                        {
                            LstVisaDetails = json;

                            foreach (var item in LstVisaDetails)
                            {
                                item.ImageFile = ImageSource.FromUri(new Uri($"{Helpers.Utility.ServerUrl}{item.UrlImgName}"));
                                item.Extension = $"{Helpers.Utility.ServerUrl}{item.UrlImgName}";
                                if (item.UrlImgName.EndsWith(".pdf"))
                                {
                                    item.Extension = ".pdf";
                                }
                            }
                        }
                    }
                }
            }

        }
        #endregion

        #region RelayCommand
        [RelayCommand]
        async Task GetTRVisaAttachment()
        {
            IsCheckedTR = true;
            IsCheckedDS = false;
            LstTrVisaDetails = new ObservableCollection<ResponseWithDistributorVisaDetailsResponse>(LstVisaDetails.Where(a => !string.IsNullOrEmpty(a.TravelAgencyCompanyName) && string.IsNullOrEmpty(a.DistributorCompanyName)).ToList());
            IsTROrDS = 1;
            if ((IsCheckedTR == true && TOD == "T") || (IsCheckedDS == true && TOD == "D"))
            {
                IsAllowEdit = true;
            }
            else
            {
                IsAllowEdit = false;
            }
        }

        [RelayCommand]
        async Task GetDSVisaAttachment()
        {
            IsCheckedDS = true;
            IsCheckedTR = false;
            LstDisVisaDetails = new ObservableCollection<ResponseWithDistributorVisaDetailsResponse>(LstVisaDetails.Where(a => !string.IsNullOrEmpty(a.DistributorCompanyName) && string.IsNullOrEmpty(a.TravelAgencyCompanyName)).ToList());
            IsTROrDS = 2;
            if ((IsCheckedTR == true && TOD == "T") || (IsCheckedDS == true && TOD == "D"))
            {
                IsAllowEdit = true;
            }
            else
            {
                IsAllowEdit = false;
            }
        }

        [RelayCommand]
        async Task Apply()
        {
            // Model.TravelAgencyGuestId = selectedGuest?.Id ?? 0;
            await App.Current!.MainPage!.Navigation.PopAsync();
        }

        [RelayCommand]
        async Task BackPressed()
        {
            await App.Current!.MainPage!.Navigation.PopAsync();
        }

        [RelayCommand]
        async Task OpenFullScreenImage(ResponseWithDistributorVisaDetailsResponse model)
        {
            if (model.UrlImgName.EndsWith(".pdf"))
            {
                if (string.IsNullOrEmpty(model.UrlImgName))
                {
                    var toast = Toast.Make(TripBliss.Resources.Language.AppResources.OpenfileAlert, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
                else
                {
                    //var vm = new PdfViewerViewModel(model.UrlImgName);
                    //var page = new PdfViewerPage();
                    //page.BindingContext = vm;
                    //await App.Current!.MainPage!.Navigation.PushAsync(page);

                    await App.Current!.MainPage!.Navigation.PushAsync(new PdfViewerPage(model.UrlImgName));
                }
            }
            else
            {
                IsBusy = false;
                //UserDialogs.Instance.ShowLoading();
                await MopupService.Instance.PushAsync(new Pages.MainPopups.FullScreenImagePopup(model.ImageFile!));
                //UserDialogs.Instance.HideHud();
                IsBusy = true;
            }
            
        }

        [RelayCommand]
        async Task OnOpenAddImagesPopup()
        {
            IsBusy = false;

            if (Constants.Permissions.CheckPermission(Constants.Permissions.Add_Attachment))
            {
                Controls.StaticMember.WayOfPhotosPopup = 0; // show all select photos or pdf file 

                var page = new Pages.MainPopups.AddAttachmentsPopup();
                page.ImageClose += async (img, imgPath) =>
                {
                    if (!string.IsNullOrEmpty(img))
                    {
                        byte[] bytes = Convert.FromBase64String(img);

                        if (IsCheckedTR == true && IsCheckedDS == false)
                        {
                            LstVisaDetails.Add(new ResponseWithDistributorVisaDetailsResponse
                            {
                                Id = ActiveVisa.Id,
                                ResponseWithDistributorVisaId = ActiveVisa.ResponseWithDistributorVisaId,
                                ImgName = img,
                                ImageFile = ImageSource.FromStream(() => new MemoryStream(bytes)),
                                TravelAgencyCompanyName = "T",
                                Extension = Path.GetExtension(imgPath),
                            });
                            await GetTRVisaAttachment();
                        }
                        else if (IsCheckedTR == false && IsCheckedDS == true)
                        {
                            LstVisaDetails.Add(new ResponseWithDistributorVisaDetailsResponse
                            {
                                Id = ActiveVisa.Id,
                                ResponseWithDistributorVisaId = ActiveVisa.ResponseWithDistributorVisaId,
                                ImgName = img,
                                ImageFile = ImageSource.FromStream(() => new MemoryStream(bytes)),
                                DistributorCompanyName = "D",
                                Extension = Path.GetExtension(imgPath),
                            });
                            await GetDSVisaAttachment();
                        }
                        await MopupService.Instance.PopAsync();

                    }
                };

                await MopupService.Instance.PushAsync(page);
            }
            else
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.PermissionAlert, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }

            IsBusy = true;
        }


        [RelayCommand]
        async Task DoneUploadPictures()
        {
            IsBusy = false;

            if (Constants.Permissions.CheckPermission(Constants.Permissions.Edit_Attachment))
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    List<ResponseWithDistributorVisaDetailsRequest> LstVisaRequest = new List<ResponseWithDistributorVisaDetailsRequest>();
                    foreach (var item in LstVisaDetails)
                    {
                        if (string.IsNullOrEmpty(item.Id))
                        {
                            LstVisaRequest.Add(new ResponseWithDistributorVisaDetailsRequest
                            {
                                ImgFile = Convert.FromBase64String(item.ImgName),
                                Extension = item.Extension,
                            });
                        }
                    }

                    if(LstVisaRequest.Count > 0)
                    {
                        string UserToken = await _service.UserToken();
                        UserDialogs.Instance.ShowLoading();
                        string Postjson = await Rep.PostMultiPicAsync($"{ApiConstants.PostVisaImageApi}{Model.ResponseWithDistributorId}/{Model.Id}", LstVisaRequest, UserToken);
                        UserDialogs.Instance.HideHud();
                        if (!string.IsNullOrEmpty(Postjson))
                        {
                            Init();
                        }
                    }
                    else
                    {
                        var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Dont_any_change, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                        await toast.Show();
                    }
                    
                }

            }
            else
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.PermissionAlert, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }

            IsBusy = true;
        }

        [RelayCommand]
        async Task DeletePhoto(ResponseWithDistributorVisaDetailsResponse model)
        {
            if (Constants.Permissions.CheckPermission(Constants.Permissions.Delete_Attachment))
            {
                try
                {
                    if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                    {
                        await App.Current!.MainPage!.DisplayAlert(TripBliss.Resources.Language.AppResources.error, TripBliss.Resources.Language.AppResources.No_Internet_connection, TripBliss.Resources.Language.AppResources.OK);
                        return;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(model.Id)) //Id = 0 (Photo New)
                        {
                            LstVisaDetails.Remove(model);
                            if (IsCheckedTR == true && IsCheckedDS == false)
                            {
                                await GetTRVisaAttachment();
                            }
                            else if (IsCheckedTR == false && IsCheckedDS == true)
                            {
                                await GetDSVisaAttachment();
                            }
                        }
                        else //Id != 0 (already Photo save)
                        {
                            IsBusy = false;
                            UserDialogs.Instance.ShowLoading();
                            string UserToken = await _service.UserToken();
                            var json = await Rep.PostAsync<string>(string.Format($"{ApiConstants.DeleteVisaImageApi}{Model.Id}/{model.Id}"), null, UserToken);
                            UserDialogs.Instance.HideHud();
                            if (json == null)
                            {
                                LstVisaDetails.Remove(model);
                                if (IsCheckedTR == true && IsCheckedDS == false)
                                {
                                    await GetTRVisaAttachment();
                                }
                                else if (IsCheckedTR == false && IsCheckedDS == true)
                                {
                                    await GetDSVisaAttachment();
                                }
                            }
                            IsBusy = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    await App.Current!.MainPage!.DisplayAlert(TripBliss.Resources.Language.AppResources.error, ex.Message, TripBliss.Resources.Language.AppResources.OK);
                }
            }
            else
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.PermissionAlert, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
        }


        [RelayCommand]
        async Task DeleteAllPhotos()
        {
            if (Constants.Permissions.CheckPermission(Constants.Permissions.Delete_Attachment))
            {
                try
                {
                    if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                    {
                        await App.Current!.MainPage!.DisplayAlert(TripBliss.Resources.Language.AppResources.error, TripBliss.Resources.Language.AppResources.No_Internet_connection, TripBliss.Resources.Language.AppResources.OK);
                        return;
                    }
                    else
                    {
                        if (LstVisaDetails.Count > 0) //Id = 0 (Photo New)
                        {
                            IsBusy = false;
                            bool ans = await App.Current!.MainPage!.DisplayAlert(TripBliss.Resources.Language.AppResources.Info, TripBliss.Resources.Language.AppResources.Do_you_agree_to_delete_all_photos, TripBliss.Resources.Language.AppResources.OK, TripBliss.Resources.Language.AppResources.Cancel);
                            var obj = LstVisaDetails.Where(x => !string.IsNullOrEmpty(x.Id)).FirstOrDefault();// old photos
                            if (ans)
                            {
                                if (obj != null)
                                {
                                    int UserCategory = Preferences.Default.Get(ApiConstants.userCategory, 0);
                                    UserDialogs.Instance.ShowLoading();
                                    string UserToken = await _service.UserToken();
                                    var json = await Rep.PostAsync<string>(string.Format($"{ApiConstants.DeleteMultiVisaImageApi}{Model.Id}/UserCategory/{UserCategory}"), null, UserToken);
                                    UserDialogs.Instance.HideHud();
                                    if (json == null)
                                    {
                                        if (TOD == "T")
                                        {
                                            LstTrVisaDetails.ForEach(f => LstVisaDetails.Remove(f));
                                            LstTrVisaDetails.Clear();
                                        }
                                        else
                                        {
                                            LstDisVisaDetails.ForEach(f => LstVisaDetails.Remove(f));
                                            LstDisVisaDetails.Clear();
                                        }
                                    }
                                }
                                else
                                {
                                    if (TOD == "T")
                                    {
                                        LstTrVisaDetails.ForEach(f => LstVisaDetails.Remove(f));
                                        LstTrVisaDetails.Clear();
                                    }
                                    else
                                    {
                                        LstDisVisaDetails.ForEach(f => LstVisaDetails.Remove(f));
                                        LstDisVisaDetails.Clear();
                                    }
                                }
                            }

                            IsBusy = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    await App.Current!.MainPage!.DisplayAlert(TripBliss.Resources.Language.AppResources.error, ex.Message, TripBliss.Resources.Language.AppResources.OK);
                }
            }
            else
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.PermissionAlert, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
        }

        #endregion
    }
}
