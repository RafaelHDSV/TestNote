using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNote.Models;

namespace TestNote.ViewModels
{
    public partial class CompaniesViewModel : ObservableObject
    {
        [ObservableProperty]
        public ObservableCollection<Company> companies = [];

        [ObservableProperty]
        public bool isLoading; // Corrigido para Campo Privado

        public CompaniesViewModel()
        {
            LoadCompaniesCommand = new AsyncRelayCommand(LoadCompaniesAsync);
        }

        public IAsyncRelayCommand LoadCompaniesCommand { get; }

        private async Task LoadCompaniesAsync()
        {
            IsLoading = true;

            await Task.Delay(500);
            var data = new List<Company>
            {
                new Company { Id = 1, Name = "Alpha Corp", Owner = "Daniel Moreira", Sector = "Tecnologia e Inovação", NumberOfEmployees = 15 },
                new Company { Id = 2, Name = "Beta Solutions", Owner = "Patrícia Vieira", Sector = "Consultoria em Sistemas", NumberOfEmployees = 150 },
                new Company { Id = 3, Name = "Gamma Group", Owner = "Ricardo Martins", Sector = "Desenvolvimento de Software", NumberOfEmployees = 1500 },
                new Company { Id = 4, Name = "Nexus Tech Labs", Owner = "Eduardo Andrade", Sector = "Inteligência Artificial", NumberOfEmployees = 300 },
                new Company { Id = 5, Name = "Skyway Logistics", Owner = "Letícia Cortes", Sector = "Logística e Transporte", NumberOfEmployees = 1200 },
                new Company { Id = 6, Name = "GreenField Agro", Owner = "Marcelo Farias", Sector = "Agronegócio", NumberOfEmployees = 800 },
                new Company { Id = 7, Name = "Prime Financial", Owner = "João Azevedo", Sector = "Serviços Financeiros", NumberOfEmployees = 500 },
                new Company { Id = 8, Name = "MediLife Health", Owner = "Carolina Mendonça", Sector = "Tecnologia para Saúde", NumberOfEmployees = 220 },
                new Company { Id = 9, Name = "OceanBlue Energy", Owner = "Vitor Ramos", Sector = "Energia Sustentável", NumberOfEmployees = 350 },
                new Company { Id = 10, Name = "Atlas Security", Owner = "Fernanda Silveira", Sector = "Cibersegurança", NumberOfEmployees = 90 }
            };

            Companies = new ObservableCollection<Company>(data);

            IsLoading = false;
        }
    }
}
