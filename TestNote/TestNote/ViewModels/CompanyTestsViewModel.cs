using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNote.Models;
using TestNote.Services;
using TestNote.Views;

namespace TestNote.ViewModels
{
    public partial class CompanyTestsViewModel : ObservableObject
    {
        private readonly DatabaseService _dbService;

        [ObservableProperty]
        private Company? company;

        [ObservableProperty]
        private ObservableCollection<Test> tests = [];

        [ObservableProperty]
        private bool isLoading;

        public CompanyTestsViewModel(DatabaseService dbService)
        {
            _dbService = dbService;
        }

        public async Task LoadTestsAsync(int companyId)
        {
            IsLoading = true;
            var data = await _dbService.GetTestsByCompanyAsync(companyId);
            Tests = new ObservableCollection<Test>(data);
            IsLoading = false;
        }

        [RelayCommand]
        private async Task GoToCreateTest()
        {
            if (Company == null) return;

            // Navega para tela de criação
            await Shell.Current.GoToAsync($"{nameof(TestCreatePage)}?companyId={Company.Id}");
        }

        [RelayCommand]
        private async Task SelectTest(Test test)
        {
            if (test == null) return;
            // Navega para detalhes/edição do teste
            // await Shell.Current.GoToAsync($"{nameof(TestEditPage)}?testId={test.Id}");
            await Shell.Current.DisplayAlert("Teste Selecionado", test.Title, "OK");
        }

        [RelayCommand]
        private async Task GoToExecuteTest(Test test)
        {
            if (test == null)
                return;

            // Navega para a página de execução, passando o objeto Test selecionado
            await Shell.Current.GoToAsync(nameof(TestExecutionPage), new Dictionary<string, object>
            {
                { "Test", test }
            });
        }
    }
}
