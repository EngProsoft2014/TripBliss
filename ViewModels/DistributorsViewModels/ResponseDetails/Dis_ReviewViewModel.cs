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

namespace TripBliss.ViewModels.DistributorsViewModels.ResponseDetails
{
    public partial class Dis_ReviewViewModel : BaseViewModel
    {
        [ObservableProperty]
        ResponseWithDistributorReviewDistributorRequest modelReview = new ResponseWithDistributorReviewDistributorRequest();


        public delegate void reviewDelegte(ResponseWithDistributorReviewDistributorRequest model);
        public event reviewDelegte ReviewClose;

        #region Services
        IGenericRepository Rep;
        readonly Services.Data.ServicesService _service;
        #endregion


        #region Cons
        public Dis_ReviewViewModel(IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
        }
        #endregion

        [RelayCommand]
        async Task ApplyReview(ResponseWithDistributorReviewDistributorRequest model)
        {
            ReviewClose.Invoke(model);
        }

        [RelayCommand]
        async Task CancelReview()
        {
            await MopupService.Instance.PopAsync();
        }
    }
}

