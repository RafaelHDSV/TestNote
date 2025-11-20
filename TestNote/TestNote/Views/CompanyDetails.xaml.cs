using TestNote.Models;
using TestNote.ViewModels;

namespace TestNote.Views;

[QueryProperty(nameof(Company), "Company")]
public partial class CompanyDetails : ContentPage
{
    private Company _company;
    public Company Company
    {
        get => _company;
        set
        {
            _company = value;
            // Ao receber a Company, cria o VM com notificação
            BindingContext = new CompanyDetailViewModel(_company);
        }
    }

    public CompanyDetails()
    {
        InitializeComponent();
        // Não setar BindingContext aqui; será setado quando Company for injetada.
    }
}
