using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using TestNote.Models;
using TestNote.Services;
using TestNote.Views;

namespace TestNote.ViewModels;

public partial class CompanyDetailViewModel : INotifyPropertyChanged
{
    private readonly DatabaseService _databaseService;

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

    public CompanyDetailViewModel(DatabaseService databaseService)
    {
        _databaseService = databaseService;

        EditCompanyCommand = new Command(() => OnEdit());
        OpenTestsCommand = new Command(() => OnOpenTests());
        OpenEmployeesCommand = new Command(() => OnOpenEmployees());
        GoBackCommand = new Command(async () => await OnBack());
    }

    public CompanyDetailViewModel() : this(null!) { }

    private async void OnEdit()
    {
        if (Company == null) return;

        await Shell.Current.GoToAsync(nameof(CompanyEditPage), new Dictionary<string, object>
        {
            { "Company", Company }
        });
    }

    public void Refresh()
    {
        OnPropertyChanged(nameof(Company));
    }

    private async void OnOpenTests()
    {
        await Shell.Current.GoToAsync(
            $"{nameof(CompanyTests)}?companyId={Company!.Id}",
            new Dictionary<string, object>
            {
            { "Company", Company }
            });
    }

    private async void OnOpenEmployees()
    {
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

    [RelayCommand]
    private async Task AddManagerAsync()
    {
        string name = await Shell.Current.DisplayPromptAsync("Novo Gerente", "Nome do Gerente:");
        string email = await Shell.Current.DisplayPromptAsync("Novo Gerente", "Email de Login:");
        string pass = await Shell.Current.DisplayPromptAsync("Novo Gerente", "Senha:");

        if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(email))
        {
            await _databaseService.CreateManagerAsync(name, email, pass, Company.Id);
            await Shell.Current.DisplayAlert("Sucesso", "Gerente criado!", "OK");
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
