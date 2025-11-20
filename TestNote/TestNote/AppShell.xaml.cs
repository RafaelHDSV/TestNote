using TestNote.Views;

namespace TestNote
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(CompanyDetails), typeof(CompanyDetails));
        }
    }
}
