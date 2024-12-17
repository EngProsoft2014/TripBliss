using Microsoft.Maui.Controls.Compatibility.Platform.iOS;
using Microsoft.Maui.Controls.Compatibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Platforms.iOS;
using UIKit;

[assembly: ExportRenderer(typeof(ContentPage), typeof(PreventKeyboardDismissPageRenderer))]
namespace TripBliss.Platforms.iOS
{
    public class PreventKeyboardDismissPageRenderer : PageRenderer
    {
        UITapGestureRecognizer _tapGesture;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Add a tap gesture recognizer to the main view
            _tapGesture = new UITapGestureRecognizer()
            {
                CancelsTouchesInView = false, // Allow other gestures like button taps to pass through
                DelaysTouchesBegan = false,
                DelaysTouchesEnded = false
            };

            View.AddGestureRecognizer(_tapGesture);
        }


        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            // Clean up the gesture recognizer
            if (_tapGesture != null)
            {
                View.RemoveGestureRecognizer(_tapGesture);
                _tapGesture.Dispose();
                _tapGesture = null;
            }
        }
    }
}
