using TestNote.Models;
using TestNote.ViewModels;

namespace TestNote.Views;

[QueryProperty(nameof(Company), "Company")]
public partial class CompanyTests : ContentPage
{
    private Company? _company;

    public CompanyTests(CompanyTestsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel; // Define o BindingContext aqui
    }

    public Company? Company
    {
        get => _company;
        set
        {
            _company = value;

            // ⚠️ CORREÇÃO: Usa o ViewModel já existente no BindingContext
            if (BindingContext is CompanyTestsViewModel vm && _company != null)
            {
                vm.Company = _company; // Atualiza a propriedade no VM
            }
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is CompanyTestsViewModel vm && vm.Company != null)
        {
            await vm.LoadTestsAsync(vm.Company.Id);
        }
    }
}