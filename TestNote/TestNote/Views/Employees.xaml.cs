using TestNote.Models;
using TestNote.ViewModels;

namespace TestNote.Views;

[QueryProperty(nameof(Company), "Company")]
public partial class Employees : ContentPage
{
    private Company? _company;

    public Employees(EmployeesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    public Company? Company
    {
        get => _company;
        set
        {
            _company = value;

            if (BindingContext is EmployeesViewModel vm && _company != null)
            {
                vm.Company = _company; // Atualiza a propriedade no VM
                _ = vm.LoadEmployeesAsync(_company.Id);
            }
        }
    }
}