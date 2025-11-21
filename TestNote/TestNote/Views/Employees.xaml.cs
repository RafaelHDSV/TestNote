using TestNote.Models;
using TestNote.ViewModels;

namespace TestNote.Views;
public partial class Employees : ContentPage
{
    private Company? _company;
    public Company? Company
    {
        get => _company;
        set
        {
            _company = value;

            var vm = new EmployeesViewModel { Company = _company };
            BindingContext = vm;

            if (_company != null)
            {
                _ = vm.LoadEmployeesAsync(_company.Id);
            }
        }
    }

    public Employees()
	{
		InitializeComponent();
	}
}