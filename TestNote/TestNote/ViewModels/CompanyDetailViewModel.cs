using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using TestNote.Models;
using TestNote.Views;

namespace TestNote.ViewModels;

public class CompanyDetailViewModel : INotifyPropertyChanged
{
    private Company? company;
    public Company? Company
    {
        get => company;
        set { company = value; OnPropertyChanged(); }
    }

    public ICommand EditCompanyCommand { get; }
    public ICommand OpenTestsCommand { get; }
    public ICommand OpenEmployeesCommand { get; }
    public ICommand GoBackCommand { get; }

    public CompanyDetailViewModel()
    {
        EditCompanyCommand = new Command(() => OnEdit());
        OpenTestsCommand = new Command(() => OnOpenTests());
        OpenEmployeesCommand = new Command(() => OnOpenEmployees());
        GoBackCommand = new Command(async () => await OnBack());
    }

    public CompanyDetailViewModel(Company company) : this()
    {
        Company = company;
    }

    private void OnEdit()
    {
        // Navegar para tela de edição
    }

    private async void OnOpenTests()
    {
        // ⚠️ Navegar para listagem de testes desta empresa, passando a Company
        await Shell.Current.GoToAsync(
            $"{nameof(CompanyTests)}?companyId={Company!.Id}",
            new Dictionary<string, object>
            {
            { "Company", Company }
            });
    }

    private async void OnOpenEmployees()
    {
        // Navegar para listagem de funcionários desta empresa
        await Shell.Current.GoToAsync(
           $"{nameof(Employees)}?companyId={Company!.Id}",
           new Dictionary<string, object>
           {
            { "Company", Company }
           });
    }

    private async Task OnBack()
    {
        await Shell.Current.GoToAsync("..");
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    // ⚠️ CORRIGIDO: Renomeado de 'd' para 'OnPropertyChanged'
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
