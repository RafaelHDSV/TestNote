using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNote.Models;
using TestNote.Services;
using TestNote.Views;

namespace TestNote.ViewModels
{
    public partial class EmployeesViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        [ObservableProperty]
        private Company? company;

        [ObservableProperty]
        private ObservableCollection<Employee> employees = [];

        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private Employee? selectedEmployee;

        public EmployeesViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task LoadEmployeesAsync(int companyId)
        {
            IsLoading = true;

            // 1. Removemos a lista fixa 'allEmployeesData' que causava os erros CS0117.

            // 2. Buscamos do banco de dados usando o método que criamos no passo anterior
            var dbEmployees = await _databaseService.GetEmployeesByCompanyAsync(companyId);

            // 3. Atualizamos a lista observável
            Employees = new ObservableCollection<Employee>(dbEmployees);

            IsLoading = false;
        }

        [RelayCommand]
        private async Task GoToCreateEmployee()
        {
            if (Company == null)
            {
                await Shell.Current.DisplayAlert("Erro", "Empresa não definida.", "OK");
                return;
            }

            // Navega para a página de criação de funcionário, passando a CompanyId
            await Shell.Current.GoToAsync($"{nameof(EmployeeCreatePage)}?companyId={Company.Id}");
        }

        [RelayCommand]
        private async Task SelectEmployee(Employee employee)
        {
            if (employee == null) return;

            // Navega para a página de edição, passando o objeto Employee
            await Shell.Current.GoToAsync(nameof(EmployeeEditPage), new Dictionary<string, object>
            {
                { "Employee", employee }
            });

            // Limpa a seleção após navegar
            SelectedEmployee = null;
        }
    }
}
