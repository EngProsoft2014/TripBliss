using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TripBliss.Constants;
using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.Models.ResponseWithDistributorVisaDetails;

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
        ObservableCollection<TravelAgencyGuestResponse> guests = new ObservableCollection<TravelAgencyGuestResponse>();
        [ObservableProperty]
        TravelAgencyGuestResponse selectedGuest = new TravelAgencyGuestResponse();

        [ObservableProperty]
        ImageSource imageFile;
        #endregion

        #region Services
        readonly Services.Data.ServicesService _service;
        IGenericRepository Rep;
        #endregion

        #region Con
        public VisaActivateViewModel(ResponseWithDistributorVisaResponse detailsResponse, IGenericRepository generic, Services.Data.ServicesService service)
        {
            _service = service;
            Rep = generic;
            Model = detailsResponse;
            Init();
        }
        #endregion

        #region RelayCommand
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
        #endregion

        #region Methods
        async void Init()
        {
            await GetImage();
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
                            }
                        }
                    }
                }
            }
        }

        

        [RelayCommand]
        async Task OpenFullScreenImage(ResponseWithDistributorVisaDetailsResponse model)
        {
            IsBusy = false;
            UserDialogs.Instance.ShowLoading();
            await MopupService.Instance.PushAsync(new Pages.MainPopups.FullScreenImagePopup(model.ImageFile!));
            UserDialogs.Instance.HideHud();
            IsBusy = true;
        }

        [RelayCommand]
        async Task OnOpenAddImagesPopup()
        {
            IsBusy = false;

            var page = new Pages.MainPopups.AddAttachmentsPopup();
            page.ImageClose += async (img) =>
            {
                if (!string.IsNullOrEmpty(img))
                {
                    byte[] bytes = Convert.FromBase64String(img);

                    LstVisaDetails.Add(new ResponseWithDistributorVisaDetailsResponse
                    {
                        Id = ActiveVisa.Id,
                        ResponseWithDistributorVisaId = ActiveVisa.ResponseWithDistributorVisaId,
                        ImgName = img,
                        ImageFile = ImageSource.FromStream(() => new MemoryStream(bytes)),
                    });
                    await MopupService.Instance.PopAsync();

                }
            };

            await MopupService.Instance.PushAsync(page);

            IsBusy = true;
        }


        [RelayCommand]
        async Task DoneUploadPictures()
        {
            IsBusy = false;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                UserDialogs.Instance.ShowLoading();

                List<ResponseWithDistributorVisaDetailsRequest> LstVisaRequest = new List<ResponseWithDistributorVisaDetailsRequest>();
                foreach (var item in LstVisaDetails)
                {
                    if (item.Id == 0 || item.Id == null)
                    {
                        LstVisaRequest.Add(new ResponseWithDistributorVisaDetailsRequest
                        {
                            ImgFile = Convert.FromBase64String(item.ImgName),
                        });
                    }
                }

                string UserToken = await _service.UserToken();
                string Postjson = await Rep.PostMultiPicAsync($"{ApiConstants.PostVisaImageApi}{Model.ResponseWithDistributorId}/{Model.Id}", LstVisaRequest, UserToken);

                UserDialogs.Instance.HideHud();
            }

            IsBusy = true;
        }

        [RelayCommand]
        async Task DeletePhoto(ResponseWithDistributorVisaDetailsResponse model)
        {
            try
            {
                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await App.Current!.MainPage!.DisplayAlert("Error", "No Internet connection!", "OK");
                    return;
                }
                else
                {
                    if (model.Id == 0 || model.Id == null) //Id = 0 (Photo New)
                    {
                        LstVisaDetails.Remove(model);
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
                        }
                        IsBusy = true;
                    }
                }
            }
            catch (Exception ex)
            {
                await App.Current!.MainPage!.DisplayAlert("Error", ex.Message, "OK");
            }
        }


        [RelayCommand]
        async Task DeleteAllPhotos()
        {
            try
            {
                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await App.Current!.MainPage!.DisplayAlert("Error", "No Internet connection!", "OK");
                    return;
                }
                else
                {
                    if (LstVisaDetails.Count > 0) //Id = 0 (Photo New)
                    {
                        IsBusy = false;
                        bool ans = await App.Current!.MainPage!.DisplayAlert("Info", "Do you agree to delete all photos?","OK","Cancel");
                        var obj = LstVisaDetails.Where(x => x.Id != null && x.Id != 0).FirstOrDefault();
                        if (ans)
                        {
                            if (obj != null)
                            {
                                UserDialogs.Instance.ShowLoading();
                                string UserToken = await _service.UserToken();
                                var json = await Rep.PostAsync<string>(string.Format($"{ApiConstants.DeleteMultiVisaImageApi}{Model.Id}"), null, UserToken);
                                UserDialogs.Instance.HideHud();
                                if (json == null)
                                {
                                    LstVisaDetails.Clear();
                                }
                            }
                            else
                            {
                                LstVisaDetails.Clear();
                            }
                        }

                        IsBusy = true;
                    }
                }
            }
            catch (Exception ex)
            {
                await App.Current!.MainPage!.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        #endregion
    }
}
