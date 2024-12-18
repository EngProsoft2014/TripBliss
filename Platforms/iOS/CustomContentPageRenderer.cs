using Microsoft.Maui.Controls.Compatibility.Platform.iOS;

using Foundation;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Platform.iOS;
using UIKit;
using TripBliss.Platforms.iOS;

[assembly: Microsoft.Maui.Controls.Compatibility.ExportRenderer(typeof(ContentPage), typeof(CustomContentPageRenderer))]
namespace TripBliss.Platforms.iOS
{
    public class CustomContentPageRenderer : PageRenderer
    {
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            var tapGestureRecognizer = new UITapGestureRecognizer(OnTapped)
            {
                CancelsTouchesInView = false
            };
            View.AddGestureRecognizer(tapGestureRecognizer);
        }

        private void OnTapped()
        {
            View.EndEditing(true);
        }
    }
}
