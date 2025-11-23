using TestNote.Views;

namespace TestNote
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(Companies), typeof(Companies));

            Routing.RegisterRoute(nameof(CompanyDetails), typeof(CompanyDetails));

            Routing.RegisterRoute(nameof(Employees), typeof(Employees));

            Routing.RegisterRoute(nameof(CompanyTests), typeof(CompanyTests));

            Routing.RegisterRoute(nameof(CompanyCreatePage), typeof(CompanyCreatePage));

            Routing.RegisterRoute(nameof(ManagerCreatePage), typeof(ManagerCreatePage));

            Routing.RegisterRoute(nameof(CompanyEditPage), typeof(CompanyEditPage));

            Routing.RegisterRoute(nameof(EmployeeCreatePage), typeof(EmployeeCreatePage));

            Routing.RegisterRoute(nameof(EmployeeEditPage), typeof(EmployeeEditPage));

            Routing.RegisterRoute(nameof(AdminUsersPage), typeof(AdminUsersPage));

            Routing.RegisterRoute(nameof(TestCreatePage), typeof(TestCreatePage));

            Routing.RegisterRoute(nameof(TestExecutionPage), typeof(TestExecutionPage));
        }
    }
}
