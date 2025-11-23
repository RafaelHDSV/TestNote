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

            // VIEW MODELS
            builder.Services.AddTransient<CompaniesViewModel>();
            builder.Services.AddTransient<CompanyTestsViewModel>();
            builder.Services.AddTransient<CompanyDetailViewModel>();
            builder.Services.AddTransient<EmployeesViewModel>();
            builder.Services.AddTransient<CompanyCreateViewModel>();
            builder.Services.AddTransient<ManagerCreateViewModel>();
            builder.Services.AddTransient<CompanyEditViewModel>();
            builder.Services.AddTransient<EmployeeCreateViewModel>();
            builder.Services.AddTransient<EmployeeEditViewModel>();
            builder.Services.AddTransient<AdminUsersViewModel>();
            builder.Services.AddTransient<TestCreateViewModel>();
            builder.Services.AddTransient<TestExecutionViewModel>();

            // VIEWS
            builder.Services.AddTransient<Companies>();
            builder.Services.AddTransient<CompanyTests>();
            builder.Services.AddTransient<CompanyDetails>();
            builder.Services.AddTransient<Employees>();
            builder.Services.AddTransient<CompanyCreatePage>();
            builder.Services.AddTransient<ManagerCreatePage>();
            builder.Services.AddTransient<CompanyEditPage>();
            builder.Services.AddTransient<EmployeeCreatePage>();
            builder.Services.AddTransient<EmployeeEditPage>();
            builder.Services.AddTransient<AdminUsersPage>();
            builder.Services.AddTransient<TestCreatePage>();
            builder.Services.AddTransient<TestExecutionPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
