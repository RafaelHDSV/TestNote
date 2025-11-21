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
    public partial class CompanyTestsViewModel : ObservableObject
    {
        [ObservableProperty]
        private Company? company;

        [ObservableProperty]
        private ObservableCollection<Test> tests = [];

        public CompanyTestsViewModel() { }

        public async Task LoadTestsAsync(int companyId)
        {
            // Simulação da lógica de carregamento de testes para a empresa
            var data = new List<Test>
            {
                new Test { Id = 101, Title = "Teste de Integração A", CompanyId = 1 },
                new Test { Id = 102, Title = "Teste de Carga B", CompanyId = 1 },
                new Test { Id = 201, Title = "Teste Unitário X", CompanyId = 2 },
                new Test { Id = 202, Title = "Teste de Regressão Y", CompanyId = 2 }
            };

            Tests = new ObservableCollection<Test>(data.Where(t => t.CompanyId == companyId));

            // Simulação de delay
            await Task.Delay(100);
        }
    }
}
