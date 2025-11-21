using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNote.Models;

namespace TestNote.ViewModels
{
    public partial class EmployeesViewModel : ObservableObject
    {
        [ObservableProperty]
        private Company? company;

        [ObservableProperty]
        private ObservableCollection<Employee> employees = [];

        [ObservableProperty]
        private bool isLoading;

        public EmployeesViewModel() { }

        public async Task LoadEmployeesAsync(int companyId)
        {
            IsLoading = true;

            var allEmployeesData = new List<Employee>
            {
                new Employee { Id = 1, Name = "Admin User", Email = "admin@company.com", CompanyId = 1, AccessLevel = 1 },
                new Employee { Id = 2, Name = "Gerente A", Email = "manager.a@company.com", CompanyId = 1, AccessLevel = 2 },
                new Employee { Id = 3, Name = "Funcionário 1", Email = "func1@company.com", CompanyId = 1, AccessLevel = 3 },

                new Employee { Id = 4, Name = "Gerente B", Email = "manager.b@company.net", CompanyId = 2, AccessLevel = 2 },
                new Employee { Id = 5, Name = "Funcionário 2", Email = "func2@company.net", CompanyId = 2, AccessLevel = 3 },

                new Employee { Id = 6, Name = "Funcionário 3", Email = "func3@company.org", CompanyId = 3, AccessLevel = 3 },
            };

            var filteredEmployees = allEmployeesData.Where(e => e.CompanyId == companyId);
            Employees = new ObservableCollection<Employee>(filteredEmployees);

            // Simulação de delay
            await Task.Delay(500);

            IsLoading = false;
        }
    }
}
