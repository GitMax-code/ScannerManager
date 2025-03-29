using CommunityToolkit.Maui;
using Microcharts.Maui;
using Microsoft.Extensions.Logging;
//using ScannerManager.Service;
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
                .UseMicrocharts()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            
            builder.Services.AddSingleton<HomeView>();
            builder.Services.AddSingleton<HomeViewModel>();


            builder.Services.AddSingleton<MainView>();
            builder.Services.AddSingleton<MainViewModel>();

            builder.Services.AddTransient<DetailsView>();
            builder.Services.AddTransient<DetailsViewModel>();

            builder.Services.AddTransient<AddAnimalView>();
            builder.Services.AddTransient<AddAnimalViewModel>();

            builder.Services.AddTransient<GraphView>();
            builder.Services.AddTransient<GraphViewModel>();

            builder.Services.AddSingleton<DeviceOrientationService>();
            builder.Services.AddSingleton<JSONServices>();
            builder.Services.AddSingleton<CSVServices>();

            return builder.Build();
        }
    }
}
