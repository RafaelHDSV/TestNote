using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using TestNote.Models;
using TestNote.Services;

namespace TestNote.ViewModels
{
    public partial class ManagerCreateViewModel : ObservableObject
    {
        private readonly DatabaseService _dbService;

        [ObservableProperty]
        private string name = string.Empty;

        [ObservableProperty]
        private string email = string.Empty;

        [ObservableProperty]
        private string password = string.Empty;

        // Lista de empresas para o Picker
        [ObservableProperty]
        private ObservableCollection<Company> companies = [];

        // Empresa selecionada no Picker
        [ObservableProperty]
        private Company? selectedCompany;

        public ManagerCreateViewModel(DatabaseService dbService)
        {
            _dbService = dbService;
            // Carrega as empresas assim que o VM é criado
            _ = LoadCompanies();
        }

        private async Task LoadCompanies()
        {
            var list = await _dbService.GetItemsAsync<Company>();
            Companies = new ObservableCollection<Company>(list);
        }

        [RelayCommand]
        private async Task SaveManager()
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                await Shell.Current.DisplayAlert("Erro", "Preencha todos os campos.", "OK");
                return;
            }

            if (SelectedCompany == null)
            {
                await Shell.Current.DisplayAlert("Erro", "Selecione uma empresa para o gerente.", "OK");
                return;
            }

            // Cria o Login e o Perfil de Manager
            await _dbService.CreateManagerAsync(Name, Email, Password, SelectedCompany.Id);

            await Shell.Current.DisplayAlert("Sucesso", "Gerente criado e vinculado!", "OK");
            await Shell.Current.GoToAsync("..");
        }
    }
}