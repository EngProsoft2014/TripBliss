using Camera.MAUI;
using CommunityToolkit.Maui;
using Controls.UserDialogs.Maui;
using Maui.PDFView;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;
using Mopups.Hosting;
using Mopups.PreBaked.PopupPages.Login;
using Plugin.FirebasePushNotifications;
using Syncfusion.Maui.Core.Hosting;
using TripBliss.Helpers;
using TripBliss.Pages;
using TripBliss.Pages.Shared;
using TripBliss.Services.Data;
using TripBliss.ViewModels;


namespace TripBliss
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseMauiCameraView()
                .UseUserDialogs()
                .UseMauiMaps()
                .ConfigureMopups()
                .ConfigureSyncfusionCore()
                .UseFirebasePushNotifications()
                .UseMauiPdfView()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Font Awesome 6 Brands-Regular-400.otf", "FontIconBrand");
                    fonts.AddFont("Font Awesome 6 Free-Regular-400.otf", "FontIcon");
                    fonts.AddFont("Font Awesome 6 Free-Solid-900.otf", "FontIconSolid");
                    fonts.AddFont("ElMessiri-Regular.ttf", "Almarai-Regular");
                    fonts.AddFont("ElMessiri-Bold.ttf", "Almarai-Bold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddDependencies();

            DependencyInjection.ControlsBackground();

            builder.ConfigureMauiHandlers(handlers =>
            {
#if ANDROID 
                handlers.AddHandler<Microsoft.Maui.Controls.Maps.Map, Platforms.Android.CustomMapHandler>();
#elif IOS
                handlers.AddHandler<Microsoft.Maui.Controls.Maps.Map, Platforms.iOS.CustomMapHandler>();
#endif
            });

            return builder.Build();
        }

    }
}
