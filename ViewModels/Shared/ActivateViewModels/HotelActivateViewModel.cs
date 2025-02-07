﻿using CommunityToolkit.Maui.Alerts;
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
    public partial class HotelActivateViewModel : BaseViewModel
    {
        #region prop
        [ObservableProperty]
        ResponseWithDistributorHotelDetailsResponse detailsResponse = new ResponseWithDistributorHotelDetailsResponse();
        [ObservableProperty]
        bool isRequestHistory;
        #endregion
        //GuestName
        #region Services
        readonly Services.Data.ServicesService _service;
        IGenericRepository Rep;
        #endregion

        public delegate void HotelDetailsDelegte(ResponseWithDistributorHotelDetailsResponse model);
        public event HotelDetailsDelegte HotelDetailsClose;


        #region Cons
        public HotelActivateViewModel(bool _IsRequestHistoryTR, bool _IsRequestHistoryDS, ResponseWithDistributorHotelDetailsResponse model, IGenericRepository generic, Services.Data.ServicesService service)
        {
            _service = service;
            Rep = generic;
            DetailsResponse = model;
            IsRequestHistory = TOD == "T" ? _IsRequestHistoryTR : _IsRequestHistoryDS;
            //Init();
        } 
        #endregion

        #region RelayCommand
        [RelayCommand]
        async Task ApplyClicked()
        {
            if (Constants.Permissions.CheckPermission(Constants.Permissions.Edit_Service))
            {
                //DetailsResponse.TravelAgencyGuestId = selectedGuest?.Id ?? null;
                HotelDetailsClose.Invoke(DetailsResponse);
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
