using TestNote.Views;

namespace TestNote
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(CompanyDetails), typeof(CompanyDetails));

            Routing.RegisterRoute(nameof(Employees), typeof(Employees));

            Routing.RegisterRoute(nameof(CompanyTests), typeof(CompanyTests));

            Routing.RegisterRoute(nameof(ManageEmployeesPage), typeof(ManageEmployeesPage));
        }
    }
}
