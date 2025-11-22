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

namespace TestNote.ViewModels
{
    [QueryProperty(nameof(CompanyId), "companyId")]
    public partial class ManageEmployeesViewModel : ObservableObject
    {
        private readonly DatabaseService _dbService;

        [ObservableProperty]
        private int companyId;

        [ObservableProperty]
        private ObservableCollection<Employee> employees = [];

        public ManageEmployeesViewModel(DatabaseService dbService)
        {
            _dbService = dbService;
        }

        // Carrega funcionários quando o ID da empresa chega
        partial void OnCompanyIdChanged(int value)
        {
            LoadEmployees();
        }

        [RelayCommand]
        private async Task LoadEmployees()
        {
            var list = await _dbService.GetEmployeesByCompanyAsync(CompanyId);
            Employees = new ObservableCollection<Employee>(list);
        }

        [RelayCommand]
        private async Task CreateEmployee()
        {
            // Exemplo simplificado com Prompt
            string name = await Shell.Current.DisplayPromptAsync("Novo Func", "Nome:");
            string email = await Shell.Current.DisplayPromptAsync("Novo Func", "Email:");
            string pass = "123"; // Senha padrão ou pede via prompt

            if (!string.IsNullOrEmpty(name))
            {
                await _dbService.CreateEmployeeAsync(name, "Cargo Padrão", email, pass, CompanyId);
                await LoadEmployees(); // Recarrega a lista
            }
        }
    }
}
