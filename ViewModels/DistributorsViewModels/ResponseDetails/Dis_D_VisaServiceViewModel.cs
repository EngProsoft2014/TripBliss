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
using TripBliss.ViewModels.ActivateViewModels;

namespace TripBliss.ViewModels.DistributorsViewModels.ResponseDetails
{
    public partial class Dis_D_VisaServiceViewModel : BaseViewModel
    {
        #region Prop
        [ObservableProperty]
        ResponseWithDistributorVisaResponse moddel = new ResponseWithDistributorVisaResponse();
        [ObservableProperty]
        int totalPayment = 0;
        [ObservableProperty]
        bool isRequestHistory;
        #endregion

        #region Services
        IGenericRepository Rep;
        readonly Services.Data.ServicesService _service;
        #endregion
        public Dis_D_VisaServiceViewModel(IGenericRepository generic)
        {
            Rep = generic;

        }
        public Dis_D_VisaServiceViewModel(bool _IsRequestHistory, int payment,ResponseWithDistributorVisaResponse model, IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            Moddel = model;
            TotalPayment = payment;
            IsRequestHistory = _IsRequestHistory;
            Lang = Preferences.Default.Get("Lan", "en");
            _service = service;
        }

        #region RelayCommand
        [RelayCommand]
        async Task Apply()
        {
            bool answer = await App.Current!.MainPage!.DisplayAlert(TripBliss.Resources.Language.AppResources.Question, TripBliss.Resources.Language.AppResources.Are_You_Accept_This_Price, TripBliss.Resources.Language.AppResources.Yes, TripBliss.Resources.Language.AppResources.No);
            Moddel!.AcceptPriceDis = answer;
            await App.Current!.MainPage!.Navigation.PopAsync();
        }
        [RelayCommand]
        async Task BackCLicked()
        {
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
                    var vm = new VisaActivateViewModel(false, IsRequestHistory, Moddel, Rep, _service);
                    var page = new VisaAttachmentsPage(vm);
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
