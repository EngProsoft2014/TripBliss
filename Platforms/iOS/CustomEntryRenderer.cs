using Microsoft.Maui.Controls.Compatibility.Platform.iOS;

using Foundation;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Platform.iOS;
using UIKit;
using TripBliss.Platforms.iOS;
using Microsoft.Maui.Controls.Platform;

[assembly: Microsoft.Maui.Controls.Compatibility.ExportRenderer(typeof(Entry), typeof(CustomEntryRenderer))]
namespace TripBliss.Platforms.iOS
{
    public class CustomEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                var nativeEntry = (UITextField)Control;
                nativeEntry.Delegate = new CustomEntryDelegate();
            }
        }

        private class CustomEntryDelegate : UITextFieldDelegate
        {
            public override bool ShouldReturn(UITextField textField)
            {
                // Dismiss the keyboard when the Return key is pressed or when the user taps outside
                textField.ResignFirstResponder();
                return true;
            }
        }
    }
}
