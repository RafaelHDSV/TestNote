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

        [ObservableProperty]
        private Company company;

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

            Company.Name = Name;
            Company.Owner = Owner;
            Company.Sector = Sector;
            Company.NumberOfEmployees = NumberOfEmployees;

            await _dbService.SaveItemAsync(Company);

            await Shell.Current.DisplayAlert("Sucesso", "Empresa atualizada!", "OK");

            await Shell.Current.GoToAsync("..");
        }
    }
}