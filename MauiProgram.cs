using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using MyApp.Service;
using System.Xml;

namespace ScannerManager
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<MainView>();
            builder.Services.AddSingleton<MainViewModel>();

            builder.Services.AddTransient<DetailsView>();
            builder.Services.AddTransient<DetailsViewModel>();

            builder.Services.AddTransient<AddAnimalView>();
            builder.Services.AddTransient<AddAnimalViewModel>();

            builder.Services.AddSingleton<DeviceOrientationService>();
            builder.Services.AddSingleton<JSONServices>();
            builder.Services.AddSingleton<CSVServices>();

            return builder.Build();
        }
    }
}
