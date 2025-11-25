using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TestNote.Models;
using TestNote.Services;

namespace TestNote.ViewModels
{
    public partial class CompanyCreateViewModel : ObservableObject
    {
        private readonly DatabaseService _dbService;

        [ObservableProperty]
        private string name = string.Empty;

        [ObservableProperty]
        private string owner = string.Empty;

        [ObservableProperty]
        private string sector = string.Empty;

        [ObservableProperty]
        private int numberOfEmployees;

        public CompanyCreateViewModel(DatabaseService dbService)
        {
            _dbService = dbService;
        }

        [RelayCommand]
        private async Task SaveCompany()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                await Shell.Current.DisplayAlert("Erro", "O nome da empresa é obrigatório.", "OK");
                return;
            }

            var newCompany = new Company
            {
                Name = Name,
                Owner = Owner,
                Sector = Sector,
                NumberOfEmployees = NumberOfEmployees
            };

            await _dbService.SaveItemAsync(newCompany);
            await Shell.Current.DisplayAlert("Sucesso", "Empresa criada!", "OK");

            await Shell.Current.GoToAsync("..");
        }
    }
}