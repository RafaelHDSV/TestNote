using Microsoft.Extensions.Logging;
using TestNote.Services;
using TestNote.ViewModels;
using TestNote.Views;

namespace TestNote
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddSingleton<DatabaseService>();

            // 2. VIEW MODELS
            builder.Services.AddTransient<CompaniesViewModel>();
            builder.Services.AddTransient<CompanyTestsViewModel>();
            builder.Services.AddTransient<CompanyDetailViewModel>();
            builder.Services.AddTransient<EmployeesViewModel>();

            // 3. VIEWS
            builder.Services.AddTransient<Companies>();
            builder.Services.AddTransient<CompanyTests>();
            builder.Services.AddTransient<CompanyDetails>();
            builder.Services.AddTransient<Employees>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
