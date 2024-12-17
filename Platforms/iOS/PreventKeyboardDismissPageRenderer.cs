using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Compatibility.Platform.iOS;
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
    class PreventKeyboardDismissPageRenderer : PageRenderer
    {
        UITapGestureRecognizer _tapGesture;

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            // Create a gesture recognizer that does nothing
            _tapGesture = new UITapGestureRecognizer(() => { /* Do nothing */ })
            {
                CancelsTouchesInView = false, // Allows other gestures to work
                DelaysTouchesBegan = false,
                DelaysTouchesEnded = false
            };

            // Add the gesture recognizer to the root view
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
