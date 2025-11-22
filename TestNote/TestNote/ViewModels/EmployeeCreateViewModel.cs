using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TestNote.Services;

namespace TestNote.ViewModels
{
    [QueryProperty(nameof(CompanyId), "companyId")]
    public partial class EmployeeCreateViewModel : ObservableObject
    {
        private readonly DatabaseService _dbService;

        [ObservableProperty]
        private string name = string.Empty;

        [ObservableProperty]
        private string jobTitle = string.Empty;

        [ObservableProperty]
        private string email = string.Empty;

        [ObservableProperty]
        private string password = string.Empty;

        // Recebido via QueryProperty
        [ObservableProperty]
        private int companyId;

        public EmployeeCreateViewModel(DatabaseService dbService)
        {
            _dbService = dbService;
        }

        [RelayCommand]
        private async Task SaveEmployee()
        {
            if (CompanyId == 0 || string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                await Shell.Current.DisplayAlert("Erro", "Preencha todos os campos obrigatórios e verifique a empresa.", "OK");
                return;
            }

            // Cria o Login (User) e o Perfil (Employee)
            await _dbService.CreateEmployeeAsync(Name, JobTitle, Email, Password, CompanyId);

            await Shell.Current.DisplayAlert("Sucesso", "Funcionário criado e vinculado!", "OK");

            // Volta para a lista de funcionários
            await Shell.Current.GoToAsync("..");
        }
    }
}