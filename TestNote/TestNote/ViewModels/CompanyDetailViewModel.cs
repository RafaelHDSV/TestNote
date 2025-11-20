using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using TestNote.Models;

namespace TestNote.ViewModels;

public class CompanyDetailViewModel : INotifyPropertyChanged
{
    private Company company;
    public Company Company
    {
        get => company;
        set { company = value; OnPropertyChanged(); }
    }

    public ICommand EditCompanyCommand { get; }
    public ICommand OpenTestsCommand { get; }
    public ICommand GoBackCommand { get; }

    public CompanyDetailViewModel()
    {
        EditCompanyCommand = new Command(() => OnEdit());
        OpenTestsCommand = new Command(() => OnOpenTests());
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

    private void OnOpenTests()
    {
        // Navegar para listagem de testes desta empresa
    }

    private async Task OnBack()
    {
        await Shell.Current.GoToAsync("..");
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
