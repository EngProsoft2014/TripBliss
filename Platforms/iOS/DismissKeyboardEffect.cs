using Microsoft.Maui.Controls.Compatibility.Platform.iOS;

using Foundation;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Platform.iOS;
using UIKit;
using TripBliss.Platforms.iOS;
using Microsoft.Maui.Controls.Platform;

[assembly: ExportEffect(typeof(DismissKeyboardEffect), nameof(DismissKeyboardEffect))]
namespace TripBliss.Platforms.iOS
{
    public class DismissKeyboardEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            if (Control != null)
            {
                var tapGesture = new UITapGestureRecognizer(() => Control.EndEditing(true));
                Control.AddGestureRecognizer(tapGesture);
            }
        }

        protected override void OnDetached() { }
    }
}
