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

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is EmployeesViewModel vm && vm.Company != null)
        {
            // Força o recarregamento dos funcionários sempre que a página aparece (útil após criação ou edição)
            _ = vm.LoadEmployeesAsync(vm.Company.Id);
        }
    }
}