using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Xml.Linq;
using TestNote.Models;
using TestNote.Services;

namespace TestNote.ViewModels
{
    [QueryProperty(nameof(Company), "Company")]
    public partial class CompanyEditViewModel : ObservableObject
    {
        private readonly DatabaseService _dbService;

        // A empresa original recebida via navegação
        [ObservableProperty]
        private Company company;

        // Propriedades editáveis (para não alterar o objeto original antes de salvar)
        [ObservableProperty]
        private string name;
        [ObservableProperty]
        private string owner;
        [ObservableProperty]
        private string sector;
        [ObservableProperty]
        private int numberOfEmployees;

        public CompanyEditViewModel(DatabaseService dbService)
        {
            _dbService = dbService;
        }

        // Quando a propriedade 'Company' é preenchida pela navegação, preenchemos os campos
        partial void OnCompanyChanged(Company value)
        {
            if (value != null)
            {
                Name = value.Name;
                Owner = value.Owner;
                Sector = value.Sector;
                NumberOfEmployees = value.NumberOfEmployees;
            }
        }

        [RelayCommand]
        private async Task UpdateCompany()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                await Shell.Current.DisplayAlert("Erro", "Nome é obrigatório", "OK");
                return;
            }

            // Atualiza os dados do objeto original
            Company.Name = Name;
            Company.Owner = Owner;
            Company.Sector = Sector;
            Company.NumberOfEmployees = NumberOfEmployees;

            // Salva no banco (O método SaveItemAsync já sabe fazer Update se o ID > 0)
            await _dbService.SaveItemAsync(Company);

            await Shell.Current.DisplayAlert("Sucesso", "Empresa atualizada!", "OK");

            // Volta para a tela anterior
            await Shell.Current.GoToAsync("..");
        }
    }
}