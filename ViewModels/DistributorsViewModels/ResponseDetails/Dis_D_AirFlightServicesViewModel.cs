using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.Pages.ActivateDetailsPages;
using TripBliss.Pages.TravelAgenciesPages.ActivateDetailsPages;
using TripBliss.Pages.TravelAgenciesPages.RequestDetails;
using TripBliss.ViewModels.ActivateViewModels;

namespace TripBliss.ViewModels.DistributorsViewModels.ResponseDetails
{
    public partial class Dis_D_AirFlightServicesViewModel : BaseViewModel
    {
        #region prop

        [ObservableProperty]
        ResponseWithDistributorAirFlightResponse moddel = new ResponseWithDistributorAirFlightResponse();
        [ObservableProperty]
        int totalPayment = 0;
        [ObservableProperty]
        bool isRequestHistory;


        #endregion

        #region Services
        IGenericRepository Rep;
        readonly Services.Data.ServicesService _service;
        #endregion
        public Dis_D_AirFlightServicesViewModel(IGenericRepository generic)
        {
            Rep = generic;

        }
        public Dis_D_AirFlightServicesViewModel(bool _IsRequestHistory, int payment,ResponseWithDistributorAirFlightResponse model, IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            Moddel = model;
            _service = service;
            IsRequestHistory = _IsRequestHistory;
            TotalPayment = payment;
        }

        #region RelayCommand
        [RelayCommand]
        async Task OnBackPressed()
        {
            await App.Current!.MainPage!.Navigation.PopAsync();
        }
        [RelayCommand]
        async Task AplyClicked()
        {
            bool answer = await App.Current!.MainPage!.DisplayAlert("Question?", "Are You Accept This Price?", "Yes", "No");
            Moddel!.AcceptPriceDis = answer;
            await App.Current!.MainPage!.Navigation.PopAsync();
        }
        [RelayCommand]
        async Task ActiveClicked()
        {
            if (Constants.Permissions.CheckPermission(Constants.Permissions.Show_Attachments))
            {
                if (TotalPayment == 0)
                {
                    var toast = Toast.Make("Please make sure to pay part of the amount due.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
                else
                {
                    var vm = new AirFlightActivateViewModel(false, IsRequestHistory, Moddel, Rep, _service);
                    var page = new AirFlightAttachmentsPage(vm);
                    page.BindingContext = vm;
                    await App.Current!.MainPage!.Navigation.PushAsync(page);
                }
            }
            else
            {
                var toast = Toast.Make("Permission not allowed for this action.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }

        }
        #endregion
    }
}
