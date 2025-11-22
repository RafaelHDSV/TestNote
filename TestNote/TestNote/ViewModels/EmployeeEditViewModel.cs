using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TestNote.Models;
using TestNote.Services;

namespace TestNote.ViewModels
{
    [QueryProperty(nameof(Employee), "Employee")]
    public partial class EmployeeEditViewModel : ObservableObject
    {
        private readonly DatabaseService _dbService;

        [ObservableProperty]
        private Employee employee;

        // Propriedades do formulário
        [ObservableProperty]
        private string name;
        [ObservableProperty]
        private string jobTitle;

        // Propriedades do User (Para fins didáticos, manteremos o email e a senha separadas)
        [ObservableProperty]
        private string email;
        [ObservableProperty]
        private string password = string.Empty; // Senha só será alterada se digitada

        public EmployeeEditViewModel(DatabaseService dbService)
        {
            _dbService = dbService;
        }

        // Popula os campos quando o objeto Employee é recebido
        partial void OnEmployeeChanged(Employee value)
        {
            if (value != null)
            {
                Name = value.Name;
                JobTitle = value.JobTitle;
                Email = value.Email; // O Email é preenchido no GetEmployeesByCompanyAsync
            }
        }

        [RelayCommand]
        private async Task UpdateEmployee()
        {
            if (Employee == null || string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Email))
            {
                await Shell.Current.DisplayAlert("Erro", "Dados inválidos.", "OK");
                return;
            }

            // 1. Atualiza o perfil de Employee
            Employee.Name = Name;
            Employee.JobTitle = JobTitle;
            await _dbService.SaveItemAsync(Employee);

            // 2. Atualiza o User (Email e Senha)
            var user = await _dbService.GetItemAsync<User>(Employee.UserId);
            if (user != null)
            {
                user.Email = Email;
                if (!string.IsNullOrWhiteSpace(Password))
                {
                    user.Password = Password; // Atualiza a senha se for digitada
                }
                await _dbService.SaveItemAsync(user);
            }

            await Shell.Current.DisplayAlert("Sucesso", "Funcionário atualizado!", "OK");
            await Shell.Current.GoToAsync("..");
        }
    }
}