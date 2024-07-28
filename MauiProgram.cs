﻿using Camera.MAUI;
using CommunityToolkit.Maui;
using Controls.UserDialogs.Maui;
using Mopups.Hosting;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Core.Hosting;
using TripBliss.Helpers;

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
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Font Awesome 6 Brands-Regular-400.otf", "FontIconBrand");
                    fonts.AddFont("Font Awesome 6 Free-Regular-400.otf", "FontIcon");
                    fonts.AddFont("Font Awesome 6 Free-Solid-900.otf", "FontIconSolid");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddScoped<IGenericRepository,GenericRepository>();

            return builder.Build();
        }
    }
}
