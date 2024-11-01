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
            bool answer = await App.Current!.MainPage!.DisplayAlert(TripBliss.Resources.Language.AppResources.Question, TripBliss.Resources.Language.AppResources.Are_You_Accept_This_Price, TripBliss.Resources.Language.AppResources.Yes, TripBliss.Resources.Language.AppResources.No);
            Moddel!.AcceptPriceDis = (Moddel.PriceAdult + Moddel.PriceChild + Moddel.PriceInfant == 0) ? false : answer;
            Moddel.Total = (Moddel.PriceAdult * Moddel.RequestTravelAgencyAirFlight.InfoAdultCount) + (Moddel.PriceChild * Moddel.RequestTravelAgencyAirFlight.InfoChildCount) + (Moddel.PriceInfant * Moddel.RequestTravelAgencyAirFlight.InfoInfantCount);
            await App.Current!.MainPage!.Navigation.PopAsync();
        }
        [RelayCommand]
        async Task ActiveClicked()
        {
            if (Constants.Permissions.CheckPermission(Constants.Permissions.Show_Attachments))
            {
                if (TotalPayment == 0)
                {
                    var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Agency_must_pay_part_of_the_amount_due, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
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
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.PermissionAlert, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }

        }
        #endregion
    }
}
