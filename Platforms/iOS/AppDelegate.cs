using Foundation;
using ObjCRuntime;
using Plugin.FirebasePushNotifications;
using TripBliss.Models;
using UIKit;
using UserNotifications;

namespace TripBliss
{
    [Register("AppDelegate")]
    public class AppDelegate : MauiUIApplicationDelegate
    {
        public static Action? BackgroundSessionCompletionHandler { get; set; }
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            var result = base.FinishedLaunching(application, launchOptions);

            // Get the main window
            var window = UIApplication.SharedApplication.KeyWindow;

            if (window != null)
            {
                // Add a tap gesture recognizer to dismiss the keyboard
                var tapRecognizer = new UITapGestureRecognizer(() =>
                {
                    window.EndEditing(true); // Dismiss the keyboard
                })
                {
                    CancelsTouchesInView = false // Ensure other UI interactions are not blocked
                };
                window.AddGestureRecognizer(tapRecognizer);
            }

            // Request permission for notifications
            UNUserNotificationCenter.Current.RequestAuthorization(
                UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound,
                (granted, error) =>
                {
                    if (granted)
                    {
                        Console.WriteLine("Notification permission granted.");
                        InvokeOnMainThread(UIApplication.SharedApplication.RegisterForRemoteNotifications);
                    }
                    else
                    {
                        Console.WriteLine("Notification permission denied.");
                    }
                });

            // 📡 Listen for background session completion
            NSNotificationCenter.DefaultCenter.AddObserver(
                new NSString("NSURLSessionDidFinishEventsForBackgroundURLSessionNotification"),
                notification =>
                {
                    Console.WriteLine("📱 Background session finished events received.");

                    if (BackgroundSessionCompletionHandler != null)
                    {
                        BackgroundSessionCompletionHandler.Invoke();
                        BackgroundSessionCompletionHandler = null;
                    }

                    // Optional: show local notification when upload completes
                    var content = new UNMutableNotificationContent
                    {
                        Title = "Upload Complete",
                        Body = "Your meeting recording has been successfully uploaded.",
                        Sound = UNNotificationSound.Default
                    };

                    var request = UNNotificationRequest.FromIdentifier(
                        Guid.NewGuid().ToString(),
                        content,
                        null
                    );

                    UNUserNotificationCenter.Current.AddNotificationRequest(request, null);
                });

            return result;
        }

        [Export("application:didRegisterForRemoteNotificationsWithDeviceToken:")]
        [BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
        public void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            IFirebasePushNotification.Current.RegisteredForRemoteNotifications(deviceToken);
        }

        [Export("application:didFailToRegisterForRemoteNotificationsWithError:")]
        [BindingImpl(BindingImplOptions.GeneratedCode | BindingImplOptions.Optimizable)]
        public void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {
            IFirebasePushNotification.Current.FailedToRegisterForRemoteNotifications(error);
        }

        [Export("application:didReceiveRemoteNotification:fetchCompletionHandler:")]
        public void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            IFirebasePushNotification.Current.DidReceiveRemoteNotification(userInfo);
            completionHandler(UIBackgroundFetchResult.NewData);

            var notification = new NotificationDto
            {
                RequestId = userInfo["requestId"]?.ToString(),
                OfferId = userInfo["offerId"]?.ToString(),
                Type = userInfo["type"]?.ToString(),
                Title = userInfo["title"]?.ToString(),
                Message = userInfo["body"]?.ToString()
            };

            var json = System.Text.Json.JsonSerializer.Serialize(notification);

            // ✅ تخزين الإشعار
            Preferences.Default.Set("PendingNotificationData", json);
        }


    }
}
