using TestNote.Models;
using TestNote.ViewModels;

namespace TestNote.Views;

[QueryProperty(nameof(Company), "Company")]
public partial class CompanyDetails : ContentPage
{
    private Company? _company;
    public Company? Company
    {
        get => _company;
        set
        {
            _company = value;
            if (BindingContext is CompanyDetailViewModel vm && _company != null)
            {
                vm.Company = _company;
            }
        }
    }

    public CompanyDetails(CompanyDetailViewModel viewModel)
    {
        InitializeComponent();

        // Definimos o BindingContext aqui. 
        // O MAUI já entregou o viewModel com o DatabaseService configurado.
        BindingContext = viewModel;
    }
}
