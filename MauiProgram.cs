using Camera.MAUI;
using CommunityToolkit.Maui;
using Controls.UserDialogs.Maui;
using Mopups.Hosting;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Core.Hosting;
using TripBliss.Helpers;
using Mopups.PreBaked.PopupPages.Login;
using TripBliss.Pages;
using TripBliss.ViewModels;
using TripBliss.Services.Data;
using Maui.PDFView;
using TripBliss.Pages.Shared;

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
                .ConfigureMopups()
                .ConfigureSyncfusionCore()
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
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
            {
#if ANDROID
            handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#endif
            });
            builder.Services.AddSingleton<ServicesService>();
            builder.Services.AddScoped<IGenericRepository,GenericRepository>();
            //builder.Services.AddScoped<IServicesService,ServicesService>();

            builder.Services.AddTransient<SignUpViewModel>();
            builder.Services.AddTransient<ViewModels.LoginViewModel>();
            builder.Services.AddTransient<ViewModels.TravelAgenciesViewModels.CreateRequest.Tr_C_AirFlightServicesViewModel>();


            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<SignUpPage>();
            builder.Services.AddTransient<Pages.TravelAgenciesPages.CreateRequest.AirFlightServicePage>();

            return builder.Build();
        }
    }
}
