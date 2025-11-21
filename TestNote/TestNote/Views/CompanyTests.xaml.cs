using TestNote.Models;
using TestNote.ViewModels;

namespace TestNote.Views;

[QueryProperty(nameof(Company), "Company")]
public partial class CompanyTests : ContentPage
{
    private Company? _company;
    public Company? Company
    {
        get => _company;
        set
        {
            _company = value;

            var vm = new CompanyTestsViewModel { Company = _company };
            BindingContext = vm;

            if (_company != null)
            {
               _ = vm.LoadTestsAsync(_company.Id);
            }
        }
    }

    public CompanyTests()
    {
        InitializeComponent();
    }
}