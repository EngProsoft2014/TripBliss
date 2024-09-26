using CommunityToolkit.Maui.Views;
using Mopups.Services;
using System.IO;
using TripBliss.Models;

namespace TripBliss.Pages.MainPopups;

public partial class AddAttachmentsPopup : Mopups.Pages.PopupPage
{
    public delegate void imageDelegte(string img,string imagePath);
    public event imageDelegte ImageClose;

    public AddAttachmentsPopup()
	{
		InitializeComponent();
	}

    private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        await MopupService.Instance.PopAsync();
    }

    private async void TapGestureRecognizer_Tapped_Cam(object sender, TappedEventArgs e)
    {
        try
        {
            // Check if the camera is available
            if (MediaPicker.Default.IsCaptureSupported)
            {
                // Capture the photo
                var photo = await MediaPicker.Default.CapturePhotoAsync();

                if (photo != null)
                {
                    // Get the file path to save it
                    var stream = await photo.OpenReadAsync();

                    // Display the image
                    ImageClose.Invoke(Convert.ToBase64String(Helpers.Utility.ReadToEnd(stream)),photo.FullPath);
                }
            }
            else
            {
                await DisplayAlert("Error", "Camera not supported on this device.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private async void TapGestureRecognizer_Tapped_Pic(object sender, TappedEventArgs e)
    {
        try
        {
            // Open the photo gallery
            var photo = await MediaPicker.Default.PickPhotoAsync();

            if (photo != null)
            {
                // Open a stream to read the photo
                var stream = await photo.OpenReadAsync();

                // Display the selected photo in the Image control
                ImageClose.Invoke(Convert.ToBase64String(Helpers.Utility.ReadToEnd(stream)), photo.FullPath);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}