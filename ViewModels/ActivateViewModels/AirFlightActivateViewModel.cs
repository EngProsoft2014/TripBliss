using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using Mopups.Services;
using Syncfusion.Maui.Data;
using System.Collections.ObjectModel;
using System.IO;
using TripBliss.Constants;
using TripBliss.Helpers;
using TripBliss.Models;
using static SQLite.SQLite3;


namespace TripBliss.ViewModels.ActivateViewModels
{

    public partial class AirFlightActivateViewModel : BaseViewModel
    {
        #region Prop
        [ObservableProperty]
        ResponseWithDistributorAirFlightResponse model = new ResponseWithDistributorAirFlightResponse();

        [ObservableProperty]
        ResponseWithDistributorAirFlightDetailsResponse activeAirFlight = new ResponseWithDistributorAirFlightDetailsResponse();

        [ObservableProperty]
        ObservableCollection<ResponseWithDistributorAirFlightDetailsResponse> lstAirFlightDetails = new ObservableCollection<ResponseWithDistributorAirFlightDetailsResponse>();
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

        #region Cons
        public AirFlightActivateViewModel(ResponseWithDistributorAirFlightResponse Response, IGenericRepository generic, Services.Data.ServicesService service)
        {
            _service = service;
            Rep = generic;
            Model = Response;
            //GetAllAirFlight(Response.ResponseWithDistributorId, Response.Id);
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
                        var json = await Rep.GetAsync<ObservableCollection<ResponseWithDistributorAirFlightDetailsResponse>>($"api/ResponseWithDistributorAirFlightDetails/ResponseWithDistributor/{Model.ResponseWithDistributorId}/{Model.Id}", UserToken);
                        UserDialogs.Instance.HideHud();

                        if (json != null)
                        {
                            LstAirFlightDetails = json;

                            foreach (var item in LstAirFlightDetails)
                            {
                                item.ImageFile = ImageSource.FromUri(new Uri($"{Helpers.Utility.ServerUrl}{item.UrlImgName}"));
                            }
                        }
                    }
                }
            }
        }

        //async Task GetAllGuests()
        //{
        //    IsBusy = true;

        //    if (Connectivity.NetworkAccess == NetworkAccess.Internet)
        //    {
        //        string TravelAgencyId = Preferences.Default.Get(ApiConstants.travelAgencyCompanyId, "");
        //        string DistributorId = Preferences.Default.Get(ApiConstants.distributorCompanyId, "");
        //        if (!string.IsNullOrEmpty(TravelAgencyId) && string.IsNullOrEmpty(DistributorId))
        //        {
        //            string UserToken = await _service.UserToken();
        //            if (!string.IsNullOrEmpty(UserToken))
        //            {
        //                UserDialogs.Instance.ShowLoading();
        //                var json = await Rep.GetAsync<ObservableCollection<TravelAgencyGuestResponse>>(ApiConstants.GuestApi + $"{TravelAgencyId}/TravelAgencyGuest", UserToken);
        //                UserDialogs.Instance.HideHud();
        //                if (json != null)
        //                {
        //                    Guests!.Clear();
        //                    Guests = json;
        //                    //SelectedGuest = Guests?.FirstOrDefault(g => g.Id == Model.TravelAgencyGuestId) ?? new TravelAgencyGuestResponse();
        //                }
        //            }
        //        }
        //    }

        //    IsBusy = false;
        //}

        [RelayCommand]
        async Task OpenFullScreenImage(ResponseWithDistributorAirFlightDetailsResponse model)
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

                    LstAirFlightDetails.Add(new ResponseWithDistributorAirFlightDetailsResponse
                    {
                        Id = ActiveAirFlight.Id,
                        ResponseWithDistributorAirFlightId = ActiveAirFlight.ResponseWithDistributorAirFlightId,
                        ImgName = img,
                        ImageFile = ImageSource.FromStream(() => new MemoryStream(bytes)),
                    });
                    await MopupService.Instance.PopAsync();

                }
            };

            await MopupService.Instance.PushAsync(page);

            IsBusy = true;
        }



        public async Task GetAllAirFlight(int DisId, int Id)
        {
            IsBusy = false;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string id = Preferences.Default.Get(ApiConstants.distributorCompanyId, "");
                string UserToken = await _service.UserToken();
                if (!string.IsNullOrEmpty(UserToken))
                {
                    UserDialogs.Instance.ShowLoading();
                    var json = await Rep.GetAsync<ResponseWithDistributorAirFlightDetailsResponse>(ApiConstants.AirFlightActive + $"{DisId}/{Id}", UserToken);
                    UserDialogs.Instance.HideHud();
                    if (json != null)
                    {
                        ActiveAirFlight = json;
                    }
                }

            }

            IsBusy = true;
        }

        [RelayCommand]
        async Task DoneUploadPictures()
        {
            IsBusy = false;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                UserDialogs.Instance.ShowLoading();

                List<ResponseWithDistributorAirFlightDetailsRequest> LstAirFltRequest = new List<ResponseWithDistributorAirFlightDetailsRequest>();
                foreach (var item in LstAirFlightDetails)
                {
                    if(item.Id == 0 || item.Id == null)
                    {
                        LstAirFltRequest.Add(new ResponseWithDistributorAirFlightDetailsRequest
                        {
                            ImgFile = Convert.FromBase64String(item.ImgName),
                        });
                    }
                }

                string UserToken = await _service.UserToken();
                string Postjson = await Rep.PostMultiPicAsync($"api/ResponseWithDistributorAirFlightDetails/ResponseWithDistributor/{Model.ResponseWithDistributorId}/{Model.Id}", LstAirFltRequest, UserToken);

                UserDialogs.Instance.HideHud();
            }

            IsBusy = true;
        }

        [RelayCommand]
        async Task DeletePhoto(ResponseWithDistributorAirFlightDetailsResponse model)
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
                        LstAirFlightDetails.Remove(model);
                    }
                    else //Id != 0 (already Photo save)
                    {      
                        IsBusy = false;          
                        UserDialogs.Instance.ShowLoading();
                        string UserToken = await _service.UserToken();
                        var json = await Rep.PostAsync<string>(string.Format($"api/ResponseWithDistributorAirFlightDetails/ResponseWithDistributorAirFlight/{Model.Id}/{model.Id}"),null, UserToken);
                        UserDialogs.Instance.HideHud();
                        if (json == null)
                        {
                            LstAirFlightDetails.Remove(model);
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
                    if (LstAirFlightDetails.Count > 0) //Id = 0 (Photo New)
                    {
                        IsBusy = false;

                        var obj = LstAirFlightDetails.Where(x => x.Id != null && x.Id != 0).FirstOrDefault();
                        if(obj != null)
                        {
                            UserDialogs.Instance.ShowLoading();
                            string UserToken = await _service.UserToken();
                            var json = await Rep.PostAsync<string>(string.Format($"api/ResponseWithDistributorAirFlightDetails/ResponseWithDistributorAirFlight/{Model.Id}"), null, UserToken);
                            UserDialogs.Instance.HideHud();
                            if (json == null)
                            {
                                LstAirFlightDetails.Clear();
                            }
                        }
                        else
                        {
                            LstAirFlightDetails.Clear();
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
