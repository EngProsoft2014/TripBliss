using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;
using Android.Views;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using Firebase;
using Plugin.FirebasePushNotifications.Platforms;
using TripBliss.Models;

namespace TripBliss
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTask, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Microsoft.Maui.ApplicationModel.Platform.Init(this, savedInstanceState);

            this.Window?.AddFlags(WindowManagerFlags.Fullscreen);

            HandleNotificationIntent(Intent);

            //Request Notification Permission
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Tiramisu)
            {
                if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.PostNotifications) != Permission.Granted)
                {
                    ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.PostNotifications }, 0);
                }
            }
        }

        protected override void AttachBaseContext(Context? @base)
        {
            Configuration configuration = new(@base!.Resources!.Configuration)
            {
                FontScale = 1.0f
            };
            ApplyOverrideConfiguration(configuration);
            base.AttachBaseContext(@base);
        }


        private void HandleNotificationIntent(Intent intent)
        {
            if (intent?.Extras != null)
            {
                var notification = new NotificationDto
                {
                    RequestId = intent.Extras.GetString("requestId"),
                    OfferId = intent.Extras.GetString("offerId"),
                    Type = intent.Extras.GetString("type"),
                    Title = intent.Extras.GetString("title"),
                    Message = intent.Extras.GetString("body")
                };

                var json = System.Text.Json.JsonSerializer.Serialize(notification);

                // ✅ تخزين الإشعار
                Preferences.Default.Set("PendingNotificationData", json);
            }
        }
    }

}
