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

        [ObservableProperty]
        private string name;
        [ObservableProperty]
        private string jobTitle;

        [ObservableProperty]
        private string email;
        [ObservableProperty]
        private string password = string.Empty; 

        public EmployeeEditViewModel(DatabaseService dbService)
        {
            _dbService = dbService;
        }

        partial void OnEmployeeChanged(Employee value)
        {
            if (value != null)
            {
                Name = value.Name;
                JobTitle = value.JobTitle;
                Email = value.Email; 
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

            Employee.Name = Name;
            Employee.JobTitle = JobTitle;
            await _dbService.SaveItemAsync(Employee);

            var user = await _dbService.GetItemAsync<User>(Employee.UserId);
            if (user != null)
            {
                user.Email = Email;
                if (!string.IsNullOrWhiteSpace(Password))
                {
                    user.Password = Password; 
                }
                await _dbService.SaveItemAsync(user);
            }

            await Shell.Current.DisplayAlert("Sucesso", "Funcionário atualizado!", "OK");
            await Shell.Current.GoToAsync("..");
        }
    }
}