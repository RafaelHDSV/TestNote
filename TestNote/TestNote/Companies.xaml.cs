using System.Collections.ObjectModel;
using TestNote.Models;

namespace TestNote;

public partial class Companies : ContentPage
{
    public ObservableCollection<Company> CompaniesList { get; set; } = [];

    public Companies()
	{
		InitializeComponent();
        BindingContext = this;
        LoadCompanies();
    }

    private void LoadCompanies()
    {
        var companies = new List<Company>
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
                new Company { Id = 10, Name = "Atlas Security", Owner = "Fernanda Silveira", Sector = "Cibersegurança", NumberOfEmployees = 90 },
                new Company { Id = 11, Name = "Pulse Digital", Owner = "Henrique Lemos", Sector = "Marketing Digital", NumberOfEmployees = 60 },
                new Company { Id = 12, Name = "UrbanBuild Engenharia", Owner = "Sérgio Gomes", Sector = "Construção Civil", NumberOfEmployees = 1100 },
                new Company { Id = 13, Name = "Bright Education", Owner = "Ana Paula Sousa", Sector = "Tecnologia Educacional", NumberOfEmployees = 200 },
                new Company { Id = 14, Name = "TrueMarket Retail", Owner = "Roberto Teles", Sector = "Varejo e E-commerce", NumberOfEmployees = 700 },
                new Company { Id = 15, Name = "Helix Automation", Owner = "Bianca Rodrigues", Sector = "Automação Industrial", NumberOfEmployees = 430 }
            };

        CompaniesList.Clear();
        foreach (var c in companies)
            CompaniesList.Add(c);
    }
}