using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Models;

namespace TripBliss.ViewModels.DistributorsViewModels.Offer
{
    partial class CreateOfferViewModel : BaseViewModel
    {
        #region Prop
        [ObservableProperty]
        public OfferModel offer;
        #endregion

        public CreateOfferViewModel()
        {
            Offer = new OfferModel();
            Lang = Preferences.Default.Get("Lan", "en");
        }

        #region RelayCommand
        [RelayCommand]
        async Task BackPressed()
        {
            await App.Current!.MainPage!.Navigation.PopAsync();
        }
        [RelayCommand]
        async Task OpenCamera()
        {
            try
            {
                if (MediaPicker.Default.IsCaptureSupported)
                {
                    FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

                    if (photo != null)
                    {
                        // Do any Thing Here

                    }
                }
                else
                {
                    await Permissions.RequestAsync<Permissions.Camera>();
                }
            }
            catch
            {

            }
        }
        [RelayCommand]
        async Task OpenGallery()
        {
            try
            {

                FileResult photo = await MediaPicker.PickPhotoAsync();

                if (photo != null)
                {
                    // Do any Thing Here

                }
                
            }
            catch
            {

            }
        }
        #endregion
    }
}
