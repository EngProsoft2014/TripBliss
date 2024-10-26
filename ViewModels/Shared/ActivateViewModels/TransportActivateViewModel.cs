using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Constants;
using TripBliss.Helpers;
using TripBliss.Models;

namespace TripBliss.ViewModels.ActivateViewModels
{
    public partial class TransportActivateViewModel : BaseViewModel
    {
        #region Prop
        [ObservableProperty]
        ResponseWithDistributorTransportDetailsResponse model = new ResponseWithDistributorTransportDetailsResponse();
        [ObservableProperty]
        bool isRequestHistory;
        #endregion

        #region Services
        readonly Services.Data.ServicesService _service;
        IGenericRepository Rep;
        #endregion

        public delegate void TransportDetailsDelegte(ResponseWithDistributorTransportDetailsResponse model);
        public event TransportDetailsDelegte TransportDetailsClose;

        #region Cons
        public TransportActivateViewModel(bool _IsRequestHistoryTR, bool _IsRequestHistoryDS, ResponseWithDistributorTransportDetailsResponse detailsResponse, IGenericRepository generic, Services.Data.ServicesService service)
        {
            _service = service;
            Rep = generic;
            Model = detailsResponse;
            IsRequestHistory = TOD == "T" ? _IsRequestHistoryTR : _IsRequestHistoryDS;
            //Init();
        } 
        #endregion

        #region RelayCommand
        [RelayCommand]
        async Task Apply()
        {
            if (Constants.Permissions.CheckPermission(Constants.Permissions.Edit_Service))
            {
                //Model.TravelAgencyGuestId = selectedGuest?.Id ?? 0;
                TransportDetailsClose.Invoke(Model);
                await App.Current!.MainPage!.Navigation.PopAsync();
            }
            else
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.PermissionAlert, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
        }

        [RelayCommand]
        async Task BackClicked()
        {
            await App.Current!.MainPage!.Navigation.PopAsync();
        } 
        #endregion

      
    }
}
