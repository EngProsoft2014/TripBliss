using CommunityToolkit.Maui.Views;
using Mopups.Services;
using System.IO;


namespace TripBliss.Pages.MainPopups;

public partial class AddAttachmentsPopup : Mopups.Pages.PopupPage
{
    public delegate void imageDelegte(string img,string imagePath);
    public event imageDelegte ImageClose;

    public AddAttachmentsPopup()
	{
		InitializeComponent();
        if(Controls.StaticMember.WayOfPhotosPopup == 1)
        {
            stkpdf.IsVisible = false;
        }
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
                    ImageClose.Invoke(Convert.ToBase64String(Helpers.Utility.ReadToEnd(stream)), photo.FullPath);
                }
            }
            else
            {
                await DisplayAlert(TripBliss.Resources.Language.AppResources.error, TripBliss.Resources.Language.AppResources.Camera_not_supported, TripBliss.Resources.Language.AppResources.OK);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert(TripBliss.Resources.Language.AppResources.error, ex.Message, TripBliss.Resources.Language.AppResources.OK);
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
            await DisplayAlert(TripBliss.Resources.Language.AppResources.error, ex.Message, TripBliss.Resources.Language.AppResources.OK);
        }
    }

    private async void TapGestureRecognizer_Tapped_Pic_pdf(object sender, TappedEventArgs e)
    {
        try
        {
            var result = await FilePicker.Default.PickAsync(new PickOptions
            {
                PickerTitle = "Select a PDF file",
                FileTypes = FilePickerFileType.Pdf
            });

            if (result != null && result.FileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
            {
                // Get the file path to save it
                var stream = await result.OpenReadAsync();

                // Display the image
                ImageClose.Invoke(Convert.ToBase64String(Helpers.Utility.ReadToEnd(stream)), result.FullPath);
            }
        }
        catch (Exception ex)
        {
            await App.Current!.MainPage!.DisplayAlert(TripBliss.Resources.Language.AppResources.error, $"{ex.Message}", TripBliss.Resources.Language.AppResources.OK);
        }
    }
}