using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Helpers;
using TripBliss.Models.ResponseWithDistributor;

namespace TripBliss.ViewModels.TravelAgenciesViewModels.RequestDetails
{
    public partial class Tr_D_ReviewViewModel : BaseViewModel
    {

        [ObservableProperty]
        ResponseWithDistributorReviewTravelAgentRequest modelReview = new ResponseWithDistributorReviewTravelAgentRequest();


        public delegate void reviewDelegte(ResponseWithDistributorReviewTravelAgentRequest model);
        public event reviewDelegte ReviewClose;

        #region Services
        IGenericRepository Rep;
        readonly Services.Data.ServicesService _service;
        #endregion


        #region Cons
        public Tr_D_ReviewViewModel(IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
        }
        #endregion

        [RelayCommand]
        async Task ApplyReview(ResponseWithDistributorReviewTravelAgentRequest model)
        {
            IsBusy = false;
            if(ModelReview.ReviewToDistributor > 0)
            {
                ReviewClose.Invoke(model);
            }
            else
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Plz_Choose_Rate_of_Review, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            
            IsBusy = true;
        }

        [RelayCommand]
        async Task CancelReview()
        {
            await MopupService.Instance.PopAsync();
        }
    }
}
