using CommunityToolkit.Mvvm.ComponentModel;
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
    public partial class EmployeesViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        [ObservableProperty]
        private Company? company;

        [ObservableProperty]
        private ObservableCollection<Employee> employees = [];

        [ObservableProperty]
        private bool isLoading;

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
    }
}
