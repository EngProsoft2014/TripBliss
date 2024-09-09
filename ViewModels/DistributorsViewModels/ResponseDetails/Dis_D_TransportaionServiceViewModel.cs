using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.Pages.ActivateDetailsPages;
using TripBliss.ViewModels.ActivateViewModels;

namespace TripBliss.ViewModels.DistributorsViewModels.ResponseDetails
{
    public partial class Dis_D_TransportaionServiceViewModel : BaseViewModel
    {
        #region Prop
        [ObservableProperty]
        ResponseWithDistributorTransportResponse serviceModdel = new ResponseWithDistributorTransportResponse();

        #endregion

        #region Services
        IGenericRepository Rep;
        readonly Services.Data.ServicesService _service;
        #endregion

        #region Const
        public Dis_D_TransportaionServiceViewModel(IGenericRepository generic)
        {
            Rep = generic;
        }
        public Dis_D_TransportaionServiceViewModel(ResponseWithDistributorTransportResponse model, IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
            ServiceModdel = model;
            Lang = Preferences.Default.Get("Lan", "en");
        } 
        #endregion

        #region RelayCommand
        [RelayCommand]
        async Task OnApply()
        {
            if (ServiceModdel != null)
            {
                bool answer = await App.Current!.MainPage!.DisplayAlert("Question?", "Are You Accept This Price?", "Yes", "No");
                ServiceModdel!.AcceptPriceDis = answer;
                await App.Current!.MainPage!.Navigation.PopAsync();
            }
        }

        [RelayCommand]
        async Task OnBackButtonClicked()
        {
            
            await App.Current!.MainPage!.Navigation.PopAsync();
        }
        [RelayCommand]
        async Task ActiveClicked()
        {
            var vm = new MainActivateViewModel(Rep, _service);
            var page = new MainActivatePage(vm);
            page.BindingContext = vm;
            await App.Current!.MainPage!.Navigation.PushAsync(page);
        }
        #endregion
    }
}
